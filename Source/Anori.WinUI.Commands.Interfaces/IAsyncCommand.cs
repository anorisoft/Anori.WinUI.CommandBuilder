// -----------------------------------------------------------------------
// <copyright file="IAsyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    /// <summary>
    ///     Async Command Interface.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IAsyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <returns>Async task result.</returns>
        [NotNull]
        Task ExecuteAsync();

        /// <summary>
        ///     Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();
    }
}