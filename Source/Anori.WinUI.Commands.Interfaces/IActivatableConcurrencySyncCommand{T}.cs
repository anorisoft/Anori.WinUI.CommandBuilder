// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencySyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Concurrency Sync Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IConcurrencySyncCommand{T}" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableConcurrencySyncCommand{T}}" />
    public interface IActivatableConcurrencySyncCommand<in T> : IConcurrencySyncCommand<T>,
                                                                IActivatable<IActivatableConcurrencySyncCommand<T>>
    {
    }
}