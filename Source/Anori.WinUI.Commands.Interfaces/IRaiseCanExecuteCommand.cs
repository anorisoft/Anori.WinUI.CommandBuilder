// -----------------------------------------------------------------------
// <copyright file="IRaiseCanExecuteCommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
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