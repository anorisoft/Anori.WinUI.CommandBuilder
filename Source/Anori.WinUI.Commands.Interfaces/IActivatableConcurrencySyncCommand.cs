// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencySyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Concurrency Sync Command Interface.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IConcurrencySyncCommand" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableConcurrencySyncCommand}" />
    public interface IActivatableConcurrencySyncCommand : IConcurrencySyncCommand,
                                                          IActivatable<IActivatableConcurrencySyncCommand>
    {
    }
}