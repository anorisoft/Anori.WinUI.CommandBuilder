// -----------------------------------------------------------------------
// <copyright file="EventHandelExtensions.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anori.WinUI.Common
{
    using CanExecuteChangedTests;

    using System;

    /// <summary>
    /// </summary>
    public static class EventHandelExtensions
    {
        /// <summary>
        ///     Raises the specified sender.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        public static bool Raise(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
            {
                return false;
            }

            eventHandler(sender, e);
            return true;
        }

        /// <summary>
        ///     Raises the specified e.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        public static bool Raise(this EventHandler eventHandler, EventArgs e)
        {
            if (eventHandler == null)
            {
                return false;
            }

            eventHandler(null, e);
            return true;
        }


        /// <summary>
        ///     Raises the specified e.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <returns></returns>
        public static bool Raise(this PropertyChangedEventHandler? eventHandler, [CallerMemberName] string? propertyName = null!)
        {
            if (eventHandler == null)
            {
                return false;
            }
            
            eventHandler(null, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        public static bool Raise(this PropertyChangedEventHandler? eventHandler, [CanBeNull] INotifyPropertyChanged sender, [CallerMemberName] string? propertyName = null!)
        {
            if (eventHandler == null)
            {
                return false;
            }

            eventHandler(sender, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        /// <summary>
        ///     Raises the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static bool Raise(this Action action)
        {
            if (action == null)
            {
                return false;
            }

            action();
            return true;
        }

        public static T Raise<T>(this Func<T>? func)
        {
            if (func == null)
            {
                return default(T);
            }

            return func();
        }




         public static async Task<bool> RaiseAsync(this Func<Task>? func)
        {
            if (func == null)
            {
                return false;
            }

            await func.Invoke();
            return true;
        }


         public static async Task<bool> RaiseAsync<T>(this Func<T,Task>? func, T arg)
         {
             if (func == null)
             {
                 return false; 
             }

             await func.Invoke(arg);
             return true;
        }

        /// <summary>
        ///     Raises the specified parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public static bool Raise<T>(this Action<T> action, T parameter)
        {
            if (action == null)
            {
                return false;
            }

            action(parameter);
            return true;
        }

        /// <summary>
        ///     Raises the specified parameter1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <returns></returns>
        public static bool Raise<T1, T2>(this Action<T1, T2> action, T1 parameter1, T2 parameter2)
        {
            if (action != null)
            {
                action(parameter1, parameter2);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Raises the specified parameter1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <returns></returns>
        public static bool Raise<T1, T2, T3>(
            this Action<T1, T2, T3> action,
            T1 parameter1,
            T2 parameter2,
            T3 parameter3)
        {
            if (action == null)
            {
                return false;
            }

            action(parameter1, parameter2, parameter3);
            return true;
        }

        /// <summary>
        ///     Raises the specified parameter1.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="parameter3">The parameter3.</param>
        /// <param name="parameter4">The parameter4.</param>
        /// <returns></returns>
        public static bool Raise<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4>? action,
            T1 parameter1,
            T2 parameter2,
            T3 parameter3,
            T4 parameter4)
        {
            if (action == null)
            {
                return false;
            }

            action(parameter1, parameter2, parameter3, parameter4);
            return true;
        }

        /// <summary>
        ///     Raises the specified sender.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static bool Raise<T>(this EventHandler<T>? eventHandler, object sender, T e)
            where T : EventArgs
        {
            if (eventHandler == null)
            {
                return false;
            }

            eventHandler(sender, e);
            return true;
        }

        /// <summary>
        ///     Raises the specified e.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="e">The e.</param>
        /// <returns></returns>
        public static bool Raise<T>(this EventHandler<T> eventHandler, T e)
            where T : EventArgs
        {
            if (eventHandler == null)
            {
                return false;
            }

            eventHandler(null, e);
            return true;
        }

        /// <summary>
        ///     Raises the specified sender.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool Raise<T>(this EventHandler<EventArgs<T>> eventHandler, object sender, T value)
        {
            if (eventHandler == null)
            {
                return false;
            }

            eventHandler(sender, new EventArgs<T>(value));
            return true;
        }

        /// <summary>
        ///     Raises the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool Raise<T>(this EventHandler<EventArgs<T>> eventHandler, T value)
        {
            if (eventHandler == null)
            {
                return false;
            }

            eventHandler(null, new EventArgs<T>(value));
            return true;
        }

        /// <summary>
        ///     Raises the empty.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <returns></returns>
        public static bool RaiseEmpty(this EventHandler eventHandler) => Raise(eventHandler, EventArgs.Empty);

        /// <summary>
        ///     Raises the empty.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The sender.</param>
        /// <returns></returns>
        public static bool RaiseEmpty(this EventHandler eventHandler, object sender) =>
            Raise(eventHandler, sender, EventArgs.Empty);
    }
}