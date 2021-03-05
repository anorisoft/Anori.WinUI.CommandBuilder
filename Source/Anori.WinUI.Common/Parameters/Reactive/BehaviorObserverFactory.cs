// -----------------------------------------------------------------------
// <copyright file="BehaviorObserverFactory.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters.Reactive
{
    using System;
    using System.Linq.Expressions;

    public static class BehaviorObserverFactory
    {
        public static BehaviorParameterObserver<TValue, TOwner> Observes<TValue, TOwner>(
            TOwner owner,
            Expression<Func<TOwner, TValue>> propertyExpression,
            bool isAutoSubscribe = true)
        {
            var observer = new BehaviorParameterObserver<TValue, TOwner>(owner, propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }

        public static BehaviorParameterObserver<TValue, object> Observes<TValue>(
            Expression<Func<TValue>> propertyExpression,
            bool isAutoSubscribe = true)
        {
            var observer = new BehaviorParameterObserver<TValue, object>(propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }
    }
}