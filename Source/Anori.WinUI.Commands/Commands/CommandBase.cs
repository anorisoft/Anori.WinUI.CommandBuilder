// -----------------------------------------------------------------------
// <copyright file="CommandBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Input;

    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    ///     Command Base.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    /// <seealso cref="Anori.WinUI.Common.IDispatchableContext" />
    internal abstract class CommandBase : ICommand, IDispatchableContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBase" /> class.
        /// </summary>
        protected CommandBase() => this.SynchronizationContext = SynchronizationContext.Current;

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public abstract event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Gets the synchronization context.
        /// </summary>
        /// <value>
        ///     The synchronization context.
        /// </value>
        public SynchronizationContext SynchronizationContext { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance has can execute.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has can execute; otherwise, <c>false</c>.
        /// </value>
        protected abstract bool HasCanExecute { get; }

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
        /// </returns>
        protected abstract bool CanExecute([CanBeNull] object parameter);

        /// <summary>
        ///     Handle the internal invocation of <see cref="ICommand.Execute(object)" />.
        /// </summary>
        /// <param name="parameter">Command Parameter.</param>
        protected abstract void Execute([CanBeNull] object parameter);

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
        [DebuggerStepThrough]
        bool ICommand.CanExecute([CanBeNull] object parameter) => this.CanExecute(parameter);

        /// <inheritdoc />
        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to <see langword="null" />.
        /// </param>
        void ICommand.Execute([CanBeNull] object parameter) => this.Execute(parameter);
    }
}