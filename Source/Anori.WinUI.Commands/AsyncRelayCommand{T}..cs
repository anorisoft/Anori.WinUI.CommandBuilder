// -----------------------------------------------------------------------
// <copyright file="AsyncRelayCommand{T}..cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Common;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands
{
    public class AsyncRelayCommand<T> : AsyncCommandBase<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="error">The error handler.</param>
        public AsyncRelayCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] Action<Exception> error)
            : base(execute, canExecute, error)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public AsyncRelayCommand([NotNull] Func<T, Task> execute, [NotNull] Predicate<T> canExecute)
            : base(execute, canExecute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="error">The error handler.</param>
        public AsyncRelayCommand([NotNull] Func<T, Task> execute, [NotNull] Action<Exception> error)
            : base(execute, error)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public AsyncRelayCommand([NotNull] Func<T, Task> execute)
            : base(execute)
        {
        }

        /// <summary>
        ///     Occurs when [can execute changed internal].
        /// </summary>
#pragma warning disable S3264 // Events should be invoked
        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public sealed override event EventHandler CanExecuteChanged
        {
            add => this.Subscribe(value);
            remove => this.Unsubscribe(value);
        }

        private event EventHandler CanExecuteChangedInternal;

#pragma warning restore S3264 // Events should be invoked

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public override void RaiseCanExecuteChanged() => this.CanExecuteChangedInternal.RaiseEmpty(this);
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