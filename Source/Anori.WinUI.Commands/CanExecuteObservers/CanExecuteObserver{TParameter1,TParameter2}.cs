// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver{TParameter1,TParameter2}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    using Anori.ExpressionObservers;
    using Anori.Extensions;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    ///     CanExecute Observer with two parameters.
    /// </summary>
    /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
    /// <typeparam name="TParameter2">The type of the parameter2.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.CanExecuteObserverBase" />
    internal sealed class CanExecuteObserver<TParameter1, TParameter2> : CanExecuteObserverBase
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver{TParameter1, TParameter2}" /> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <exception cref="ArgumentNullException">parameter1 or parameter2 or canExecuteExpression is null.</exception>
        public CanExecuteObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, bool>> canExecuteExpression)
        {
            if (parameter1 == null)
            {
                throw new ArgumentNullException(nameof(parameter1));
            }

            if (parameter2 == null)
            {
                throw new ArgumentNullException(nameof(parameter2));
            }

            if (canExecuteExpression == null)
            {
                throw new ArgumentNullException(nameof(canExecuteExpression));
            }

            this.Parameter1 = parameter1;
            this.Parameter2 = parameter2;
            this.Observer = PropertyValueObserver.Observes(
                parameter1,
                parameter2,
                canExecuteExpression,
                () => this.Update.Raise());
            this.CanExecute = () => canExecuteExpression.Compile()(parameter1, parameter2);
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        public override event Action Update;

        /// <summary>
        ///     Gets the parameter1.
        /// </summary>
        /// <value>
        ///     The parameter1.
        /// </value>
        public TParameter1 Parameter1 { get; }

        /// <summary>
        ///     Gets the parameter2.
        /// </summary>
        /// <value>
        ///     The parameter2.
        /// </value>
        public TParameter2 Parameter2 { get; }

        /// <summary>
        /// Creates the specified owner.
        /// </summary>
        /// <param name="parameter1">The owner.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns>CanExecute Observer.</returns>
        public static CanExecuteObserver<TParameter1, TParameter2> Create(
            TParameter1 parameter1,
            TParameter2 parameter2,
            Expression<Func<TParameter1, TParameter2, bool>> canExecuteExpression)
        {
            var instance = new CanExecuteObserver<TParameter1, TParameter2>(
                parameter1,
                parameter2,
                canExecuteExpression);
            instance.Subscribe();
            return instance;
        }
    }
}