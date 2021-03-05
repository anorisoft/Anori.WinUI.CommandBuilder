// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencyAsyncCommand.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Concurrency Async Command Interface.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IConcurrencyAsyncCommand" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableConcurrencyAsyncCommand}" />
    public interface IActivatableConcurrencyAsyncCommand : IConcurrencyAsyncCommand,
                                                           IActivatable<IActivatableConcurrencyAsyncCommand>
    {
        /// <summary>
        ///     Executes this instance.
        /// </summary>
        void Execute();
    }
}