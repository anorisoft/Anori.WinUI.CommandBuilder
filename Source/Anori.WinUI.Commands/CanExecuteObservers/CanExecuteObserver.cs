// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;

    using Anori.ExpressionObservers;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    /// CanExecute Observer.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.CanExecuteObserverBase" />
    public sealed class CanExecuteObserver : CanExecuteObserverBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver" /> class.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <exception cref="ArgumentNullException">canExecuteExpression</exception>
        public CanExecuteObserver([NotNull] Expression<Func<bool>> canExecuteExpression)
        {
            if (canExecuteExpression == null)
            {
                throw new ArgumentNullException(nameof(canExecuteExpression));
            }

            var observesAndGet = PropertyValueObserver.ObservesAndGet(
                canExecuteExpression,
                () => { this.Update.Raise(); },
                false);
            this.Observer = observesAndGet;
            this.CanExecute = observesAndGet.GetValue;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver" /> class.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <exception cref="ArgumentNullException">canExecuteExpression</exception>
        public CanExecuteObserver([NotNull] Expression<Func<bool>> canExecuteExpression, bool fallback)
        {
            if (canExecuteExpression == null)
            {
                throw new ArgumentNullException(nameof(canExecuteExpression));
            }

            var observesAndGet = PropertyValueObserver.ObservesAndGet(
                canExecuteExpression,
                () => this.Update.Raise(),
                fallback);
            this.Observer = observesAndGet;
            this.CanExecute = observesAndGet.GetValue;
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        public override event Action Update;

        /// <summary>
        ///     Creates the specified can execute expression.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public static CanExecuteObserver Create([NotNull] Expression<Func<bool>> canExecuteExpression)
        {
            var instance = new CanExecuteObserver(canExecuteExpression);
            instance.Subscribe();
            instance.Update += () => { Debug.WriteLine("Update"); };
            return instance;
        }

        /// <summary>
        ///     Creates the specified can execute expression.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        public static CanExecuteObserver Create([NotNull] Expression<Func<bool>> canExecuteExpression, bool fallback)
        {
            var instance = new CanExecuteObserver(canExecuteExpression, fallback);
            instance.Subscribe();
            return instance;
        }
    }
}