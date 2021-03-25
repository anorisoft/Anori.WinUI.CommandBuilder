// -----------------------------------------------------------------------
// <copyright file="DispatchableExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;
    using System.Threading.Tasks;

    using Anori.Common;
    using Anori.Extensions;

    using JetBrains.Annotations;

    /// <summary>
    ///     Dispatchable Context Extensions.
    /// </summary>
    public static class DispatchableExtensions
    {
        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch<TContext>([NotNull] this TContext context, EventHandler? handler)
            where TContext : IDispatchable
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            var synchronizationContext = context.Dispatcher;
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
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch<TContext>([NotNull] this TContext context, [NotNull] Action<TContext> action)
            where TContext : IDispatchable
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Dispatcher.Dispatch(context, action);
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="System.ArgumentNullException">context is null.</exception>
        public static void Dispatch<TContext>([NotNull] this TContext context, [NotNull] Action action)
            where TContext : IDispatchable
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Dispatcher.Dispatch(action);
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch<T>(
            [NotNull] this IDispatchable context,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            context.Dispatcher.Dispatch(context, value, handler);
        }

        /// <summary>
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T>([NotNull] this TContext context, T value, [NotNull] Action<T> action)
            where TContext : IDispatchable
        {
            context.Dispatcher.Dispatch(value, v => action.Raise(v));
        }

        /// <summary>
        ///     Dispatches the specified value1.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T1, T2>(
            [NotNull] this TContext context,
            T1 value1,
            T2 value2,
            [NotNull] Action<T1, T2> action)
            where TContext : IDispatchable
        {
            context.Dispatcher.Dispatch((value1, value2), v => action.Raise(v.value1, v.value2));
        }

        /// <summary>
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T>(
            [NotNull] this TContext context,
            T value,
            [NotNull] Action<TContext, T> action)
            where TContext : IDispatchable
        {
            context.Dispatcher.Dispatch((context, value), v => action.Raise(v.context, v.value));
        }

        /// <summary>
        ///     Dispatches the specified value1.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<TContext, T1, T2>(
            [NotNull] this TContext context,
            T1 value1,
            T2 value2,
            [NotNull] Action<TContext, T1, T2> action)
            where TContext : IDispatchable
        {
            context.Dispatcher.Dispatch((context, value1, value2), v => action.Raise(v.context, v.value1, v.value2));
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        /// <returns>A task object that can be awaited.</returns>
        public static Task DispatchAsync<TContext>([NotNull] this TContext context, EventHandler? handler)
            where TContext : IDispatchable
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return InternalDispatchAsync(context, handler);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        /// <returns>A task object that can be awaited.</returns>
        public static Task DispatchAsync<TContext>([NotNull] this TContext context, [NotNull] Action<TContext> action)
            where TContext : IDispatchable
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Dispatcher.DispatchAsync(context, action);
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        /// <returns>A task object that can be awaited.</returns>
        public static Task DispatchAsync<TContext>([NotNull] this TContext context, [NotNull] Action action)
            where TContext : IDispatchable
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Dispatcher.DispatchAsync(action);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        /// <returns>A task object that can be awaited.</returns>
        public static Task DispatchAsync<T>(
            [NotNull] this IDispatchable context,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return context.Dispatcher.DispatchAsync(context, value, handler);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static async Task DispatchAsync<TContext, T>(
            [NotNull] this TContext context,
            T value,
            [NotNull] Action<T> action)
            where TContext : IDispatchable =>
            await context.Dispatcher.DispatchAsync(value, v => action.Raise(v));

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static async Task DispatchAsync<TContext, T1, T2>(
            [NotNull] this TContext context,
            T1 value1,
            T2 value2,
            [NotNull] Action<T1, T2> action)
            where TContext : IDispatchable =>
            await context.Dispatcher.DispatchAsync((value1, value2), v => action.Raise(v.value1, v.value2));

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static Task DispatchAsync<TContext, T>(
            [NotNull] this TContext context,
            T value,
            [NotNull] Action<TContext, T> action)
            where TContext : IDispatchable =>
            context.Dispatcher.DispatchAsync((context, value), v => action.Raise(v.context, v.value));

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static async Task DispatchAsync<TContext, T1, T2>(
            [NotNull] this TContext context,
            T1 value1,
            T2 value2,
            [NotNull] Action<TContext, T1, T2> action)
            where TContext : IDispatchable =>
            await context.Dispatcher.DispatchAsync(
                (context, value1, value2),
                v => action.Raise(v.context, v.value1, v.value2));

        /// <summary>
        ///     Internals the dispatch asynchronous.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        private static async Task InternalDispatchAsync<TContext>(TContext context, EventHandler? handler)
            where TContext : IDispatchable
        {
            var synchronizationContext = context.Dispatcher;

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (synchronizationContext == null)
            {
                handler.RaiseEmpty(context);
            }
            else
            {
                await synchronizationContext.DispatchAsync(context, handler);
            }
        }
    }
}