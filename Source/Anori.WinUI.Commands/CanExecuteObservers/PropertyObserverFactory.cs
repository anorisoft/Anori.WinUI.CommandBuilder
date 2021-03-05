// -----------------------------------------------------------------------
// <copyright file="PropertyObserverFactory.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class PropertyObserverFactory
    {

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TResult">The type of the type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public PropertyObserver<TResult> ObservesProperty<TResult>(
            Expression<Func<TResult>> propertyExpression) =>
            PropertyObserver<TResult>.Create(propertyExpression);

      
        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public PropertyObserver<TParameter, TResult> ObservesProperty<TParameter, TResult>(
            TParameter parameter,
            Expression<Func<TParameter, TResult>> propertyExpression)
            where TParameter : INotifyPropertyChanged
            where TResult : struct =>
            PropertyObserver<TParameter, TResult>.Create(parameter, propertyExpression);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public CanExecuteObserver ObservesCanExecute(
            Expression<Func<bool>> canExecuteExpression) => 
            CanExecuteObserver.Create(canExecuteExpression);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public CanExecuteObserver<TParameter> ObservesCanExecute<TParameter>(
            TParameter parameter,
            Expression<Func<TParameter, bool>> canExecuteExpression)
            where TParameter : INotifyPropertyChanged =>
            CanExecuteObserver<TParameter>.Create(parameter, canExecuteExpression);


        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public CanExecuteObserver<TParameter1, TParameter2> ObservesCanExecute<TParameter1, TParameter2>(
            TParameter1 parameter1,
            TParameter2 parameter2,
            Expression<Func<TParameter1, TParameter2, bool>> canExecuteExpression)
            where TParameter1 : INotifyPropertyChanged
            where TParameter2 : INotifyPropertyChanged =>
            CanExecuteObserver<TParameter1, TParameter2>.Create(parameter1, parameter2, canExecuteExpression);
    }
}