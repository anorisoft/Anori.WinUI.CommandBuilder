// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Async Command Interface.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.IAsyncCommand" />
    /// <seealso cref="Anori.WinUI.Common.IActivatable{Anori.WinUI.Commands.Interfaces.IActivatableAsyncCommand}" />
    public interface IActivatableAsyncCommand : IAsyncCommand, IActivatable<IActivatableAsyncCommand>
    {
    }
}