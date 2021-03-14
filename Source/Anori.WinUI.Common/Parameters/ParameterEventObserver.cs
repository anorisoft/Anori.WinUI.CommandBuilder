// -----------------------------------------------------------------------
// <copyright file="ParameterEventObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;
    using System.Linq.Expressions;

    using Anori.Extensions;

    using JetBrains.Annotations;

    public sealed class ParameterEventObserver<TValue, TOwner> : ParameterObserverBase<TOwner>
    {
        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        private readonly Func<TValue> propertyGetter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver{TValue, TOwner}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     action
        ///     or
        ///     propertyGetter
        /// </exception>
        internal ParameterEventObserver([NotNull] Expression<Func<TValue>> propertyExpression)
            : base(propertyExpression?.Body)
        {
            this.propertyGetter = propertyExpression.Compile();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver{TValue, TOwner}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal ParameterEventObserver(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TValue>> propertyExpression)
            : base(owner, propertyExpression?.Body)
        {
            this.propertyGetter = () =>
                propertyExpression.Compile()(this.Owner)
                ?? throw new ArgumentNullException(nameof(this.propertyGetter));
        }

        /// <summary>
        ///     Gets the length of the subscribed.
        /// </summary>
        /// <value>
        ///     The length of the subscribed.
        /// </value>
        public int SubscribedLength => this.ParameterChanged?.GetInvocationList()?.Length ?? 0;

        /// <summary>
        ///     The action
        /// </summary>
        public event Action<TValue> ParameterChanged;

        /// <summary>
        ///     Calls the action.
        /// </summary>
        protected override void CallAction() => this.ParameterChanged.Raise(this.propertyGetter());
    }
}