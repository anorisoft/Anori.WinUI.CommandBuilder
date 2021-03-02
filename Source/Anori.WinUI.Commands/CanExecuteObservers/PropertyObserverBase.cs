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
        protected virtual Anori.ExpressionObservers.Observers.PropertyObserverBase Observer { get; set; } = null!;

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

        ///// <summary>
        /////     Determines whether the specified objects are equal.
        ///// </summary>
        ///// <param name="x">The first object of type <paramref name="x" /> to compare.</param>
        ///// <param name="y">The second object of type <paramref name="y" /> to compare.</param>
        ///// <returns>
        /////     <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.
        ///// </returns>
        //public bool Equals(T x, T y)
        //{
        //    if (ReferenceEquals(x, y))
        //    {
        //        return true;
        //    }

        //    if (x is null)
        //    {
        //        return false;
        //    }

        //    if (y is null)
        //    {
        //        return false;
        //    }

        //    if (x.GetType() != y.GetType())
        //    {
        //        return false;
        //    }

        //    return x.Equals(y);
        //}

        ///// <summary>
        /////     Indicates whether the current object is equal to another object of the same type.
        ///// </summary>
        ///// <param name="other">An object to compare with this object.</param>
        ///// <returns>
        /////     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
        /////     <see langword="false" />.
        ///// </returns>
        //public bool Equals(T other)
        //{
        //    if (other is null)
        //    {
        //        return false;
        //    }

        //    if (ReferenceEquals(this, other))
        //    {
        //        return true;
        //    }

        //    return this.Observer.Equals(other.Observer);
        //}

        ///// <summary>
        /////     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        ///// </summary>
        ///// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        ///// <returns>
        /////     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        ///// </returns>
        //public override bool Equals(object obj)
        //{
        //    if (obj is null)
        //    {
        //        return false;
        //    }

        //    if (ReferenceEquals(this, obj))
        //    {
        //        return true;
        //    }

        //    if (obj.GetType() != this.GetType())
        //    {
        //        return false;
        //    }

        //    return this.Equals((T)obj);
        //}

        ///// <summary>
        /////     Returns a hash code for this instance.
        ///// </summary>
        ///// <param name="obj">The object.</param>
        ///// <returns>
        /////     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        ///// </returns>
        //public int GetHashCode(T obj)
        //{
        //    unchecked
        //    {
        //        var hashCode = obj.observables.GetHashCode();
        //        hashCode = (hashCode * 397) ^ obj.Observer.GetHashCode();
        //        hashCode = (hashCode * 397)
        //                   ^ (obj.PropertyExpression != null ? obj.PropertyExpression.GetHashCode() : 0);
        //        return hashCode;
        //    }
        //}

        ///// <summary>
        /////     Returns a hash code for this instance.
        ///// </summary>
        ///// <returns>
        /////     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        ///// </returns>
        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        return (this.observables.GetHashCode() * 397) ^ this.Observer.GetHashCode();
        //    }
        //}

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
        protected void Subscribe() => this.Observer.Subscribe();

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        protected void Unsubscribe() => this.Observer.Unsubscribe();
    }
}