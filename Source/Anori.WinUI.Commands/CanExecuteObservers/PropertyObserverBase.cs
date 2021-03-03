using System;
using System.Collections.Generic;
using Anori.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ICanExecuteChangedSubject" />
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{T}" />
    public abstract class PropertyObserverBase : ICanExecuteChangedSubjectBase
    {
        /// <summary>
        ///     The observables
        /// </summary>
        [NotNull] protected readonly IDictionary<ICanExecuteChangedObserver, Action> observables =
            new Dictionary<ICanExecuteChangedObserver, Action>();

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
        ///     The observerBase
        /// </summary>
        [NotNull]
        protected virtual ExpressionObservers.Observers.PropertyObserverBase Observer { get; set; } = null!;

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

            if (this.observables.TryGetValue(observer, out _))
            {
                return;
            }

            void Handler()
            {
                observer.RaisePropertyChanged();
            }

            this.observables.Add(observer, Handler);
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

            if (!this.observables.TryGetValue(observer, out var handler))
            {
                return;
            }

            this.Update -= handler;
            this.observables.Remove(observer);
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

            foreach (var handler in this.observables.Values)
            {
                this.Update -= handler;
            }

            foreach (var updateable in this.observables.Keys)
            {
                updateable.RaisePropertyChanged();
            }

            this.observables.Clear();
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