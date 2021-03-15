// -----------------------------------------------------------------------
// <copyright file="AsyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.CanExecuteChangedTests
{
    using System;
    using System.Threading.Tasks;

    using Anori.Common;
    using Anori.Extensions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The error handler
        /// </summary>
        private readonly Action<Exception> error;

        /// <summary>
        ///     The execute
        /// </summary>
        private readonly Func<T, Task> execute;

        /// <summary>
        ///     The is executing
        /// </summary>
        private bool isExecuting;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="canExecute">The execute.</param>
        /// <param name="error">The can execute.</param>
        /// <param name="error">The error handler.</param>
        public AsyncCommand(
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
        public AsyncCommand([NotNull] Func<T, Task> execute, [NotNull] Predicate<T> canExecute)
            : this(execute)
        {
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <exception cref="ArgumentNullException">error</exception>
        public AsyncCommand([NotNull] Func<T, Task> execute, [NotNull] Action<Exception> error)
            : this(execute)
        {
            this.error = error ?? throw new ArgumentNullException(nameof(error));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public AsyncCommand([NotNull] Func<T, Task> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return !this.isExecuting && (this.canExecute?.Invoke(parameter) ?? true);
        }

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

        #region Explicit implementations

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
        bool System.Windows.Input.ICommand.CanExecute(object parameter) => this.CanExecute((T)parameter);

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        void System.Windows.Input.ICommand.Execute(object parameter) =>
            this.ExecuteAsync((T)parameter).FireAndForgetSafeAsync(this.error);

        #endregion Explicit implementations
    }
}