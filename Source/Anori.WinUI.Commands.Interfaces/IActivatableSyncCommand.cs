// -----------------------------------------------------------------------
// <copyright file="IActivatableSyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Sync Command Interface.
    /// </summary>
    /// <seealso cref="ISyncCommand" />
    /// <seealso cref="IActivatable{TSelf}" />
    public interface IActivatableSyncCommand : ISyncCommand, IActivatable<IActivatableSyncCommand>
    {
    }
}