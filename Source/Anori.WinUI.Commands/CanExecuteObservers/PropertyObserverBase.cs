// -----------------------------------------------------------------------
// <copyright file="PropertyObserverBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.Collections.Generic;

    using Anori.WinUI.Commands.Interfaces;

    using JetBrains.Annotations;

    /// <summary>
    ///     Property Observer Base.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteChangedSubjectBase" />
    /// <seealso cref="ICanExecuteChangedSubject" />
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{T}" />
    internal abstract class PropertyObserverBase : ICanExecuteChangedSubjectBase
    {
        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        [CanBeNull]
        public abstract event Action Update;

        /// <summary>
        ///     Gets or sets the property expression.
        /// </summary>
        /// <value>
        ///     The property expression.
        /// </value>
        public string PropertyExpression { get; protected set; } = null!;

        /// <summary>
        ///     Gets or sets the observer.
        /// </summary>
        /// <value>
        ///     The observer.
        /// </value>
        [NotNull]
        protected virtual ExpressionObservers.Observers.PropertyObserverBase Observer { get; set; } = null!;

        /// <summary>
        ///     Gets the observables.
        /// </summary>
        /// <value>
        ///     The observables.
        /// </value>
        [NotNull]
        protected IDictionary<ICanExecuteChangedObserver, Action> Observables { get; } =
            new Dictionary<ICanExecuteChangedObserver, Action>();

        /// <summary>
        ///     Adds the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void Add(ICanExecuteChangedObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (this.Observables.TryGetValue(observer, out _))
            {
                return;
            }

            void Handler()
            {
                observer.RaisePropertyChanged();
            }

            this.Observables.Add(observer, Handler);
            this.Update += Handler;
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
        ///     Removes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void Remove(ICanExecuteChangedObserver observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }

            if (!this.Observables.TryGetValue(observer, out var handler))
            {
                return;
            }

            this.Update -= handler;
            this.Observables.Remove(observer);
            observer.RaisePropertyChanged();
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            this.Unsubscribe();

            foreach (var handler in this.Observables.Values)
            {
                this.Update -= handler;
            }

            foreach (var updateable in this.Observables.Keys)
            {
                updateable.RaisePropertyChanged();
            }

            this.Observables.Clear();
        }

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        protected void Subscribe() => this.Observer.Subscribe(true);

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        protected void Unsubscribe() => this.Observer.Unsubscribe();
    }
}