// -----------------------------------------------------------------------
// <copyright file="ObservableParameterObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters.Reactive
{
    using System;
    using System.Linq.Expressions;
    using System.Reactive.Subjects;

    using JetBrains.Annotations;

    public sealed class BehaviorParameterObserver<TValue, TOwner> : ParameterObserverBase<TOwner>, IObservable<TValue>
    {
        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        private readonly Func<TValue> propertyGetter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Parameters.ParameterObserver{TValue,TOwner}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     action
        ///     or
        ///     propertyGetter
        /// </exception>
        internal BehaviorParameterObserver([NotNull] Expression<Func<TValue>> propertyExpression)
            : base(propertyExpression?.Body)
        {
            this.propertyGetter = propertyExpression.Compile();
            this.subject = new BehaviorSubject<TValue>(this.propertyGetter());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Parameters.ParameterObserver{TValue,TOwner}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal BehaviorParameterObserver(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TValue>> propertyExpression)
            : base(owner, propertyExpression?.Body)
        {
            this.propertyGetter = () => propertyExpression.Compile()(this.Owner);
            this.subject = new BehaviorSubject<TValue>(this.propertyGetter());
        }

        /// <summary>
        /// The subject
        /// </summary>
        private readonly SubjectBase<TValue> subject;

        /// <summary>
        ///     Calls the action.
        /// </summary>
        protected override void CallAction() => this.subject.OnNext(this.propertyGetter());

        /// <summary>
        /// Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<TValue> observer) => this.subject.Subscribe(observer);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.subject.Dispose();
            }
        }
    }
}