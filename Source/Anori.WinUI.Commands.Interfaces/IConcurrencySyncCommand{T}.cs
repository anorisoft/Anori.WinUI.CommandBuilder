// -----------------------------------------------------------------------
// <copyright file="IConcurrencySyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using System.Threading;

    using JetBrains.Annotations;

    /// <summary>
    ///     Concurrency Sync Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IConcurrencySyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="token">The token.</param>
        void Execute([CanBeNull] T parameter, CancellationToken token);

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