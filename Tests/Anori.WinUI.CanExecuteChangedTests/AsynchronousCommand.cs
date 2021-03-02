// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WPF.Commands
{
    #region

    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Anori.WinUI.Common;

    #endregion

    /// <summary>
    ///     Asynchronous Relay Command
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    /// <seealso cref="System.IDisposable" />
    public class AsynchronousCommand : ICommand, IDisposable
    {
        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action action;

        /// <summary>
        ///     The cancel
        /// </summary>
        private readonly Action cancel;

        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Func<bool> canExecute;

        /// <summary>
        ///     The completed
        /// </summary>
        private readonly Action<object> completed;

        /// <summary>
        ///     The error
        /// </summary>
        private readonly Action<Exception> error;

        /// <summary>
        ///     The cancellation token source
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        ///     The task
        /// </summary>
        private Task task;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsynchronousCommand" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        public AsynchronousCommand(
            Action action,
            Func<bool> canExecute = null,
            Action<object> completed = null,
            Action<Exception> error = null,
            Action cancel = null)
        {
            this.action = action;
            this.canExecute = canExecute;
            this.completed = completed;
            this.error = error;
            this.cancel = cancel;
        }

        /// <summary>
        ///     Called when [ran to completion].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="obj">The object.</param>
        private void OnRanToCompletion(Task t, object obj)
        {
            this.completed?.Invoke(obj);
        }

        #region ICommand Members

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        ///     Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            this.cancellationTokenSource?.Cancel();
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
        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null && !this.canExecute())
            {
                return false;
            }

            if (this.task == null)
            {
                return true;
            }

            if (this.task.IsFinished())
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        public void Execute(object parameter)
        {
            this.task?.Dispose();
            this.cancellationTokenSource = new CancellationTokenSource();
            this.task = new TaskFactory().StartNew(this.OnAction, this.cancellationTokenSource.Token)
                .ContinueWith(this.OnRanToCompletion, TaskContinuationOptions.OnlyOnRanToCompletion)
                .ContinueWith(this.OnFaulted, TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith(this.OnCanceled, TaskContinuationOptions.OnlyOnCanceled)
                .ContinueWith(this.OnFinally);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.cancellationTokenSource != null)
                {
                    this.cancellationTokenSource.Cancel();
                    this.cancellationTokenSource.Dispose();
                    this.cancellationTokenSource = null;
                }

                if (this.task != null)
                {
                    this.task.Dispose();
                    this.task = null;
                }
            }
        }

        /// <summary>
        ///     Called when [action].
        /// </summary>
        /// <param name="obj">The object.</param>
        private void OnAction(object obj)
        {
            this.action();
        }

        /// <summary>
        ///     Called when [canceled].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="obj">The object.</param>
        private void OnCanceled(Task t, object obj)
        {
            this.cancel?.Invoke();
        }

        /// <summary>
        ///     Called when [faulted].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="obj">The object.</param>
        private void OnFaulted(Task t, object obj)
        {
            this.error?.Invoke(t.Exception);
        }

        private void OnFinally(Task obj)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion

        #region IDisposable Members

        #endregion
    }
}