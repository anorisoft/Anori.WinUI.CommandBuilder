// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.Common;

    /// <summary>
    ///     Activatable Async Command Interface.
    /// </summary>
    /// <seealso cref="IActivatable{TSelf}" />
    /// <seealso cref="IAsyncCommand" />
    public interface IActivatableAsyncCommand : IAsyncCommand, IActivatable<IActivatableAsyncCommand>
    {
    }
}