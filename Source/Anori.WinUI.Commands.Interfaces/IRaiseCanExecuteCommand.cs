// -----------------------------------------------------------------------
// <copyright file="IRaiseCanExecuteCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    public interface IRaiseCanExecuteCommand
    {
        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}