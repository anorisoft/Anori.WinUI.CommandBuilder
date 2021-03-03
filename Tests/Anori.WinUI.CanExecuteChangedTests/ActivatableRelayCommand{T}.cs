// -----------------------------------------------------------------------
// <copyright file="ActivatableRelayCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.CanExecuteChangedTests
{
    #region

    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using global::CanExecuteChangedTests;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     A Command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class ActivatableRelayCommand<T> : ISyncCommand<T>, IActivatable
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        [CanBeNull]
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The execute
        /// </summary>
        [NotNull]
        private readonly Action<T> execute;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteChangedTests.RelayCommand" /> class and the Command can
        ///     always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ActivatableRelayCommand([NotNull] Action<T> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandT{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        /// <exception cref="System">execute</exception>
        public ActivatableRelayCommand([NotNull] Action<T> execute, [NotNull] Predicate<T> canExecute)
            : this(execute) =>
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        #region ICommand Members

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
        public bool IsActive { get; private set; }

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
            this.IsActiveChanged.Raise(this, false);
            this.CanExecuteChanged.Raise(this, EventArgs.Empty);
        }

        /// <inheritdoc />
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
        [DebuggerStepThrough]
        public bool CanExecute(object parameter) => this.CanExecute((T)parameter);

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
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
            this.IsActiveChanged.Raise(this, false);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter) => this.Execute((T)parameter);

        /// <summary>
        ///     Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public void Execute(T parameter)
        {
            if (this.canExecute == null || this.canExecute(parameter))
            {
                this.execute(parameter);
            }
        }

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnCanExecuteChanged(object sender, EventArgs e) => this.CanExecuteChanged.Raise(this, e);

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

        #endregion
    }
}