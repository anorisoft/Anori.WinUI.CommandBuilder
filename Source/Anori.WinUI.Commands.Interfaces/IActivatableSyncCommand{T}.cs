// -----------------------------------------------------------------------
// <copyright file="IActivatableSyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.Common;

    /// <summary>
    ///     Activatable Sync Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="ISyncCommand{T}" />
    /// <seealso cref="IActivatable" />
    public interface IActivatableSyncCommand<in T> : ISyncCommand<T>, IActivatable<IActivatableSyncCommand<T>>
    {
    }
}