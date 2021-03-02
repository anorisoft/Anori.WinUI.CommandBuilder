// -----------------------------------------------------------------------
// <copyright file="ActivatableAsyncRelayCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WinUI.Commands.Commands;

namespace Anori.WinUI.Commands
{
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using CanExecuteChangedTests;

    using JetBrains.Annotations;

    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="AsyncCommandBase{T}" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable" />
    /// <seealso cref="IAsyncCommand" />
    public class ActivatableAsyncRelayCommand<T> : AsyncCommandBase<T>, IActivatable
    {
        /// <summary>
        ///     The is active
        /// </summary>
        private bool isActive;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableAsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="error">The error.</param>
        public ActivatableAsyncRelayCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] Action<Exception> error)
            : base(execute, canExecute, error)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableAsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public ActivatableAsyncRelayCommand([NotNull] Func<T, Task> execute, [NotNull] Predicate<T> canExecute)
            : base(execute, canExecute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableAsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="errorHandler">The error handler.</param>
        public ActivatableAsyncRelayCommand([NotNull] Func<T, Task> execute, [NotNull] Action<Exception> error)
            : base(execute, error)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableAsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public ActivatableAsyncRelayCommand([NotNull] Func<T, Task> execute)
            : base(execute)
        {
        }

        /// <summary>
        ///     Notifies that the value for <see cref="P:Anori.WinUI.Common.IActivated.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

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

            this.Subscribe(this.OnCanExecuteChanged);
            this.IsActive = true;
            this.RaiseCanExecuteChanged();
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

            this.Unsubscribe(this.OnCanExecuteChanged);
            this.IsActive = false;
            this.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanExecute(T parameter) => this.IsActive && base.CanExecute(parameter);

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public override void RaiseCanExecuteChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        ///     Called when [is active changed].
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        protected virtual void OnIsActiveChanged(bool value) => this.IsActiveChanged.Raise(value);

        /// <summary>
        ///     Subscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected void Subscribe(EventHandler value)
        {
            if (this.HasCanExecute)
            {
                return;
            }

            CommandManager.RequerySuggested += value;
        }

        /// <summary>
        ///     Unsubscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected void Unsubscribe(EventHandler value)
        {
            if (this.HasCanExecute)
            {
                return;
            }

            CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnCanExecuteChanged(object sender, EventArgs e) => this.RaiseCanExecuteChanged();
    }
}