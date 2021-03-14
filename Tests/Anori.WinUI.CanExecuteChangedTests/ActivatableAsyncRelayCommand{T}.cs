// -----------------------------------------------------------------------
// <copyright file="ActivatableAsyncRelayCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.CanExecuteChangedTests
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Anori.Common;
    using Anori.Extensions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="IAsyncCommand" />
    /// <seealso cref="CanExecuteChangedTests.IActivatable" />
    public class ActivatableAsyncRelayCommand<T> : IAsyncCommand<T>, IActivatable
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        [CanBeNull]
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The error handler
        /// </summary>
        [CanBeNull]
        private readonly Action<Exception> error;

        /// <summary>
        ///     The execute
        /// </summary>
        [NotNull]
        private readonly Func<T, Task> execute;

        /// <summary>
        ///     The is active
        /// </summary>
        private bool isActive;

        /// <summary>
        ///     The is executing
        /// </summary>
        private bool isExecuting;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <exception cref="ArgumentNullException">errorHandler</exception>
        public ActivatableAsyncRelayCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] Action<Exception> error)
            : this(execute, canExecute)
        {
            this.error = error ?? throw new ArgumentNullException(nameof(error));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        public ActivatableAsyncRelayCommand([NotNull] Func<T, Task> execute, [NotNull] Predicate<T> canExecute)
            : this(execute)
        {
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="error">The error handler.</param>
        /// <exception cref="ArgumentNullException">errorHandler</exception>
        public ActivatableAsyncRelayCommand([NotNull] Func<T, Task> execute, [NotNull] Action<Exception> error)
            : this(execute)
        {
            this.error = error ?? throw new ArgumentNullException(nameof(error));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ActivatableAsyncRelayCommand([NotNull] Func<T, Task> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        ///     Notifies that the value for <see cref="P:Anori.WinUI.Common.IActivated.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Gets a value indicating whether this instance is activated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is activated; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive
        {
            get => this.isActive;
            private set
            {
                if (this.isActive == value)
                {
                    return;
                }

                this.isActive = value;
                this.OnIsActiveChanged(value);
            }
        }

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        public void Activate()
        {
            if (this.IsActive)
            {
                return;
            }

            this.Subscribe();
            this.IsActive = true;
            this.OnCanExecuteChanged();
        }

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        public void Deactivate()
        {
            if (!this.IsActive)
            {
                return;
            }

            this.Unsubscribe();
            this.IsActive = false;
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(T parameter) => !this.isExecuting && (this.canExecute?.Invoke(parameter) ?? true);

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        bool ICommand.CanExecute(object parameter) => this.CanExecute((T)parameter);

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        void ICommand.Execute(object parameter) => this.ExecuteAsync((T)parameter).FireAndForgetSafeAsync(this.error);

        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public async Task ExecuteAsync(T parameter)
        {
            if (this.CanExecute(parameter))
            {
                try
                {
                    this.isExecuting = true;
                    await this.execute(parameter);
                }
                finally
                {
                    this.isExecuting = false;
                }
            }

            this.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        /// <summary>
        ///     Raises the <see cref="E:CanExecuteChanged" /> event.
        /// </summary>
        protected virtual void OnCanExecuteChanged() => this.CanExecuteChanged.Raise(this, EventArgs.Empty);

        /// <summary>
        ///     Called when [is active changed].
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        protected virtual void OnIsActiveChanged(bool value) => this.IsActiveChanged.Raise(value);

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnCanExecuteChanged(object sender, EventArgs e) => this.OnCanExecuteChanged();

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        private void Subscribe()
        {
            if (this.canExecute == null)
            {
                return;
            }

            CommandManager.RequerySuggested -= this.OnCanExecuteChanged;
            CommandManager.RequerySuggested += this.OnCanExecuteChanged;
        }

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        private void Unsubscribe()
        {
            if (this.canExecute != null)
            {
                CommandManager.RequerySuggested -= this.OnCanExecuteChanged;
            }
        }
    }
}