// -----------------------------------------------------------------------
// <copyright file="ObserverFactory.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters.Reactive
{
    using System;
    using System.Linq.Expressions;

    public static class ObserverFactory
    {
        public static ParameterObserver<TValue, TOwner> Observes<TValue, TOwner>(
            TOwner owner,
            Expression<Func<TOwner, TValue>> propertyExpression,
            bool isAutoSubscribe = true)
        {
            var observer = new ParameterObserver<TValue, TOwner>(owner, propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }

        public static ParameterObserver<TValue, object> Observes<TValue>(
            Expression<Func<TValue>> propertyExpression,
            bool isAutoSubscribe = true)
        {
            var observer = new ParameterObserver<TValue, object>(propertyExpression);
            if (isAutoSubscribe)
            {
                observer.SubscribeListener();
            }

            return observer;
        }
    }
}