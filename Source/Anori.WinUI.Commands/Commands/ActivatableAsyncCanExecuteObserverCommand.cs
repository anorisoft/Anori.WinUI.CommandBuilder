// -----------------------------------------------------------------------
// <copyright file="ActivatableAsyncCanExecuteObserverCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using CanExecuteChangedTests;

    using JetBrains.Annotations;

    /// <summary>
    ///     Activatable Async CanExecute Observer Command.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Commands.AsyncCommandBase" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IActivatableAsyncCommand" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteChangedObserver" />
    /// <seealso cref="System.IDisposable" />
    public sealed class ActivatableAsyncCanExecuteObserverCommand : AsyncCommandBase,
                                                                    IActivatableAsyncCommand,
                                                                    ICanExecuteChangedObserver,
                                                                    IDisposable
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
        /// Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observer
        /// or
        /// observer</exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
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
        /// Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="error">The error.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
            [NotNull] Action<Exception> error)
            : this(execute, false, error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="error">The error.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
            bool autoActivate,
            [NotNull] Action<Exception> error)
            : base(execute, error)
        {
            if (autoActivate)
            {
                this.Activate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">canExecuteSubject
        /// or
        /// observers</exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
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
        /// Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecuteSubject, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject)
            : this(execute, false, canExecuteSubject)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observers
        ///     or
        ///     observers
        /// </exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
            [NotNull] Func<bool> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecute, observers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers</exception>
        public ActivatableAsyncCanExecuteObserverCommand(
            [NotNull] Func<Task> execute,
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
        /// Finalizes an instance of the <see cref="ActivatableAsyncCanExecuteObserverCommand" /> class.
        /// </summary>
        ~ActivatableAsyncCanExecuteObserverCommand() => this.Dispose(false);

        /// <summary>
        /// Notifies that the value for <see cref="P:Anori.WinUI.Common.IActivated.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value>
        ///   <see langword="true" /> if the object is active; otherwise <see langword="false" />.
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
        /// Activates this instance.
        /// </summary>
        /// <returns>The Self.</returns>
        public ActivatableAsyncCanExecuteObserverCommand Activate()
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
        /// Deactivates this instance.
        /// </summary>
        /// <returns>The Self.</returns>
        public ActivatableAsyncCanExecuteObserverCommand Deactivate()
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
        ///     Called when [can execute changed].
        /// </summary>
        public void RaisePropertyChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Raises the can execute changed.
        /// </summary>
        public override void RaiseCanExecuteChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
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

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommand IActivatable<IActivatableAsyncCommand>.Activate() => this.Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommand IActivatable<IActivatableAsyncCommand>.Deactivate() => this.Deactivate();
    }
}