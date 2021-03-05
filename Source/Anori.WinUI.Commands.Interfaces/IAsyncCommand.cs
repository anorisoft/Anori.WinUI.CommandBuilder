// -----------------------------------------------------------------------
// <copyright file="IAsyncCommand.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using JetBrains.Annotations;

    using System.Threading.Tasks;

    /// <summary>
    ///     Async Command Interface.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IAsyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
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