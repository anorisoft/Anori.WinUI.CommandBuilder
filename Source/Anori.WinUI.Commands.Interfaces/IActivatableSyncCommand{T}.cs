// -----------------------------------------------------------------------
// <copyright file="IActivatableSyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Sync Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ISyncCommand{T}" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableSyncCommand{T}}" />
    public interface IActivatableSyncCommand<in T> : ISyncCommand<T>, IActivatable<IActivatableSyncCommand<T>>
    {
    }
}