// -----------------------------------------------------------------------
// <copyright file="DispatcherExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Threading;

    using Anori.Common;
    using Anori.Extensions;

    using JetBrains.Annotations;

    /// <summary>
    ///     Synchronization Context Extensions.
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="action">The action.</param>
        public static void Dispatch([NotNull] this Dispatcher dispatcher, [NotNull] Action action)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            dispatcher.Invoke(() => action());
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dispatcher
        ///     or
        ///     action is null.
        /// </exception>
        public static async Task DispatchAsync([NotNull] this Dispatcher dispatcher, [NotNull] Action action)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await dispatcher.InvokeAsync(action);
        }

        /// <summary>
        ///     Dispatches the specified action.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="state">The state.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dispatcher
        ///     or
        ///     action is null.
        /// </exception>
        public static void Dispatch<TState>(
            [NotNull] this Dispatcher dispatcher,
            [CanBeNull] TState state,
            [NotNull] Action<TState> action)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            dispatcher.Invoke(() => action(state));
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <typeparam name="TState">The type of the state.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="state">The state.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     dispatcher
        ///     or
        ///     action is null.
        /// </exception>
        public static async Task DispatchAsync<TState>(
            [NotNull] this Dispatcher dispatcher,
            [CanBeNull] TState state,
            [NotNull] Action<TState> action)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            await dispatcher.InvokeAsync(() => action(state));
        }

        /// <summary>
        ///     Dispatches the specified handler.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static void Dispatch([NotNull] this Dispatcher dispatcher, EventHandler? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (handler == null)
            {
                return;
            }

            dispatcher.Invoke(handler.RaiseEmpty);
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static async Task DispatchAsync([NotNull] this Dispatcher dispatcher, EventHandler? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (handler == null)
            {
                return;
            }

            await dispatcher.InvokeAsync(handler.RaiseEmpty);
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static void Dispatch([NotNull] this Dispatcher dispatcher, object sender, EventHandler? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (handler == null)
            {
                return;
            }

            dispatcher.Invoke(() => handler.RaiseEmpty(sender));
        }

        /// <summary>
        ///     Dispatches the asynchronous.
        /// </summary>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher the type.</exception>
        public static async Task DispatchAsync(
            [NotNull] this Dispatcher dispatcher,
            object sender,
            EventHandler? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            if (handler == null)
            {
                return;
            }

            await dispatcher.InvokeAsync(() => handler.RaiseEmpty(sender));
        }

        /// <summary>
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        public static void Dispatch<T>(
            [NotNull] this Dispatcher dispatcher,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            dispatcher.Dispatch(value, v => handler.Raise(v));
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static async Task DispatchAsync<T>(
            [NotNull] this Dispatcher dispatcher,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            await dispatcher.DispatchAsync(value, v => handler.Raise(v));
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static void Dispatch<T>(
            [NotNull] this Dispatcher dispatcher,
            object sender,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            dispatcher.Invoke(() => handler.Raise(sender, value));
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static async Task DispatchAsync<T>(
            [NotNull] this Dispatcher dispatcher,
            object sender,
            T value,
            EventHandler<EventArgs<T>>? handler)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            await dispatcher.InvokeAsync(() => handler.Raise(sender, value));
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static void Dispatch<TEventArgs>(
            [NotNull] this Dispatcher dispatcher,
            object sender,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            dispatcher.Invoke(() => handler.Raise(sender, args));
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static async Task DispatchAsync<TEventArgs>(
            [NotNull] this Dispatcher dispatcher,
            object sender,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            await dispatcher.InvokeAsync(() => handler.Raise(sender, args));
        }

        /// <summary>
        ///     Dispatches the specified arguments.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static void Dispatch<TEventArgs>(
            [NotNull] this Dispatcher dispatcher,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            dispatcher.Invoke(() => handler.Raise(args));
        }

        /// <summary>
        ///     Dispatches the asyncronous.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="dispatcher">The dispatcher.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="ArgumentNullException">dispatcher is null.</exception>
        public static async Task DispatchAsync<TEventArgs>(
            [NotNull] this Dispatcher dispatcher,
            TEventArgs args,
            EventHandler<EventArgs>? handler)
            where TEventArgs : EventArgs
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            await dispatcher.InvokeAsync(() => handler.Raise(args));
        }
    }
}