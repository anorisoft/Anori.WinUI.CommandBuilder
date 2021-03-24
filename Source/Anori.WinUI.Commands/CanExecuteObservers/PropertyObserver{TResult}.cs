// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{TResult}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.Linq.Expressions;

    using Anori.Common;
    using Anori.ExpressionObservers;
    using Anori.Extensions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    ///     Property Observer.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TResult}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IPropertyObserver" />
    internal sealed class PropertyObserver<TResult> : PropertyObserverBase<TResult>, IPropertyObserver
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver{TResult}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">propertyExpression is null.</exception>
        private PropertyObserver([NotNull] Expression<Func<TResult>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Observer = PropertyObserver.Observes(propertyExpression, () => this.Update.Raise());
            this.PropertyExpression = this.Observer.ExpressionString;
        }

        /// <summary>
        ///     Occurs when [can execute changed].
        /// </summary>
        public override event Action Update;

        /// <summary>
        ///     Creates the specified property expression.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>The Property Observer.</returns>
        /// <exception cref="ArgumentNullException">propertyExpression is null.</exception>
        [NotNull]
        public static PropertyObserver<TType> Create<TType>([NotNull] Expression<Func<TType>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var instance = new PropertyObserver<TType>(propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }
}