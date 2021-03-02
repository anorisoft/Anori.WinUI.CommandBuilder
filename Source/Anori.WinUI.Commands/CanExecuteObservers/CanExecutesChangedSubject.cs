// -----------------------------------------------------------------------
// <copyright file="CanExecutesChangedSubject.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using Anorisoft.WinUI.Commands.Interfaces;
    using Anorisoft.WinUI.Common;

    using JetBrains.Annotations;

    public class CanExecutesChangedSubject<T> : ICanExecuteChangedSubject
    {
        /// <summary>
        ///     The observables
        /// </summary>
        private readonly IDictionary<ICanExecuteChangedObserver, Action> observables =
            new Dictionary<ICanExecuteChangedObserver, Action>();

        /// <summary>
        ///     The observed properties expressions
        /// </summary>
        private readonly IDictionary<string, PropertyObserver> observedPropertiesExpressions =
            new Dictionary<string, PropertyObserver>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{T}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        public CanExecutesChangedSubject(Expression<Func<T>> propertyExpression)
        {
            this.AddExpression(propertyExpression);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{T}" /> class.
        /// </summary>
        public CanExecutesChangedSubject()
        {
        }

        /// <summary>
        ///     Adds the specified changedObserver.
        /// </summary>
        /// <param name="changedObserver">The changedObserver.</param>
        public void Add([NotNull] ICanExecuteChangedObserver changedObserver)
        {
            if (changedObserver == null)
            {
                throw new ArgumentNullException(nameof(changedObserver));
            }

            if (this.observables.TryGetValue(changedObserver, out _))
            {
                return;
            }

            void Handler() => changedObserver.RaisePropertyChanged();
            this.observables.Add(changedObserver, Handler);
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
        ///     Removes the specified changedObserver.
        /// </summary>
        /// <param name="changedObserver">The changedObserver.</param>
        public void Remove([NotNull] ICanExecuteChangedObserver changedObserver)
        {
            if (changedObserver == null)
            {
                throw new ArgumentNullException(nameof(changedObserver));
            }

            if (!this.observables.TryGetValue(changedObserver, out var handler))
            {
                return;
            }

            this.Update -= handler;
            this.observables.Remove(changedObserver);
            changedObserver.RaisePropertyChanged();
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        private event Action Update;

        /// <summary>
        ///     Adds the expression.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="System.ArgumentException">propertyExpression</exception>
        public void AddExpression(Expression<Func<T>> propertyExpression)
        {
            if (this.observedPropertiesExpressions.TryGetValue(propertyExpression.ToString(), out _))
            {
                throw new ArgumentException(
                    $"{propertyExpression} is already being observed.",
                    nameof(propertyExpression));
            }

            var observer = PropertyObserver.Observes(propertyExpression, () => this.Update.Raise());
            this.observedPropertiesExpressions.Add(propertyExpression.ToString(), observer);
            observer.Subscribe();
        }

        /// <summary>
        ///     Adds the expression.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentException">propertyExpression</exception>
        public void AddExpression<TOwner>(TOwner owner, Expression<Func<TOwner, T>> propertyExpression)
            where TOwner : INotifyPropertyChanged

        {
            if (this.observedPropertiesExpressions.TryGetValue(
                owner.GetHashCode() + propertyExpression.ToString(),
                out _))
            {
                throw new ArgumentException(
                    $"{propertyExpression} is already being observed.",
                    nameof(propertyExpression));
            }

            var observer = PropertyObserver.Observes(owner, propertyExpression, () => this.Update.Raise());
            this.observedPropertiesExpressions.Add(owner.GetHashCode() + propertyExpression.ToString(), observer);
            observer.Subscribe();
        }

        /// <summary>
        ///     Removes the expression.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public void RemoveExpression<TOwner>(TOwner owner, Expression<Func<T>> propertyExpression)
        {
            if (!this.observedPropertiesExpressions.TryGetValue(
                    owner.GetHashCode() + propertyExpression.ToString(),
                    out var observer))
            {
                return;
            }

            this.observedPropertiesExpressions.Remove(owner.GetHashCode() + propertyExpression.ToString());
            observer.Unsubscribe();
        }

        /// <summary>
        ///     Removes the expression.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        public void RemoveExpression(Expression<Func<T>> propertyExpression)
        {
            if (!this.observedPropertiesExpressions.TryGetValue(propertyExpression.ToString(), out var observer))
            {
                return;
            }

            this.observedPropertiesExpressions.Remove(propertyExpression.ToString());
            observer.Unsubscribe();
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
    }
}