// -----------------------------------------------------------------------
// <copyright file="IConcurrencyAsyncCommand{T}.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{

    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    /// <summary>
    ///     Concurrency Async Command Interface.
    /// </summary>
    /// <typeparam name="T">The Type.</typeparam>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IConcurrencyAsyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Gets a value indicating whether this instance is executing.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is executing; otherwise, <c>false</c>.
        /// </value>
        bool IsExecuting { get; }

        /// <summary>
        ///     Gets the cancel command.
        /// </summary>
        /// <value>
        ///     The cancel command.
        /// </value>
        [NotNull]
        ISyncCommand CancelCommand { get; }

        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="token">The token.</param>
        /// <returns>The Task.</returns>
        [NotNull]
        Task ExecuteAsync([CanBeNull] T parameter, CancellationToken token);

        /// <summary>
        ///     Determines whether this instance can execute.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute([CanBeNull] T parameter);
    }
}