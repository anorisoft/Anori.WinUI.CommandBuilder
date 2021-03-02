// -----------------------------------------------------------------------
// <copyright file="ActivatablePropertyObserverCommandBase.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;
using Anori.ExpressionObservers;
using Anori.WinUI.Common;
using CanExecuteChangedTests;

namespace Anori.WinUI.Commands
{
    /// <summary>
    ///     An <see cref="ICommand" /> whose delegates can be attached for <see cref="Execute" /> and <see cref="CanExecute" />
    ///     .
    /// </summary>
    public abstract class ActivatablePropertyObserverCommandBase : ICommand, IDispatchableContext
    {
        /// <summary>
        ///     The observed properties expressions
        /// </summary>
        private readonly HashSet<string> observedPropertiesExpressions = new HashSet<string>();

        /// <summary>
        ///     The is active
        /// </summary>
        private bool isActive;

        /// <summary>
        ///     Creates a new instance of a <see cref="ActivatablePropertyObserverCommandBase" />, specifying both the execute
        ///     action and the can
        ///     execute function.
        /// </summary>
        protected ActivatablePropertyObserverCommandBase() => SynchronizationContext = SynchronizationContext.Current;

        /// <summary>
        ///     Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
        public bool IsActive
        {
            get => this.isActive;
            protected set
            {
                if (this.isActive == value)
                {
                    return;
                }

                this.isActive = value;
                this.OnIsActiveChanged(value);
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        bool ICommand.CanExecute(object parameter) => this.CanExecute(parameter);

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        void ICommand.Execute(object parameter) => this.Execute(parameter);

        /// <summary>
        ///     Gets the synchronization context.
        /// </summary>
        /// <value>
        ///     The synchronization context.
        /// </value>
        public SynchronizationContext SynchronizationContext { get; }

        /// <summary>
        ///     Fired if the <see cref="IsActive" /> property changes.
        /// </summary>
        public event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        ///     Raises <see cref="CanExecuteChanged" /> so every command invoker
        ///     can requery to check if the command can execute.
        /// </summary>
        /// <remarks>Note that this will trigger the execution of <see cref="CanExecuteChanged" /> once for each invoker.</remarks>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void RaiseCanExecuteChanged() => this.OnCanExecuteChanged();

        /// <summary>
        ///     Observes a property that implements INotifyPropertyChanged, and automatically calls
        ///     ActivatablePropertyObserverCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() =&gt; PropertyName).</param>
        /// <exception cref="ArgumentException">propertyExpression</exception>
        protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
        {
            if (this.observedPropertiesExpressions.Contains(propertyExpression.ToString()))
            {
                throw new ArgumentException(
                    $"{propertyExpression} is already being observed.",
                    nameof(propertyExpression));
            }

            this.observedPropertiesExpressions.Add(propertyExpression.ToString());
            PropertyObserver.Observes(propertyExpression, this.RaiseCanExecuteChanged);
        }

        /// <summary>
        ///     Observeses the property internal.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentException">propertyExpression</exception>
        /// <exception cref="System.ArgumentException">propertyExpression</exception>
        protected internal void ObservesPropertyInternal<TOwner, T>(
            TOwner owner,
            Expression<Func<TOwner, T>> propertyExpression)
            where TOwner : INotifyPropertyChanged
        {
            var str = propertyExpression.ToAnonymousParametersString();
            if (this.observedPropertiesExpressions.Contains(str))
            {
                throw new ArgumentException(
                    $"{propertyExpression} is already being observed.",
                    nameof(propertyExpression));
            }

            this.observedPropertiesExpressions.Add(str);
            PropertyObserver.Observes(owner, propertyExpression, this.RaiseCanExecuteChanged);
        }

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.CanExecute(object)" />
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns><see langword="true" /> if the Command Can Execute, otherwise <see langword="false" /></returns>
        protected abstract bool CanExecute(object parameter);

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected abstract void Execute(object parameter);

        /// <summary>
        ///     Raises <see cref="ICommand.CanExecuteChanged" /> so every
        ///     command invoker can requery <see cref="ICommand.CanExecute" />.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler == null)
            {
                return;
            }

            this.Dispatch(handler);
        }

        /// <summary>
        ///     This raises the <see cref="ActivatablePropertyObserverCommandBase.IsActiveChanged" /> event.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        protected virtual void OnIsActiveChanged(bool value) => this.IsActiveChanged.Raise(this, value);
    }
}