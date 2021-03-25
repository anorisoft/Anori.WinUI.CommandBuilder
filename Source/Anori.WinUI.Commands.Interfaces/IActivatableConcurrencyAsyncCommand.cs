// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencyAsyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.Common;

    /// <summary>
    ///     Activatable Concurrency Async Command Interface.
    /// </summary>
    /// <seealso cref="IConcurrencyAsyncCommand" />
    /// <seealso cref="IActivatable" />
    public interface IActivatableConcurrencyAsyncCommand : IConcurrencyAsyncCommand,
                                                           IActivatable<IActivatableConcurrencyAsyncCommand>
    {
        /// <summary>
        ///     Executes this instance.
        /// </summary>
        void Execute();
    }
}