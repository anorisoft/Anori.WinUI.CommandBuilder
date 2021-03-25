// -----------------------------------------------------------------------
// <copyright file="IAsyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Async Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IAsyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute(T parameter);

        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The async taskresult.</returns>
        Task ExecuteAsync(T parameter);
    }
}