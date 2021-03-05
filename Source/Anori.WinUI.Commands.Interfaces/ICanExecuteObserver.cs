// -----------------------------------------------------------------------
// <copyright file="ICanExecuteObserver.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{

    /// <summary>
    /// CanExecute Observer Interface.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteSubject" />
    public interface ICanExecuteObserver : ICanExecuteSubject
    {
        /// <summary>
        /// Gets the can execute.
        /// </summary>
        /// <value>
        /// The can execute.
        /// </value>

    }
}