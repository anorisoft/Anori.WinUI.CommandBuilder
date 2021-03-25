// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TParameter1,TParameter2,TResult}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using Anori.ExpressionObservers;
    using Anori.Extensions;
    using Anori.WinUI.Commands.Interfaces;
    using JetBrains.Annotations;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    /// <summary>
    ///     Property Observer.
    /// </summary>
    /// <typeparam name="TParameter1">The type of the parameter1.</typeparam>
    /// <typeparam name="TParameter2">The type of the parameter2.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TResult}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IPropertyObserver" />
    internal sealed class PropertyObserver<TParameter1, TParameter2, TResult> : PropertyObserverBase<TResult>,
                                                                                IPropertyObserver
        where TParameter1 : INotifyPropertyChanged
        where TParameter2 : INotifyPropertyChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{TParameter1, TParameter2, TResult}" /> class.
        /// </summary>
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">parameter1 or parameter2 or propertyExpression is null.</exception>
        public PropertyObserver(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression)
        {
            this.Parameter1 = parameter1 ?? throw new ArgumentNullException(nameof(parameter1));
            this.Parameter2 = parameter2 ?? throw new ArgumentNullException(nameof(parameter2));

            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Observer = PropertyObserver.Observes<TParameter1, TParameter2, TResult>(
                parameter1,
                parameter2,
                propertyExpression,
                () => this.Update.Raise());
            this.PropertyExpression = this.Observer.ExpressionString;
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
        /// <param name="parameter1">The parameter1.</param>
        /// <param name="parameter2">The parameter2.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>
        /// The Property Observer.
        /// </returns>
        public static PropertyObserver<TParameter1, TParameter2, TResult> Create(
            [NotNull] TParameter1 parameter1,
            [NotNull] TParameter2 parameter2,
            [NotNull] Expression<Func<TParameter1, TParameter2, TResult>> propertyExpression)
        {
            var instance = new PropertyObserver<TParameter1, TParameter2, TResult>(
                parameter1,
                parameter2,
                propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}