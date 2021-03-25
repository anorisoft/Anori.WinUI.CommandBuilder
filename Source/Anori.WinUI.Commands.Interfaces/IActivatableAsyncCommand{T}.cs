// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.Common;

    /// <summary>
    ///     Activatable Async Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="IAsyncCommand{T}" />
    /// <seealso cref="IActivatable" />
    public interface IActivatableAsyncCommand<in T> : IAsyncCommand<T>, IActivatable<IActivatableAsyncCommand<T>>
    {
    }
}