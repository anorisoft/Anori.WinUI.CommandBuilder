// -----------------------------------------------------------------------
// <copyright file="IConcurrencySyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using System.Threading;

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