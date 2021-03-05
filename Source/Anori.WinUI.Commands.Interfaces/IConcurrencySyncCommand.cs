// -----------------------------------------------------------------------
// <copyright file="IConcurrencySyncCommand.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;

namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    ///     Concurrency Sync Command Interface.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IConcurrencySyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        void Execute(CancellationToken token);

        /// <summary>
        ///     Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();

        /// <summary>
        ///     Executes this instance.
        /// </summary>
        void Execute();
    }
}