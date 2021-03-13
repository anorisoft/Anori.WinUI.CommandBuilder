// -----------------------------------------------------------------------
// <copyright file="ActivatableCanExecuteObserverCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

[assembly:
    InternalsVisibleTo(
        "Anori.WinUI.CanExecuteChangedTests, "
        + "PublicKey=0024000004800000940000000602000000240000525341310004000001000100a520658730454fb71a447c87dcb713412746dd0b04a1a1afea4067c991bc260f965eb7481148266358fc635ca8395b78375f4cf69097188ab3cb2c27a52d1812872edd13157ed84b651f0462accdb31b65dfc4a3522c8ada5c895c24738b342f759ac7ad33086e44a631a8884e1e6eb526e7e4e7170a52b723fe3c0ddb55b3c2")]
[assembly:
    InternalsVisibleTo(
        "Anori.WinUI.Commands.Tests, "
        + "PublicKey=0024000004800000940000000602000000240000525341310004000001000100a520658730454fb71a447c87dcb713412746dd0b04a1a1afea4067c991bc260f965eb7481148266358fc635ca8395b78375f4cf69097188ab3cb2c27a52d1812872edd13157ed84b651f0462accdb31b65dfc4a3522c8ada5c895c24738b342f759ac7ad33086e44a631a8884e1e6eb526e7e4e7170a52b723fe3c0ddb55b3c2")]

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Collections.Generic;

    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    ///     Activatable CanExecute Observer Command with generic parameter.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Commands.SyncCommandBase{T}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IActivatableSyncCommand{T}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteChangedObserver" />
    /// <seealso cref="System.IDisposable" />
    internal sealed class ActivatableCanExecuteObserverCommand<T> : SyncCommandBase<T>,
                                                                    IActivatableSyncCommand<T>,
                                                                    ICanExecuteChangedObserver,
                                                                    IDisposable
    {
        /// <summary>
        ///     The observers.
        /// </summary>
        private readonly List<ICanExecuteChangedSubjectBase> observers = new List<ICanExecuteChangedSubjectBase>();

        /// <summary>
        ///     The is active.
        /// </summary>
        private bool isActive;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers is null.</exception>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
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
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observer
        ///     or
        ///     observer is null.
        /// </exception>
        /// <exception cref="ArgumentException">propertyObserver is null.</exception>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            bool autoActivate,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : base(execute, canExecuteSubject)
        {
            if (canExecuteSubject == null)
            {
                throw new ArgumentNullException(nameof(observers));
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
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecuteSubject, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] ICanExecuteSubject canExecuteSubject)
            : this(execute, false, canExecuteSubject)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] Predicate<T> canExecute,
            [NotNull] [ItemNotNull] params ICanExecuteChangedSubject[] observers)
            : this(execute, false, canExecute, observers)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="autoActivate">if set to <c>true</c> [automatic activate].</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observers
        ///     or
        ///     observers is null.
        /// </exception>
        public ActivatableCanExecuteObserverCommand(
            [NotNull] Action<T> execute,
            bool autoActivate,
            [NotNull] Predicate<T> canExecute,
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
        ///     Finalizes an instance of the <see cref="ActivatableCanExecuteObserverCommand{T}" /> class.
        /// </summary>
        ~ActivatableCanExecuteObserverCommand() => this.Dispose(false);

        /// <summary>
        ///     Notifies that the value for <see cref="P:Anori.WinUI.Common.IActivated.IsActive" /> property has changed.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Gets a value indicating whether the object is active.
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
                this.CanExecuteChanged.RaiseEmpty(this);
            }
        }

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        /// <returns>Activatable CanExecute Observer Command.</returns>
        public ActivatableCanExecuteObserverCommand<T> Activate()
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
        /// <returns>Activatable CanExecute Observer Command.</returns>
        public ActivatableCanExecuteObserverCommand<T> Deactivate()
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
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaisePropertyChanged() => this.CanExecuteChanged.RaiseEmpty(this);

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        /// <returns>Activatable Sync Command.</returns>
        IActivatableSyncCommand<T> IActivatable<IActivatableSyncCommand<T>>.Activate() => this.Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        /// <returns>Activatable Sync Command.</returns>
        IActivatableSyncCommand<T> IActivatable<IActivatableSyncCommand<T>>.Deactivate() => this.Deactivate();

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
    }
}