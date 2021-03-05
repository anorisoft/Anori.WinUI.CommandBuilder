// -----------------------------------------------------------------------
// <copyright file="DispatchableContextExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using CanExecuteChangedTests;

    using JetBrains.Annotations;

    using System;

    public static class DispatchableContextExtensions
    {
        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public static void Dispatch<TContext>([NotNull] this TContext context, [CanBeNull] EventHandler handler)
            where TContext : IDispatchableContext
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            var synchronizationContext = context.SynchronizationContext;
            if (synchronizationContext == null)
            {
                handler.RaiseEmpty(context);
            }
            else
            {
                synchronizationContext.Dispatch(context, handler);
            }
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="TContext">The type of the context context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public static void Dispatch<TContext>([NotNull] this TContext context, [NotNull] Action<TContext> action)
            where TContext : IDispatchableContext
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SynchronizationContext.Dispatch(context, action);
        }
        public static void Dispatch<TContext>([NotNull] this TContext context, [NotNull] Action action)
            where TContext : IDispatchableContext
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.SynchronizationContext.Dispatch(action);
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public static void Dispatch<T>(
            [NotNull] this IDispatchableContext context,
            T value,
            [CanBeNull] EventHandler<EventArgs<T>> handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            context.SynchronizationContext.Dispatch(context, value, handler);
        }

        /// <summary>
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T>([NotNull] this TContext context, T value, [NotNull] Action<T> action)
            where TContext : IDispatchableContext
        {
            context.SynchronizationContext.Dispatch(value, v => action.Raise(v));
        }

        /// <summary>
        ///     Dispatches the specified value1.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T1, T2>(
            [NotNull] this TContext context,
            T1 value1,
            T2 value2,
            [NotNull] Action<T1, T2> action)
            where TContext : IDispatchableContext
        {
            context.SynchronizationContext.Dispatch((value1, value2), v => action.Raise(v.value1, v.value2));
        }

        /// <summary>
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T>(
            [NotNull] this TContext context,
            T value,
            [NotNull] Action<TContext, T> action)
            where TContext : IDispatchableContext
        {
            context.SynchronizationContext.Dispatch((context, value), v => action.Raise(v.context, v.value));
        }

        /// <summary>
        ///     Dispatches the specified value1.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T1, T2>(
            [NotNull] this TContext context,
            T1 value1,
            T2 value2,
            [NotNull] Action<TContext, T1, T2> action)
            where TContext : IDispatchableContext
        {
            context.SynchronizationContext.Dispatch(
                (context, value1, value2),
                v => action.Raise(v.context, v.value1, v.value2));
        }
    }
}