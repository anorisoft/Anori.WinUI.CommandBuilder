// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver{TParameter1}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using Anori.Common;
    using Anori.ExpressionObservers;
    using Anori.Extensions;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    ///     CanExecute Observer with one parameters.
    /// </summary>
    /// <typeparam name="TParameter1">The type of the parameter.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.CanExecuteObserverBase" />
    internal sealed class CanExecuteObserver<TParameter1> : CanExecuteObserverBase
        where TParameter1 : INotifyPropertyChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver{TOwner}" /> class.
        /// </summary>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     parameter
        ///     or
        ///     canExecuteExpression is null.
        /// </exception>
        public CanExecuteObserver(
            [NotNull] TParameter1 parameter,
            [NotNull] Expression<Func<TParameter1, bool>> canExecuteExpression)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (canExecuteExpression == null)
            {
                throw new ArgumentNullException(nameof(canExecuteExpression));
            }

            this.Parameter = parameter;
            var observesAndGet = PropertyValueObserver.ObservesAndGet(
                parameter,
                canExecuteExpression,
                () => this.Update.Raise(),
                false);
            this.Observer = observesAndGet;
            this.CanExecute = () => observesAndGet.GetValue();
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        public override event Action Update;

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public TParameter1 Parameter { get; }

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns>The observer.</returns>
        public static CanExecuteObserver<TParameter1> Create(
            TParameter1 parameter,
            Expression<Func<TParameter1, bool>> canExecuteExpression)
        {
            var instance = new CanExecuteObserver<TParameter1>(parameter, canExecuteExpression);
            instance.Subscribe();
            return instance;
        }
    }
}