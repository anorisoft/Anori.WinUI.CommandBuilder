// -----------------------------------------------------------------------
// <copyright file="DirectAndRelayCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Windows.Input;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Common;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands
{
    /// <summary>
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class DirectAndRelayCommand<T> : SyncCommandBase<T>, IRaiseCanExecuteCommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectAndRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <inheritdoc />
        public DirectAndRelayCommand([NotNull] Action<T> execute)
            : base(execute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DirectAndRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public DirectAndRelayCommand([NotNull] Action<T> execute, [NotNull] Predicate<T> canExecute)
            : base(execute, canExecute)
        {
        }

        public sealed override event EventHandler CanExecuteChanged
        {
            add => this.Subscribe(value);
            remove => this.Unsubscribe(value);
        }

        /// <summary>
        ///     Occurs when [can execute changed internal].
        /// </summary>
        private event EventHandler CanExecuteChangedInternal;

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChangedInternal.RaiseEmpty(this);
        }
        /// <summary>
        ///     Subscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Subscribe(EventHandler value)
        {
            if (!this.HasCanExecute)
            {
                return;
            }

            CommandManager.RequerySuggested += value;
            this.CanExecuteChangedInternal += value;
        }

        /// <summary>
        ///     Unsubscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Unsubscribe(EventHandler value)
        {
            if (!this.HasCanExecute)
            {
                return;
            }

            CommandManager.RequerySuggested -= value;
            this.CanExecuteChangedInternal -= value;
        }
    }
}