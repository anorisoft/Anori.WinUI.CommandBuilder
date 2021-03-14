// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencySyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.Common;
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Concurrency Sync Command Interface.
    /// </summary>
    /// <seealso cref="IConcurrencySyncCommand" />
    /// <seealso cref="IActivatable" />
    public interface IActivatableConcurrencySyncCommand : IConcurrencySyncCommand,
                                                          IActivatable<IActivatableConcurrencySyncCommand>
    {
    }
}