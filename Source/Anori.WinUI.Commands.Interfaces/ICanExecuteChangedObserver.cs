// -----------------------------------------------------------------------
// <copyright file="ICanExecuteChangedObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    /// CanExecute Changed Observer Interface.
    /// </summary>
    public interface ICanExecuteChangedObserver
    {
        /// <summary>
        /// Called when [can execute changed].
        /// </summary>
        void RaisePropertyChanged();
    }
}