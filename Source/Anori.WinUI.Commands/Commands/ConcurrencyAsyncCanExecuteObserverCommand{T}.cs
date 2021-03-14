// -----------------------------------------------------------------------
// <copyright file="ConcurrencyAsyncCanExecuteObserverCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Anori.Extensions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    /// The Concurrency Asynchronous Can Execute Observer Command class.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Commands.ConcurrencyAsyncCommandBase{T}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteChangedObserver" />
    internal sealed class ConcurrencyAsyncCanExecuteObserverCommand<T> : ConcurrencyAsyncCommandBase<T>,
                                                                         ICanExecuteChangedObserver
    {
        /// <summary>
        ///     The observers.
        /// </summary>
        private readonly List<ICanExecuteChangedSubjectBase> observers = new List<ICanExecuteChangedSubjectBase>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyAsyncCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observer
        /// or
        /// observer is null.</exception>
        public ConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, CancellationToken, Task> execute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddIfNotContains(observers);

            this.Subscribe();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrencyAsyncCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers is null.</exception>
        public ConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, CancellationToken, Task> execute,
            [CanBeNull] Func<Task> completed,
            [CanBeNull] Func<Exception, Task> error,
            [CanBeNull] Func<Task> cancel,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, completed, error, cancel)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddIfNotContains(observers);

            this.Subscribe();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyAsyncCanExecuteObserverCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">canExecuteSubject
        /// or
        /// observers is null.</exception>
        public ConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, CancellationToken, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecuteSubject)
        {
            if (canExecuteSubject == null)
            {
                throw new ArgumentNullException(nameof(canExecuteSubject));
            }

            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.Add(canExecuteSubject);

            this.observers.AddIfNotContains(observers);

            this.Subscribe();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrencyAsyncCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     canExecuteSubject
        ///     or
        ///     observers is null.
        /// </exception>
        public ConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, CancellationToken, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [CanBeNull] Func<Task> completed,
            [CanBeNull] Func<Exception, Task> error,
            [CanBeNull] Func<Task> cancel,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecuteSubject, completed, error, cancel)
        {
            if (canExecuteSubject == null)
            {
                throw new ArgumentNullException(nameof(canExecuteSubject));
            }

            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.Add(canExecuteSubject);

            this.observers.AddIfNotContains(observers);

            this.Subscribe();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcurrencyAsyncCanExecuteObserverCommand{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers is null.</exception>
        public ConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, CancellationToken, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecute)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddIfNotContains(observers);

            this.Subscribe();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrencyAsyncCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers is null.</exception>
        public ConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<T, CancellationToken, Task> execute,
            [NotNull] Predicate<T> canExecute,
            [CanBeNull] Func<Task> completed,
            [CanBeNull] Func<Exception, Task> error,
            [CanBeNull] Func<Task> cancel,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecute, completed, error, cancel)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddIfNotContains(observers);

            this.Subscribe();
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaisePropertyChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        ///     Raises the can execute command.
        /// </summary>
        public override void RaiseCanExecuteCommand() => this.RaisePropertyChanged();

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.Unsubscribe();
            }
        }

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        private void Subscribe() => this.observers.ForEach(observer => observer.Add(this));

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        private void Unsubscribe() => this.observers.ForEach(observer => observer.Remove(this));
    }
}