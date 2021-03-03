// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TOwner,T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------


using Anori.ExpressionObservers;

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;
    using JetBrains.Annotations;
    using System;

    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    /// PropertyObserver
    /// </summary>
    /// <typeparam name="TParameter">The type of the parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TResult}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IPropertyObserver" />
    public sealed class PropertyObserver<TParameter, TResult> : PropertyObserverBase<TResult>,
                                                      IPropertyObserver
        where TParameter : INotifyPropertyChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{TParameter,TResult}" /> class.
        /// </summary>
        /// <param name="parameter">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public PropertyObserver(TParameter parameter, Expression<Func<TParameter, TResult>> propertyExpression)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Parameter = parameter;
            this.Observer = PropertyObserver.Observes(parameter, propertyExpression, () => this.Update.Raise(), false);
            this.PropertyExpression = Observer.ExpressionString;
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        public override event Action Update;

        /// <summary>
        /// Gets the parameter.
        /// </summary>
        /// <value>
        /// The parameter.
        /// </value>
        public TParameter Parameter { get; }

        /// <summary>
        ///     Creates the specified owner.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public static PropertyObserver<TParameter, TResult> Create(
            [NotNull] TParameter owner,
            [NotNull] Expression<Func<TParameter, TResult>> propertyExpression)
        {
            var instance = new PropertyObserver<TParameter, TResult>(owner, propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}