// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Async Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IAsyncCommand{T}" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableAsyncCommand{T}}" />
    public interface IActivatableAsyncCommand<in T> : IAsyncCommand<T>, IActivatable<IActivatableAsyncCommand<T>>
    {
    }
}