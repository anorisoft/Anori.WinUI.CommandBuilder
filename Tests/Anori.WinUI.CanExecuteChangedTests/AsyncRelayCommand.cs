// -----------------------------------------------------------------------
// <copyright file="AsyncRelayCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Common;

namespace CanExecuteChangedTests
{
    using JetBrains.Annotations;

    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class AsyncRelayCommand : IAsyncCommand
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        ///     The error handler
        /// </summary>
        private readonly Action<Exception> error;

        /// <summary>
        ///     The execute
        /// </summary>
        private readonly Func<Task> execute;

        /// <summary>
        ///     The is executing
        /// </summary>
        private bool isExecuting;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="errorHandler">The error handler.</param>
        public AsyncRelayCommand(
            [NotNull] Func<Task> execute,
            [NotNull] Func<bool> canExecute,
            [NotNull] Action<Exception> error)
            : this(execute, canExecute)
        {
            this.error = error ?? throw new ArgumentNullException(nameof(error));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public AsyncRelayCommand([NotNull] Func<Task> execute, [NotNull] Func<bool> canExecute)
            : this(execute)
        {
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public AsyncRelayCommand([NotNull] Func<Task> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncRelayCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="errorHandler">The error handler.</param>
        /// <exception cref="ArgumentNullException">
        ///     execute
        ///     or
        ///     errorHandler
        /// </exception>
        public AsyncRelayCommand([NotNull] Func<Task> execute, [NotNull] Action<Exception> error)
            : this(execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.error = error ?? throw new ArgumentNullException(nameof(error));
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute() => !this.isExecuting && (this.canExecute?.Invoke() ?? true);

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
        bool System.Windows.Input.ICommand.CanExecute(object parameter) => this.CanExecute();

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        void System.Windows.Input.ICommand.Execute(object parameter) =>
            this.ExecuteAsync().FireAndForgetSafeAsync(this.error);

        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        public async Task ExecuteAsync()
        {
            if (this.CanExecute())
            {
                try
                {
                    this.isExecuting = true;
                    await this.execute();
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
    }
}