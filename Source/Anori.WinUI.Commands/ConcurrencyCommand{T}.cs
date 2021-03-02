// -----------------------------------------------------------------------
// <copyright file="ConcurrencyRelayCommand {T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Common;
using JetBrains.Annotations;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Anori.WinUI.Commands
{
    /// <summary>
    ///     Asynchronous Relay Command
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    /// <seealso cref="System.IDisposable" />
    public class ConcurrencyCommand<T> : CommandBase, ISyncCommand<T>, IDisposable
    {
        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action<T, CancellationToken> action;

        /// <summary>
        ///     The cancel
        /// </summary>
        private readonly Action cancel;

        /// <summary>
        ///     The can execute
        /// </summary>
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The completed
        /// </summary>
        private readonly Action<T> completed;

        /// <summary>
        ///     The error
        /// </summary>
        private readonly Action<Exception> error;

        /// <summary>
        ///     The finally task scheduler
        /// </summary>
        private readonly TaskScheduler finallyTaskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

        /// <summary>
        ///     The post actions task scheduler
        /// </summary>
        private readonly TaskScheduler postTaskScheduler = TaskScheduler.Default;

        /// <summary>
        ///     The task factory
        /// </summary>
        private readonly TaskFactory taskFactory = new TaskFactory();

        /// <summary>
        ///     The actions task scheduler
        /// </summary>
        private readonly TaskScheduler taskScheduler = TaskScheduler.Default;

        /// <summary>
        ///     The cancellation token source
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        ///     The task
        /// </summary>
        private Task task;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrencyRelayCommand" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        public ConcurrencyCommand(
            Action<T, CancellationToken> action,
            Predicate<T> canExecute = null,
            Action<T> completed = null,
            Action<Exception> error = null,
            Action cancel = null)
        {
            this.action = action;
            this.canExecute = canExecute;
            this.completed = completed;
            this.error = error;
            this.cancel = cancel;
            this.CancelCommand = new RelayCommand(this.Cancel, () => this.IsExecute);
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        ///     Gets or sets the cancel command.
        /// </summary>
        /// <value>
        ///     The cancel command.
        /// </value>
        public ISyncCommand CancelCommand { get; set; }

        /// <summary>
        ///     Gets the exception.
        /// </summary>
        /// <value>
        ///     The exception.
        /// </value>
        public Exception Exception { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether this instance is canceled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is canceled; otherwise, <c>false</c>.
        /// </value>
        public bool IsCanceled { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is execute.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is execute; otherwise, <c>false</c>.
        /// </value>
        public bool IsExecute { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has can execute.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has can execute; otherwise, <c>false</c>.
        /// </value>
        protected override bool HasCanExecute => this.canExecute != null;

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
        public bool CanExecute([CanBeNull] T parameter)
        {
            if (this.IsExecute)
            {
                return false;
            }

            if (this.canExecute != null && !this.canExecute(parameter))
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

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        public void Execute([CanBeNull] T parameter)
        {
            if (!this.CanExecute(parameter))
            {
                return;
            }

            try
            {
                this.OnBegin();
                this.task?.Dispose();
                this.cancellationTokenSource = new CancellationTokenSource();
                var token = this.cancellationTokenSource.Token;

                this.task = this.taskFactory
                    .StartNew(
                        p => this.OnAction((T)p, token),
                        parameter,
                        token,
                        TaskCreationOptions.DenyChildAttach,
                        this.taskScheduler)
                    .ContinueWith((t, p) => this.OnPostAction(t, (T)p), parameter, this.postTaskScheduler)
                    .ContinueWith(t => this.OnFinally(), this.finallyTaskScheduler);
            }
            catch (TaskCanceledException ex)
            {
                this.OnFinally();
                Debug.WriteLine(ex);
            }
            catch (AggregateException ex)
            {
                this.OnFinally();
                foreach (var e in ex.InnerExceptions)
                {
                    // Handle the custom exception.
                    if (e is TaskCanceledException)
                    {
                        Debug.WriteLine(e.Message);
                    }
                    // Rethrow any other exception.
                    else
                    {
                        throw e;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                this.OnFinally();
                throw;
            }
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

        /// <summary>
        ///     Cancels this instance.
        /// </summary>
        public void Cancel() => this.cancellationTokenSource?.Cancel();

        /// <summary>
        ///     Raises the can execute command.
        /// </summary>
        public void RaiseCanExecuteCommand() => this.Dispatch(CommandManager.InvalidateRequerySuggested);

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected sealed override bool CanExecute(object parameter) => this.CanExecute((T)parameter);

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
        ///     Handle the internal invocation of <see cref="ISyncCommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected sealed override void Execute(object parameter) => this.Execute((T)parameter);

        /// <summary>
        ///     Called when [action].
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="token">The token.</param>
        private void OnAction(T parameter, CancellationToken token) => this.action(parameter, token);

        /// <summary>
        ///     Called when [begin].
        /// </summary>
        private void OnBegin()
        {
            this.IsCanceled = false;
            this.HasErrors = false;
            this.Exception = null;
            this.IsExecute = true;
            this.RaiseCanExecuteCommand();
        }

        /// <summary>
        ///     Called when [canceled].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="obj">The object.</param>
        private void OnCanceled()
        {
            Debug.WriteLine("OnCanceled");
            this.IsCanceled = true;
            this.cancel.Raise();
        }

        /// <summary>
        ///     Called when [faulted].
        /// </summary>
        /// <param name="exception">The exception.</param>
        private void OnFaulted(Exception exception)
        {
            Debug.WriteLine("OnFaulted");
            this.HasErrors = true;
            this.Exception = exception;
            this.error.Raise(exception);
        }

        /// <summary>
        ///     Called when [finally].
        /// </summary>
        private void OnFinally()
        {
            Debug.WriteLine("OnFinally");
            this.IsExecute = false;
            this.RaiseCanExecuteCommand();
        }

        /// <summary>
        ///     Called when [post action].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="parameter">The parameter.</param>
        private void OnPostAction(Task t, T parameter)
        {
            switch (t.Status)
            {
                case TaskStatus.Canceled:
                    this.OnCanceled();
                    break;

                case TaskStatus.Faulted:
                    this.OnFaulted(t.Exception);
                    break;

                case TaskStatus.RanToCompletion:
                    this.OnRanToCompletion(parameter);
                    break;
            }
        }

        /// <summary>
        ///     Called when [ran to completion].
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="parameter">The parameter.</param>
        private void OnRanToCompletion(T parameter)
        {
            Debug.WriteLine("OnRanToCompletion");
            this.completed.Raise(parameter);
        }
    }
}