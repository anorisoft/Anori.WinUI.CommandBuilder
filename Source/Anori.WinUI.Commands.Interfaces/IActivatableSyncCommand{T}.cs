// -----------------------------------------------------------------------
// <copyright file="IActivatableSyncCommand{T}.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Sync Command Interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ISyncCommand{T}" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableSyncCommand{T}}" />
    public interface IActivatableSyncCommand<in T> : ISyncCommand<T>, IActivatable<IActivatableSyncCommand<T>>
    {
    }
}