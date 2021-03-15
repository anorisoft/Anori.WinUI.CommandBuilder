// -----------------------------------------------------------------------
// <copyright file="SynchronizationContextExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;
    using System.Threading;

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

            context.Post(o => action(), null);
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
        ///     Dispatches the specified value.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        public static void Dispatch<T>(
            [NotNull] this SynchronizationContext context,
            T value,
            EventHandler<EventArgs<T>> handler)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Dispatch(value, v => handler.Raise(v));
        }

        /// <summary>
        ///     Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <param name="handler">The handler.</param>
        public static void Dispatch<T>(
            [NotNull] this SynchronizationContext context,
            object sender,
            T value,
            EventHandler<EventArgs<T>> handler)
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
        /// Dispatches the specified sender.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="TEventArgs" /> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="System.ArgumentNullException">context is null.</exception>
        public static void Dispatch<TEventArgs>(
            [NotNull] this SynchronizationContext context,
            object sender,
            TEventArgs args,
            EventHandler<EventArgs> handler)
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
        /// Dispatches the specified arguments.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="args">The <see cref="TEventArgs"/> instance containing the event data.</param>
        /// <param name="handler">The handler.</param>
        /// <exception cref="System.ArgumentNullException">context is null.</exception>
        public static void Dispatch<TEventArgs>(
            [NotNull] this SynchronizationContext context,
            TEventArgs args,
            EventHandler<EventArgs> handler)
            where TEventArgs : EventArgs
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Send(o => handler.Raise(args), args);
        }
    }
}