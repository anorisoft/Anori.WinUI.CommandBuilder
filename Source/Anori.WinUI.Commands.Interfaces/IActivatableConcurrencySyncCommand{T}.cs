// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencySyncCommand{T}.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Concurrency Sync Command Interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IConcurrencySyncCommand{T}" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableConcurrencySyncCommand{T}}" />
    public interface IActivatableConcurrencySyncCommand<in T> : IConcurrencySyncCommand<T>,
                                                                IActivatable<IActivatableConcurrencySyncCommand<T>>
    {
    }
}