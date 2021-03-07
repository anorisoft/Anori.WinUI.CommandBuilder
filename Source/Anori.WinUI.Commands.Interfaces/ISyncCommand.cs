// -----------------------------------------------------------------------
// <copyright file="ISyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    ///     Sync Command Interface.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface ISyncCommand : System.Windows.Input.ICommand
    {
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