// -----------------------------------------------------------------------
// <copyright file="ISyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using JetBrains.Annotations;

    /// <summary>
    ///     Sync Command.
    /// </summary>
    /// <typeparam name="T">Parameter Type.</typeparam>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface ISyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute([CanBeNull] T parameter);

        /// <summary>
        ///     Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void Execute([CanBeNull] T parameter);
    }
}