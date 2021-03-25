// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserverCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Collections.Generic;

    using Anori.Extensions;
    using Anori.WinUI.Commands.Interfaces;

    using JetBrains.Annotations;

    /// <summary>
    ///     CanExecute Observer Command.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Commands.SyncCommandBase" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteChangedObserver" />
    /// <seealso cref="System.IDisposable" />
    internal sealed class CanExecuteObserverCommand : SyncCommandBase, ICanExecuteChangedObserver, IDisposable
    {
        /// <summary>
        ///     The observers.
        /// </summary>
        private readonly List<ICanExecuteChangedSubjectBase> observers = new List<ICanExecuteChangedSubjectBase>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     observer
        ///     or
        ///     observer is null.
        /// </exception>
        public CanExecuteObserverCommand(
            [NotNull] Action execute,
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
        ///     Initializes a new instance of the <see cref="CanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecuteSubject">The can execute subject.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">
        ///     canExecuteSubject
        ///     or
        ///     observers is null.
        /// </exception>
        public CanExecuteObserverCommand(
            [NotNull] Action execute,
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
        ///     Initializes a new instance of the <see cref="CanExecuteObserverCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="observers">The observers.</param>
        /// <exception cref="ArgumentNullException">observers is null.</exception>
        public CanExecuteObserverCommand(
            [NotNull] Action execute,
            [NotNull] Func<bool> canExecute,
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
        ///     Finalizes an instance of the <see cref="CanExecuteObserverCommand" /> class.
        /// </summary>
        ~CanExecuteObserverCommand() => this.Dispose(false);

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public override event EventHandler CanExecuteChanged;

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
        private void Subscribe() => this.observers.ForEach(observer => { observer.Add(this); });

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        private void Unsubscribe() => this.observers.ForEach(observer => observer.Remove(this));
    }
}