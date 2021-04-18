// -----------------------------------------------------------------------
// <copyright file="PropertyObserverBase{TResult}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;

    using Anori.ExpressionObservers.Interfaces;

    /// <summary>
    /// Property Observer Base.
    /// </summary>
    /// <typeparam name="TPropertyObserver">The type of the property observer.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TPropertyObserver}" />
    internal abstract class PropertyObserverBase<TPropertyObserver, TResult> : PropertyObserverBase<TPropertyObserver>,
                                                                             IEquatable<PropertyObserverBase<TPropertyObserver,TResult>>
    {
        /// <summary>
        ///     Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref name="x" /> to compare.</param>
        /// <param name="y">The second object of type <paramref name="y" /> to compare.</param>
        /// <returns>
        ///     <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(PropertyObserverBase<TResult> x, PropertyObserverBase<TResult> y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null)
            {
                return false;
            }

            if (y is null)
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Equals(y);
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public bool Equals(PropertyObserverBase<TPropertyObserver, TResult> other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.PropertyExpression != other.PropertyExpression)
            {
                return false;
            }

            return this.Observer.Equals(other.Observer);
        }

       /// <summary>
        ///     Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((PropertyObserverBase<TPropertyObserver, TResult>)obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(PropertyObserverBase<TPropertyObserver, TResult> obj)
        {
            unchecked
            {
                var hashCode = obj.Observables.GetHashCode();
                hashCode = (hashCode * 397) ^ obj.Observer.GetHashCode();
                hashCode = (hashCode * 397)
                           ^ (obj.PropertyExpression != null ? obj.PropertyExpression.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Observables.GetHashCode() * 397) ^ this.Observer.GetHashCode();
            }
        }
    }
}