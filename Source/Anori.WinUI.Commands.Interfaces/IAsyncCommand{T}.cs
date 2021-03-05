// -----------------------------------------------------------------------
// <copyright file="IAsyncCommand{T}.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Async Command Interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface IAsyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        ///     Executes the asynchronous.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        Task ExecuteAsync(T parameter);

        /// <summary>
        ///     Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///     <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute(T parameter);
    }
}