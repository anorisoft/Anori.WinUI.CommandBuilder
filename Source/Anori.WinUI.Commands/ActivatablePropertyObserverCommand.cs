// -----------------------------------------------------------------------
// <copyright file="ActivatablePropertyObserverCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Commands.Interfaces.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands
{
    /// <summary>
    /// An <see cref="ICommand" /> whose delegates do not take any parameters for <see cref="Execute()" /> and
    /// <see cref="CanExecute()" />.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.ActivatablePropertyObserverCommandBase" />
    /// <seealso cref="ISyncCommand" />
    /// <see cref="ActivatablePropertyObserverCommandBase" />
    public class ActivatablePropertyObserverCommand :
        ActivatablePropertyObserverCommandBase, IActivatableSyncCommand
    {
        /// <summary>
        /// The execute method
        /// </summary>
        [NotNull] private readonly Action execute;

        /// <summary>
        ///     The can execute method
        /// </summary>
        [NotNull] private Func<bool> canExecute;

        /// <summary>
        ///     Creates a new instance of <see cref="ActivatablePropertyObserverCommand" /> with the <see cref="Action" /> to
        ///     invoke on execution.
        /// </summary>
        /// <param name="execute">The <see cref="Action" /> to invoke when <see cref="ICommand.Execute(object)" /> is called.</param>
        public ActivatablePropertyObserverCommand([NotNull] Action execute)
            : this(execute, () => true)
        {
        }

        /// <summary>
        ///     Creates a new instance of <see cref="ActivatablePropertyObserverCommand" /> with the <see cref="Action" /> to
        ///     invoke on execution
        ///     and a <see langword="Func" /> to query for determining if the command can execute.
        /// </summary>
        /// <param name="execute">The <see cref="Action" /> to invoke when <see cref="ICommand.Execute" /> is called.</param>
        /// <param name="canExecute">
        ///     The <see cref="Func{TResult}" /> to invoke when <see cref="ICommand.CanExecute" /> is
        ///     called
        /// </param>
        public ActivatablePropertyObserverCommand([NotNull] Action execute, [NotNull] Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(this.canExecute));
        }

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        public IActivatableSyncCommand Activate()
        {
            this.IsActive = true;
            return this;
        }

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        public IActivatableSyncCommand Deactivate()
        {
            this.IsActive = false;
            return this;
        }

        /// <summary>
        ///     Determines if the command can be executed.
        /// </summary>
        /// <returns>Returns <see langword="true" /> if the command can execute,otherwise returns <see langword="false" />.</returns>
        public bool CanExecute() => this.IsActive && this.canExecute();

        /// <summary>
        ///     Executes the command.
        /// </summary>
        void Interfaces.ISyncCommand.Execute()
        {
            this.execute();
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            if (this.CanExecute())
            {
                this.execute();
            }
        }

        /// <summary>
        ///     Observes a property that is used to determine if this command can execute, and if it implements
        ///     INotifyPropertyChanged it will automatically call ActivatablePropertyObserverCommandBase.RaiseCanExecuteChanged on
        ///     property changed
        ///     notifications.
        /// </summary>
        /// <param name="canExecuteExpression">The property expression. Example: ObservesCanExecute(() => PropertyName).</param>
        /// <returns>The current instance of ActivatablePropertyObserverCommand</returns>
        public ActivatablePropertyObserverCommand ObservesCanExecute(Expression<Func<bool>> canExecuteExpression)
        {
            this.canExecute = canExecuteExpression.Compile();
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
        public ActivatablePropertyObserverCommand ObservesCanExecute<TOwner>(
            TOwner owner,
            Expression<Func<TOwner, bool>> canExecuteExpression)
            where TOwner : INotifyPropertyChanged
        {
            var parameter = canExecuteExpression.Parameters.First();
            var expression = Expression.Lambda<Func<TOwner, bool>>(canExecuteExpression.Body, parameter);
            this.canExecute = () => expression.Compile()(owner);
            this.ObservesPropertyInternal(owner, canExecuteExpression);
            return this;
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns></returns>
        public ActivatablePropertyObserverCommand ObservesProperty<TOwner, T>(
            TOwner owner,
            Expression<Func<TOwner, T>> propertyExpression)
            where TOwner : INotifyPropertyChanged
        {
            this.ObservesPropertyInternal(owner, propertyExpression);
            return this;
        }

        /// <summary>
        ///     Observes a property that implements INotifyPropertyChanged, and automatically calls
        ///     ActivatablePropertyObserverCommandBase.RaiseCanExecuteChanged on property changed notifications.
        /// </summary>
        /// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
        /// <param name="propertyExpression">The property expression. Example: ObservesProperty(() => PropertyName).</param>
        /// <returns>The current instance of ActivatablePropertyObserverCommand</returns>
        public ActivatablePropertyObserverCommand ObservesProperty<T>(Expression<Func<T>> propertyExpression)
        {
            this.ObservesPropertyInternal(propertyExpression);
            return this;
        }

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.CanExecute(object)" />
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns><see langword="true" /> if the Command Can Execute, otherwise <see langword="false" /></returns>
        protected override bool CanExecute(object parameter) => this.CanExecute();

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.Execute(object)" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected override void Execute(object parameter) => this.Execute();
    }
}