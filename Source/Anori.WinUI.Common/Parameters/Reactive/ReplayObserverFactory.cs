// -----------------------------------------------------------------------
// <copyright file="ReplayObserverFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters.Reactive
{
    using System;
    using System.Linq.Expressions;

    public static class ReplayObserverFactory
    {
        public static ReplayParameterObserver<TValue, TOwner> Observes<TValue, TOwner>(
            TOwner owner,
            Expression<Func<TOwner, TValue>> propertyExpression,
            bool isAutoSubscribe = true) where TValue : struct
        {
            var observer = new ReplayParameterObserver<TValue, TOwner>(owner, propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }

        public static ReplayParameterObserver<TValue, object> Observes<TValue>(
            Expression<Func<TValue>> propertyExpression,
            bool isAutoSubscribe = true) where TValue : struct
        {
            var observer = new ReplayParameterObserver<TValue, object>(propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }
    }
}