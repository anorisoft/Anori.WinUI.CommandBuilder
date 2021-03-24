// -----------------------------------------------------------------------
// <copyright file="SynchronizationContextExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Anori.Common;
    using Anori.Extensions;

    using JetBrains.Annotations;

    /// <summary>
    ///     Synchronization Context Extensions.
    /// </summary>
    public static class SynchronizationContextExtensions
    {
        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">context or actionis null.</exception>
        public static void Dispatch([NotNull] this SynchronizationContext context, [NotNull] Action action)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            context.Send(o => action(), null);
        }

        /// <summary>
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="d">The d.</param>
        /// <param name="state">The state.</param>
        public static Task SendAsync(this SynchronizationContext context, SendOrPostCallback d, object state)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            context.Post(
                o =>
                    {
                        try
                        {
                            d(state);
                            taskCompletionSource.SetResult(true);
                        }
                        catch (Exception e)
                        {
                            taskCompletionSource.SetException(e);
                        }
                    },
                null);
            return taskCompletionSource.Task;
        }

        /// <summary>
        ///     Sends the specified action.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public static void Send<T1, T2>(this SynchronizationContext context, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            context.Send(
                o =>
                    {
                        var (a1, a2) = o;
                        action(a1, a2);
                    },
                new ValueTuple<T1, T2>(arg1, arg2));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public static Task SendAsync<T1, T2>(
            this SynchronizationContext context,
            Action<T1, T2> action,
            T1 arg1,
            T2 arg2)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            context.Post(
                o =>
                    {
                        try
                        {
                            action(arg1, arg2);
                            taskCompletionSource.SetResult(true);
                        }
                        catch (Exception e)
                        {
                            taskCompletionSource.SetException(e);
                        }
                    },
                null);
            return taskCompletionSource.Task;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <param name="arg">The state.</param>
        public static Task SendAsync<T>(this SynchronizationContext context, Action<T> action, T arg)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            context.Post(
                o =>
                    {
                        try
                        {
                            action(arg);
                            taskCompletionSource.SetResult(true);
                        }
                        catch (Exception e)
                        {
                            taskCompletionSource.SetException(e);
                        }
                    },
                null);
            return taskCompletionSource.Task;
        }

        /// <summary>
        ///     Sends the specified action.
        /// </summary>
        /// <typeparam name="T">The argument.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <param name="arg">The argument.</param>
        public static void Send<T>(this SynchronizationContext context, Action<T> action, T arg) =>
            context.Send(o => action((T)o), arg);

        /// <summary>
        ///     Sends the specified action.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        public static void Send(this SynchronizationContext context, Action action) =>
            context.Send(o => action(), null);

        /// <summary>
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        public static Task SendAsync(this SynchronizationContext context, Action action)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            context.Post(
                state =>
                    {
                        try
                        {
                            action();
                            taskCompletionSource.SetResult(true);
                        }
                        catch (Exception e)
                        {
                            taskCompletionSource.SetException(e);
                        }
                    },
                null);
            return taskCompletionSource.Task;
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     context
        ///     or
        ///     action is null.
        /// </exception>
        public static async Task DispatchAsync([NotNull] this SynchronizationContext context, [NotNull] Action action)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await context.SendAsync(action);
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="state">The state.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     context
        ///     or
        ///     action is null.
        /// </exception>
        public static void Dispatch<TState>(
            [NotNull] this SynchronizationContext context,
            [CanBeNull] TState state,
            [NotNull] Action<TState> action)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            context.Send(o => action((TState)o), state);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="state">The state.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     context
        ///     or
        ///     action is null.
        /// </exception>
        public static async Task DispatchAsync<TState>(
            [NotNull] this SynchronizationContext context,
            [CanBeNull] TState state,
            [NotNull] Action<TState> action)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await context.SendAsync(action, state);
        }

        /// <summary>
        ///     Dispatches the specified handler.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch([NotNull] this SynchronizationContext context, EventHandler? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            context.Send(o => handler.RaiseEmpty(), null);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static async Task DispatchAsync([NotNull] this SynchronizationContext context, EventHandler? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            await context.SendAsync(() => handler.RaiseEmpty());
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch([NotNull] this SynchronizationContext context, object sender, EventHandler? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            context.Send(s => handler.RaiseEmpty(s), sender);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context the type.</exception>
        public static async Task DispatchAsync(
            [NotNull] this SynchronizationContext context,
            object sender,
            EventHandler? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (handler == null)
            {
                return;
            }

            await context.SendAsync<object>(s => handler.RaiseEmpty(s), sender);
        }

        /// <summary>
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        public static void Dispatch<T>(
            [NotNull] this SynchronizationContext context,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Dispatch(value, v => handler.Raise(v));
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static async Task DispatchAsync<T>(
            [NotNull] this SynchronizationContext context,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await context.DispatchAsync(value, v => handler.Raise(v));
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch<T>(
            [NotNull] this SynchronizationContext context,
            object sender,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Send(
                o =>
                    {
                        var (s, a) = (ValueTuple<object, T>)o;
                        handler.Raise(s, a);
                    },
                new ValueTuple<object, T>(sender, value));
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static async Task DispatchAsync<T>(
            [NotNull] this SynchronizationContext context,
            object sender,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await context.SendAsync(
                o =>
                    {
                        var (s, a) = o;
                        handler.Raise(s, a);
                    },
                new ValueTuple<object, T>(sender, value));
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch<TEventArgs>(
            [NotNull] this SynchronizationContext context,
            object sender,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Send(
                o =>
                    {
                        var (s, a) = (ValueTuple<object, TEventArgs>)o;
                        handler.Raise(s, a);
                    },
                new ValueTuple<object, TEventArgs>(sender, args));
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static async Task DispatchAsync<TEventArgs>(
            [NotNull] this SynchronizationContext context,
            object sender,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await context.SendAsync(
                o =>
                    {
                        var (s, a) = o;
                        handler.Raise(s, a);
                    },
                new ValueTuple<object, TEventArgs>(sender, args));
        }

        /// <summary>
        ///     Dispatches the specified arguments.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static void Dispatch<TEventArgs>(
            [NotNull] this SynchronizationContext context,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Send(o => handler.Raise(o), args);
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">context is null.</exception>
        public static async Task DispatchAsync<TEventArgs>(
            [NotNull] this SynchronizationContext context,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await context.SendAsync(o => handler.Raise(o), args);
        }
    }
}