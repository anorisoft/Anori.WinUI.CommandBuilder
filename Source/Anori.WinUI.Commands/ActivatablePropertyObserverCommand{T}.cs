// -----------------------------------------------------------------------
// <copyright file="ActivatablePropertyObserverCommand{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Commands.Interfaces.Commands;
using Anori.WinUI.Common;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands
{
    /// <summary>
    ///     An <see cref="ICommand" /> whose delegates can be attached for <see cref="Execute(T)" /> and
    ///     <see cref="CanExecute(T)" />.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <remarks>
    ///     The constructor deliberately prevents the use of value types.
    ///     Because ICommand takes an object, having a value type for T would cause unexpected behavior when CanExecute(null)
    ///     is called during XAML initialization for command bindings.
    ///     Using default(T) was considered and rejected as a solution because the implementor would not be able to distinguish
    ///     between a valid and defaulted values.
    ///     <para />
    ///     Instead, callers should support a value type by using a nullable value type and checking the HasValue property
    ///     before using the Value property.
    ///     <example>
    ///         <code>
    /// public MyClass()
    /// {
    ///     this.submitCommand = new ActivatablePropertyObserverCommand&lt;int?&gt;(this.Submit, this.CanSubmit);
    /// }
    ///
    /// private bool CanSubmit(int? customerId)
    /// {
    ///     return (customerId.HasValue &amp;&amp; customers.Contains(customerId.Value));
    /// }
    ///     </code>
    ///     </example>
    /// </remarks>
    public class ActivatablePropertyObserverCommand<T> : ActivatablePropertyObserverCommandBase,
        IActivatableSyncCommand<T>
    {
        /// <summary>
        ///     The execute method
        /// </summary>
        private readonly Action<T> execute;

        /// <summary>
        ///     The can execute method
        /// </summary>
        private Func<T, bool> canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatablePropertyObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute method.</param>
        public ActivatablePropertyObserverCommand(Action<T> execute)
            : this(execute, o => true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatablePropertyObserverCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute method.</param>
        /// <param name="canExecute">The can execute method.</param>
        /// <exception cref="ArgumentNullException">executeMethod
        /// or
        /// canExecuteMethod</exception>
        /// <exception cref="InvalidCastException">DelegateCommandInvalidGenericPayloadType</exception>
        public ActivatablePropertyObserverCommand(
            [NotNull] Action<T> execute,
            [NotNull] Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException(nameof(canExecute));
            }

            var genericTypeInfo = typeof(T).GetTypeInfo();

            if (genericTypeInfo.IsValueType && !genericTypeInfo.IsNullable())
            {
                throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        public IActivatableSyncCommand<T> Activate()
        {
            this.IsActive = true;
            return this;
        }

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        public IActivatableSyncCommand<T> Deactivate()
        {
            this.IsActive = false;
            return this;
        }

        /// <summary>
        ///     Determines if the command can execute by invoked the <see cref="Func{T,Bool}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command to determine if it can execute.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        public bool CanExecute(T parameter) => this.canExecute(parameter);

        /// <summary>
        ///     Executes the command and invokes the <see cref="Action{T}" /> provided during construction.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public void Execute(T parameter) => this.execute(parameter);

        /// <summary>
        ///     Observes a property that is used to determine if this command can execute, and if it implements
        ///     INotifyPropertyChanged it will automatically call ActivatablePropertyObserverCommandBase.RaiseCanExecuteChanged on
        ///     property changed
        ///     notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of ActivatablePropertyObserverCommand</returns>
        public ActivatablePropertyObserverCommand<T> ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            var expression = Expression.Lambda<Func<T, bool>>(
                canExecuteExpression.Body,
                Expression.Parameter(typeof(T), "o"));
            this.canExecute = expression.Compile();
            this.ObservesPropertyInternal(canExecuteExpression);
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="canExecuteExpression">The can execute expression.</param>
        /// <returns></returns>
        public ActivatablePropertyObserverCommand<T> ObservesCanExecute<TOwner>(
            TOwner owner,
            Expression<Func<TOwner, bool>> canExecuteExpression)
            where TOwner : INotifyPropertyChanged
        {
            var parameter = canExecuteExpression.Parameters.First();
            var parameterName2 = parameter.Name != "o" ? "o" : "arg2";
            var expression = Expression.Lambda<Func<TOwner, T, bool>>(
                canExecuteExpression.Body,
                parameter,
                Expression.Parameter(typeof(T), parameterName2));
            this.canExecute = o => expression.Compile()(owner, o);

            this.ObservesPropertyInternal(owner, canExecuteExpression);
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public ActivatablePropertyObserverCommand<T> ObservesCanExecute<TParameter, TType>(
            TParameter parameter,
            Func<T, bool> canExecute,
            Expression<Func<TParameter, TType>> propertyExpression)
            where TParameter : INotifyPropertyChanged
        {
            this.canExecute = canExecute;
            this.ObservesPropertyInternal(parameter, propertyExpression);
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <typeparam name="TParameter">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="parameter">The owner.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public ActivatablePropertyObserverCommand<T> ObservesCanExecute<TParameter, TType>(
            TParameter parameter,
            Func<TParameter, bool> canExecute,
            Expression<Func<TParameter, TType>> propertyExpression)
            where TParameter : INotifyPropertyChanged
        {
            this.canExecute = arg => canExecute(parameter);
            this.ObservesPropertyInternal(parameter, propertyExpression);
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public ActivatablePropertyObserverCommand<T> ObservesCanExecute<TOwner, TType>(
            TOwner owner,
            Func<TOwner, T, bool> canExecute,
            Expression<Func<TOwner, TType>> propertyExpression)
            where TOwner : INotifyPropertyChanged
        {
            this.canExecute = arg => canExecute(owner, arg);
            this.ObservesPropertyInternal(owner, propertyExpression);
            return this;
        }

        /// <summary>
        ///     Observes a property that implements INotifyPropertyChanged, and automatically calls
        ///     ActivatablePropertyObserverCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="TType">The type of the return value of the method that this delegate encapulates</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of ActivatablePropertyObserverCommand</returns>
        public ActivatablePropertyObserverCommand<T> ObservesProperty<TType>(Expression<Func<TType>> propertyExpression)
        {
            this.ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public ActivatablePropertyObserverCommand<T> ObservesProperty<TOwner, TType>(
            TOwner owner,
            Expression<Func<TOwner, TType>> propertyExpression)
            where TOwner : INotifyPropertyChanged
        {
            this.ObservesPropertyInternal(owner, propertyExpression);
            return this;
        }

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.CanExecute(object)" />
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns><see langword="true" /> if the Command Can Execute, otherwise <see langword="false" /></returns>
        protected override bool CanExecute(object parameter)
        {
            return this.CanExecute((T) parameter);
        }

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected override void Execute(object parameter)
        {
            this.Execute((T) parameter);
        }
    }
}