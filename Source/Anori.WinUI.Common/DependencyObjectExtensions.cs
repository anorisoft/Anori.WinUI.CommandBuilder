// -----------------------------------------------------------------------
// <copyright file="DependencyObjectExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;

    using Anori.Common;
    using Anori.Extensions;

    using JetBrains.Annotations;

    /// <summary>
    ///     Dispatchable Context Extensions.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <param name="dependencyObject">The context.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch([NotNull] this DependencyObject dependencyObject, EventHandler? handler)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(dependencyObject));
            }

            if (handler == null)
            {
                return;
            }

            var synchronizationContext = dependencyObject.Dispatcher;
            if (synchronizationContext == null)
            {
                handler.RaiseEmpty(dependencyObject);
            }
            else
            {
                synchronizationContext.Dispatch(dependencyObject, handler);
            }
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch([NotNull] this DependencyObject context, [NotNull] Action action)
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
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<T>([NotNull] this DependencyObject context, T value, [NotNull] Action<T> action) =>
            context.Dispatcher.Dispatch(value, v => action.Raise(v));

        /// <summary>
        ///     Dispatches the specified value1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<T1, T2>(
            [NotNull] this DependencyObject context,
            T1 value1,
            T2 value2,
            [NotNull] Action<T1, T2> action) =>
            context.Dispatcher.Dispatch((value1, value2), v => action.Raise(v.value1, v.value2));

        /// <summary>
        ///     Dispatches the specified value1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<T1, T2>(
            [NotNull] this DependencyObject context,
            T1 value1,
            T2 value2,
            [NotNull] Action<DependencyObject, T1, T2> action) =>
            context.Dispatcher.Dispatch((context, value1, value2), v => action.Raise(v.context, v.value1, v.value2));

        /// <summary>
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch<T>(
            [NotNull] this DependencyObject context,
            T value,
            [NotNull] Action<DependencyObject, T> action) =>
            context.Dispatcher.Dispatch((context, value), v => action.Raise(v.context, v.value));

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

            return InternalDispatchAsync(context, value, handler);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static async Task DispatchAsync<T>(
            [NotNull] this DependencyObject context,
            T value,
            [NotNull] Action<T> action) =>
            await context.Dispatcher.DispatchAsync(value, v => action.Raise(v));

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static async Task DispatchAsync<T1, T2>(
            [NotNull] this DependencyObject context,
            T1 value1,
            T2 value2,
            [NotNull] Action<T1, T2> action) =>
            await context.Dispatcher.DispatchAsync((value1, value2), v => action.Raise(v.value1, v.value2));

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static async Task DispatchAsync<T>(
            [NotNull] this DependencyObject context,
            T value,
            [NotNull] Action<DependencyObject, T> action) =>
            await context.Dispatcher.DispatchAsync((context, value), v => action.Raise(v.context, v.value));

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="action">The action.</param>
        /// <returns>A task object that can be awaited.</returns>
        public static async Task DispatchAsync<T1, T2>(
            [NotNull] this DependencyObject context,
            T1 value1,
            T2 value2,
            [NotNull] Action<DependencyObject, T1, T2> action) =>
            await context.Dispatcher.DispatchAsync(
                (context, value1, value2),
                v => action.Raise(v.context, v.value1, v.value2));

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        /// <returns>A task object that can be awaited.</returns>
        public static Task DispatchAsync([NotNull] this DependencyObject context, EventHandler? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return InternalDispatchAsync(context, handler);
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        /// <returns>A task object that can be awaited.</returns>
        public static Task DispatchAsync([NotNull] this DependencyObject context, [NotNull] Action action)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return InternalDispatchAsync(context, action);
        }

        /// <summary>
        ///     Internals the dispatch asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        private static async Task InternalDispatchAsync(DependencyObject context, Action action)
        {
            await context.Dispatcher.DispatchAsync(action);
        }

        /// <summary>
        ///     Internals the dispatch asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        private static async Task InternalDispatchAsync(DependencyObject context, EventHandler? handler)
        {
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
                await synchronizationContext.DispatchAsync(context, handler);
            }
        }

        /// <summary>
        ///     Internals the dispatch asynchronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        private static async Task InternalDispatchAsync<T>(
            IDispatchable context,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (handler == null)
            {
                return;
            }

            await context.Dispatcher.DispatchAsync(context, value, handler);
        }
    }
}