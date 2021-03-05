// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCommand.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WinUI.Common;

namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    ///     Activatable Async Command Interface.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IAsyncCommand" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableAsyncCommand}" />
    public interface IActivatableAsyncCommand : IAsyncCommand, IActivatable<IActivatableAsyncCommand>
    {
    }
}