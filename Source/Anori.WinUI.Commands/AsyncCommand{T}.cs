// -----------------------------------------------------------------------
// <copyright file="AsyncCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Common;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands
{
    public class AsyncCommand<T> : AsyncCommandBase<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="error">The error handler.</param>
        public AsyncCommand(
            [NotNull] Func<T, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] Action<Exception> error)
            : base(execute, canExecute, error)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public AsyncCommand([NotNull] Func<T, Task> execute, [NotNull] Predicate<T> canExecute)
            : base(execute, canExecute)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="error">The error.</param>
        public AsyncCommand([NotNull] Func<T, Task> execute, [NotNull] Action<Exception> error)
            : base(execute, error)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public AsyncCommand([NotNull] Func<T, Task> execute)
            : base(execute)
        {
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public override void RaiseCanExecuteChanged() => this.CanExecuteChanged.RaiseEmpty(this);
    }
}