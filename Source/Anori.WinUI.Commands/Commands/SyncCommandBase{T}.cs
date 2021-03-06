// -----------------------------------------------------------------------
// <copyright file="SyncCommandBase{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Windows.Input;

    using Anori.WinUI.Commands.Interfaces;

    using JetBrains.Annotations;

    /// <summary>
    ///     A Command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    /// <seealso cref="T:System.Windows.Input.ICommand" />
    public abstract class SyncCommandBase<T> : CommandBase, ISyncCommand<T>
    {
        /// <summary>
        ///     The can execute.
        /// </summary>
        [CanBeNull]
        private readonly Predicate<T> canExecute;

        /// <summary>
        ///     The execute.
        /// </summary>
        [NotNull]
        private readonly Action<T> execute;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SyncCommandBase{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="System.ArgumentNullException">execute is null.</exception>
        protected SyncCommandBase([NotNull] Action<T> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBase{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        protected SyncCommandBase([NotNull] Action<T> execute, [NotNull] Predicate<T> canExecute)
            : this(execute) =>
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

        /// <summary>
        ///     Initializes a new instance of the <see cref="SyncCommandBase{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        protected SyncCommandBase([NotNull] Action<T> execute, [NotNull] ICanExecute canExecute)
            : this(execute)
        {
            this.canExecute = t => canExecute.CanExecute();
        }

        /// <summary>
        /// Gets a value indicating whether this instance has can execute.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has can execute; otherwise, <c>false</c>.
        /// </value>
        protected override bool HasCanExecute => this.canExecute != null;

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanExecute(T parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        /// <summary>
        ///     Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Execute(T parameter)
        {
            if (this.CanExecute(parameter))
            {
                this.execute(parameter);
            }
        }

        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// CanExecute result.
        /// </returns>
        protected sealed override bool CanExecute(object parameter) => this.CanExecute((T)parameter);

        /// <summary>
        /// Handle the internal invocation of <see cref="ICommand.Execute(object)" />.
        /// </summary>
        /// <param name="parameter">Command Parameter.</param>
        protected sealed override void Execute(object parameter) => this.Execute((T)parameter);
    }
}