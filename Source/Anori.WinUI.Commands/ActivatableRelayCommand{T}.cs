// -----------------------------------------------------------------------
// <copyright file="ActivatableRelayCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows.Input;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Common;
using CanExecuteChangedTests;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands
{
    /// <summary>
    ///     A Command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class ActivatableRelayCommand<T> : SyncCommandBase<T>, IActivatable
    {
        /// <summary>
        ///     The is active
        /// </summary>
        private bool isActive;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <inheritdoc />
        public ActivatableRelayCommand([NotNull] Action<T> execute)
            : base(execute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public ActivatableRelayCommand([NotNull] Action<T> execute, [NotNull] Predicate<T> canExecute)
            : base(execute, canExecute)
        {
        }

        /// <summary>
        ///     Notifies that the value for <see cref="P:Anori.WinUI.Common.IActivated.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

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
                this.IsActiveChanged.Raise(this, value);
                this.CanExecuteChanged.RaiseEmpty(this);
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
        ///     Subscribes the specified value.
        /// </summary>
        protected void Subscribe()
        {
            if (this.HasCanExecute)
            {
                CommandManager.RequerySuggested += this.OnCanExecuteChanged;
            }
        }

        /// <summary>
        ///     Unsubscribes the specified value.
        /// </summary>
        protected void Unsubscribe()
        {
            if (this.HasCanExecute)
            {
                CommandManager.RequerySuggested -= this.OnCanExecuteChanged;
            }
        }

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void OnCanExecuteChanged(object sender, EventArgs e) =>
            this.Dispatch(me => me.CanExecuteChanged.Raise(me, e));
    }
}