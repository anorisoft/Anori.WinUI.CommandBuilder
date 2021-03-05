// -----------------------------------------------------------------------
// <copyright file="SyncCommandBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Anori.WinUI.Commands.Interfaces;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Commands
{
    /// <summary>
    ///     A Command whose sole purpose is to relay its functionality to other objects by invoking delegates.
    ///     The default return value for the CanExecute method is 'true'.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public abstract class SyncCommandBase : CommandBase, Interfaces.ISyncCommand
    {
        /// <summary>
        ///     The can execute
        /// </summary>
        [CanBeNull] private readonly Func<bool> canExecute;

        /// <summary>
        ///     The execute
        /// </summary>
        [NotNull] private readonly Action execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBase" /> class and the Command can
        /// always be executed.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        protected SyncCommandBase(
            [NotNull] Action execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBase"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        protected SyncCommandBase(
            [NotNull] Action execute,
            [NotNull] Func<bool> canExecute)
            : this(execute) =>
            this.canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBase"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        protected SyncCommandBase(
            [NotNull] Action execute,
            [NotNull] ICanExecuteSubject canExecuteSubject)
            : this(execute)
        {
            if (canExecuteSubject == null)
            {
                throw new ArgumentNullException(nameof(canExecuteSubject));
            }

            this.canExecute = canExecuteSubject.CanExecute;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has can execute.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has can execute; otherwise, <c>false</c>.
        /// </value>
        protected override bool HasCanExecute => this.canExecute != null;

        /// <summary>
        ///     Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanExecute() => this.canExecute == null || this.canExecute();

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
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        protected sealed override bool CanExecute(object parameter) => this.CanExecute();

        /// <summary>
        ///     Handle the internal invocation of <see cref="System.Windows.Input.ICommand.Execute" />
        /// </summary>
        /// <param name="parameter">Command Parameter</param>
        protected sealed override void Execute(object parameter) => this.execute();
    }
}