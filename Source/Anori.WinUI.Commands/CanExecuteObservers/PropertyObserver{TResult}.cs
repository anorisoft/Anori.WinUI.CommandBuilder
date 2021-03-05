// -----------------------------------------------------------------------
// <copyright file="PropertyObserver{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.ExpressionObservers;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Common;


namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using JetBrains.Annotations;
    using System;
    using System.Linq.Expressions;

    public sealed class PropertyObserver<TResult> : PropertyObserverBase<TResult>, IPropertyObserver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValueObserver{TParameter}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">propertyExpression</exception>
        private PropertyObserver([NotNull] Expression<Func<TResult>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            this.Observer = PropertyObserver.Observes(propertyExpression, () => this.Update.Raise(),false);
            this.PropertyExpression = Observer.ExpressionString;
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
        /// <returns></returns>
        [NotNull]
        public static PropertyObserver<TType> Create<TType>([NotNull] Expression<Func<TType>> propertyExpression)
        {
            if (propertyExpression == null) throw new ArgumentNullException(nameof(propertyExpression));
            var instance = new PropertyObserver<TType>(propertyExpression);
            instance.Subscribe();
            return instance;
        }
    }

}