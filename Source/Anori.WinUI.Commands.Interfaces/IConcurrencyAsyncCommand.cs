// -----------------------------------------------------------------------
// <copyright file="IConcurrencyAsyncCommand.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using JetBrains.Annotations;

using System.Threading;
using System.Threading.Tasks;

namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    ///     Concurrency Async Command Interface.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IConcurrencyAsyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Gets the is executing.
        /// </summary>
        /// <value>
        ///     The is executing.
        /// </value>
        bool IsExecuting { get; }

        /// <summary>
        ///     Gets or sets the cancel command.
        /// </summary>
        /// <value>
        ///     The cancel command.
        /// </value>
        ISyncCommand CancelCommand { get; }

        /// <summary>
        ///     Gets a value indicating whether [was canceled].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [was canceled]; otherwise, <c>false</c>.
        /// </value>
        bool WasCanceled { get; }

        /// <summary>
        ///     Gets a value indicating whether [was successfuly].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [was successfuly]; otherwise, <c>false</c>.
        /// </value>
        bool WasSuccessfuly { get; }

        /// <summary>
        ///     Gets a value indicating whether [was faulty].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [was faulty]; otherwise, <c>false</c>.
        /// </value>
        bool WasFaulty { get; }

        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task ExecuteAsync(CancellationToken token);

        /// <summary>
        ///     Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();
    }
}