// -----------------------------------------------------------------------
// <copyright file="ParameterObserverFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;
    using System.Linq.Expressions;

    public static class ParameterObserverFactory
    {
        /// <summary>
        ///     Observeses the specified property expression.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterObserver<TValue, object> Observes<TValue>(
            Expression<Func<TValue>> propertyExpression,
            Action<TValue> action,
            bool isAutoSubscribe = true)
        {
            var observer = new ParameterObserver<TValue, object>(propertyExpression, action);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }

        /// <summary>
        ///     Observes the specified owner.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterObserver<TValue, TOwner> Observes<TValue, TOwner>(
            TOwner owner,
            Expression<Func<TOwner, TValue>> propertyExpression,
            Action<TValue> action,
            bool isAutoSubscribe = true)
        {
            ParameterObserver<TValue, TOwner> observer = null;
            observer = new ParameterObserver<TValue, TOwner>(owner, propertyExpression, action);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }

        /// <summary>
        ///     Observeses the specified owner.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterEventObserver<TValue, TOwner> Observes<TValue, TOwner>(
            TOwner owner,
            Expression<Func<TOwner, TValue>> propertyExpression,
            bool isAutoSubscribe = true)
        {
            var observer = new ParameterEventObserver<TValue, TOwner>(owner, propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }

        /// <summary>
        ///     Observeses the specified owner.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterEventObserver<TValue, object> Observes<TValue>(
            Expression<Func<TValue>> propertyExpression,
            bool isAutoSubscribe = true)
        {
            var observer = new ParameterEventObserver<TValue, object>(propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }
    }
}