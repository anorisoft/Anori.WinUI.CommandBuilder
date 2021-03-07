// -----------------------------------------------------------------------
// <copyright file="ActivatableConcurrencyAsyncCanExecuteObserverCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    internal sealed class ActivatableConcurrencyAsyncCanExecuteObserverCommand : ConcurrencyAsyncCommandBase,
        IActivatableConcurrencyAsyncCommand,
        ICanExecuteChangedObserver
    {
        /// <summary>
        ///     The observers
        /// </summary>
        private readonly List<ICanExecuteChangedSubjectBase> observers = new List<ICanExecuteChangedSubjectBase>();

        /// <summary>
        ///     The is active
        /// </summary>
        private bool isActive;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observer
        ///     or
        ///     observer
        /// </exception>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            bool autoActivate,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddIfNotContains(observers);

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableConcurrencyAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers</exception>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            bool autoActivate,
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

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     canExecuteSubject
        ///     or
        ///     observers
        /// </exception>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            bool autoActivate,
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

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableConcurrencyAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     canExecuteSubject
        ///     or
        ///     observers
        /// </exception>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            bool autoActivate,
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

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            [CanBeNull] Func<Task> completed,
            [CanBeNull] Func<Exception, Task> error,
            [CanBeNull] Func<Task> cancel)
            : this(execute, false, completed, error, cancel)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [CanBeNull] Func<Task> completed,
            [CanBeNull] Func<Exception, Task> error,
            [CanBeNull] Func<Task> cancel,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecuteSubject, completed, error, cancel, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject)
            : this(execute, false, canExecuteSubject)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableConcurrencyAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [CanBeNull] Func<Task> completed,
            [CanBeNull] Func<Exception, Task> error,
            [CanBeNull] Func<Task> cancel)
            : this(execute, false, canExecuteSubject, completed, error, cancel)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observers
        ///     or
        ///     observers
        /// </exception>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            [NotNull] Func<bool> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecute, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableConcurrencyAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            [NotNull] Func<bool> canExecute,
            [CanBeNull] Func<Task> completed,
            [CanBeNull] Func<Exception, Task> error,
            [CanBeNull] Func<Task> cancel,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecute, completed, error, cancel, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers</exception>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            bool autoActivate,
            [NotNull] Func<bool> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecute)
        {
            if (observers == null)
            {
                throw new ArgumentNullException(nameof(observers));
            }

            this.observers.AddIfNotContains(observers);

            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableConcurrencyAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="completed">The completed.</param>
        /// <param name="error">The error.</param>
        /// <param name="cancel">The cancel.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers</exception>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand(
            [NotNull] Func<CancellationToken, Task> execute,
            bool autoActivate,
            [NotNull] Func<bool> canExecute,
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

            if (autoActivate)
            {
                this.Activate();
            }
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
        ///     Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if the object is active; otherwise <see langword="false" />.
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
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand IActivatable<IActivatableConcurrencyAsyncCommand>.Activate() =>
            this.Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand IActivatable<IActivatableConcurrencyAsyncCommand>.Deactivate() =>
            this.Deactivate();

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaisePropertyChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand Activate()
        {
            if (this.IsActive)
            {
                return this;
            }

            this.Subscribe();
            this.IsActive = true;
            return this;
        }

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        public ActivatableConcurrencyAsyncCanExecuteObserverCommand Deactivate()
        {
            if (!this.IsActive)
            {
                return this;
            }

            this.Unsubscribe();
            this.IsActive = false;
            return this;
        }

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