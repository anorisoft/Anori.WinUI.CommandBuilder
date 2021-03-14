// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TParameter1,TResult}.cs" company="AnoriSoft">
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
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    /// Property Observer.
    /// </summary>
    /// <typeparam name="TParameter1">The type of the parameter.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TResult}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IPropertyObserver" />
    internal sealed class PropertyObserver<TParameter1, TResult> : PropertyObserverBase<TResult>, IPropertyObserver
        where TParameter1 : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyObserver{TParameter,TResult}" /> class.
        /// </summary>
        /// <param name="parameter">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        /// parameter1 or propertyExpression is null.
        /// </exception>
        public PropertyObserver(TParameter1 parameter, Expression<Func<TParameter1, TResult>> propertyExpression)
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
            this.PropertyExpression = this.Observer.ExpressionString;
        }

        /// <summary>
        /// Occurs when [can execute changed].
        /// </summary>
        public override event Action Update;

        /// <summary>
        ///     Gets the parameter.
        /// </summary>
        /// <value>
        ///     The parameter.
        /// </value>
        public TParameter1 Parameter { get; }

        /// <summary>
        /// Creates the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The Property Observer.</returns>
        public static PropertyObserver<TParameter1, TResult> Create(
            [NotNull] TParameter1 owner,
            [NotNull] Expression<Func<TParameter1, TResult>> propertyExpression)
        {
            var instance = new PropertyObserver<TParameter1, TResult>(owner, propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}