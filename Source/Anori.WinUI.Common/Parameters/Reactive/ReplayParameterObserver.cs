// -----------------------------------------------------------------------
// <copyright file="ReplayParameterObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters.Reactive
{
    using JetBrains.Annotations;

    using System;
    using System.Linq.Expressions;
    using System.Reactive.Subjects;

    public sealed class ReplayParameterObserver<TValue, TOwner> : ParameterObserverBase<TOwner>, IObservable<TValue?>
        where TValue : struct
    {
        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        [NotNull]
        private readonly Func<TValue?> propertyGetter;

        /// <summary>
        ///     The subject
        /// </summary>
        [NotNull]
        private readonly SubjectBase<TValue?> subject;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Parameters.ParameterObserver{TValue,TOwner}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     action
        ///     or
        ///     propertyGetter
        /// </exception>
        internal ReplayParameterObserver([NotNull] Expression<Func<TValue>> propertyExpression)
            : base(propertyExpression.Body)
        {
            this.propertyGetter = () => PropertyGetter(propertyExpression.Compile());
            this.subject = new ReplaySubject<TValue?>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReplayParameterObserver{TValue, TOwner}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        internal ReplayParameterObserver([NotNull] Expression<Func<TValue>> propertyExpression, int bufferSize)
            : base(propertyExpression?.Body)
        {
            this.propertyGetter = () => PropertyGetter(propertyExpression.Compile());
            this.subject = new ReplaySubject<TValue?>(bufferSize);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReplayParameterObserver{TValue, TOwner}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="window">The window.</param>
        internal ReplayParameterObserver(
            [NotNull] Expression<Func<TValue>> propertyExpression,
            int bufferSize,
            TimeSpan window)
            : base(propertyExpression?.Body)
        {
            this.propertyGetter = () => PropertyGetter(propertyExpression.Compile());
            this.subject = new ReplaySubject<TValue?>(bufferSize, window);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ReplayParameterObserver{TValue, TOwner}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="window">The window.</param>
        internal ReplayParameterObserver([NotNull] Expression<Func<TValue>> propertyExpression, TimeSpan window)
            : base(propertyExpression?.Body)
        {
            this.propertyGetter = () => PropertyGetter(propertyExpression.Compile());
            this.subject = new ReplaySubject<TValue?>(window);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Parameters.ParameterObserver{TValue,TOwner}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal ReplayParameterObserver(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TValue>> propertyExpression)
            : base(owner, propertyExpression?.Body)
        {
            this.propertyGetter = () => this.PropertyGetter(propertyExpression.Compile(), this.Owner);
            this.subject = new ReplaySubject<TValue?>();
        }

        internal ReplayParameterObserver(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TValue>> propertyExpression,
            int bufferSize)
            : base(owner, propertyExpression?.Body)
        {
            this.propertyGetter = () => this.PropertyGetter(propertyExpression.Compile(), this.Owner);
            this.subject = new ReplaySubject<TValue?>(bufferSize);
        }

        internal ReplayParameterObserver(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TValue>> propertyExpression,
            int bufferSize,
            TimeSpan window)
            : base(owner, propertyExpression?.Body)
        {
            this.propertyGetter = () => propertyExpression.Compile()(this.Owner);
            this.subject = new ReplaySubject<TValue?>(bufferSize, window);
        }

        internal ReplayParameterObserver(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TValue>> propertyExpression,
            TimeSpan window)
            : base(owner, propertyExpression?.Body)
        {
            this.propertyGetter = () => propertyExpression.Compile()(this.Owner);
            this.subject = new ReplaySubject<TValue?>(window);
        }

        /// <summary>
        ///     Notifies the provider that an observer is to receive notifications.
        /// </summary>
        /// <param name="observer">The object that is to receive notifications.</param>
        /// <returns>
        ///     A reference to an interface that allows observers to stop receiving notifications before the provider has finished
        ///     sending them.
        /// </returns>
        public IDisposable Subscribe(IObserver<TValue?> observer) => this.subject.Subscribe(observer);

        /// <summary>
        ///     Calls the action.
        /// </summary>
        protected override void CallAction() => this.subject.OnNext(this.propertyGetter());

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
                this.subject.Dispose();
            }
        }

        /// <summary>
        ///     Properties the getter.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        private static TValue? PropertyGetter([NotNull] Func<TValue> propertyExpression)
        {
            try
            {
                return propertyExpression();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        private TValue? PropertyGetter([NotNull] Func<TOwner, TValue> propertyExpression, TOwner owner)
        {
            try
            {
                return propertyExpression(owner);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}