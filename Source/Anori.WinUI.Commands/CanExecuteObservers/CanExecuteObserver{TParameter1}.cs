// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserver{TOwner}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.ExpressionObservers;

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using Anori.WinUI.Common;
    using JetBrains.Annotations;

    public sealed class CanExecuteObserver<TParameter> : CanExecuteObserverBase
        where TParameter : INotifyPropertyChanged

    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CanExecuteObserver{TOwner}" /> class.
        /// </summary>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CanExecuteObserver([NotNull] TParameter parameter, [NotNull] Expression<Func<TParameter, bool>> canExecuteExpression)
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
             var observesAndGet = PropertyValueObserver.ObservesAndGet(parameter, canExecuteExpression, () => this.Update.Raise(), false);
            this.Observer = observesAndGet;
            this.CanExecute = () => observesAndGet.GetValue();
            //this.Observer = PropertyValueObserver.Observes<TParameter, bool>(parameter, canExecuteExpression, () => this.Update.Raise());
            //this.CanExecute = () => canExecuteExpression.Compile()(parameter);
        }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public TParameter Parameter { get; }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        public override event Action Update;

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public static CanExecuteObserver<TParameter> Create(
            TParameter parameter,
            Expression<Func<TParameter, bool>> canExecuteExpression)
        {
            var instance = new CanExecuteObserver<TParameter>(parameter, canExecuteExpression);
            instance.Subscribe();
            return instance;
        }
    }
}