// -----------------------------------------------------------------------
// <copyright file="IExecutable.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    /// Executable Interface.
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is executing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is executing; otherwise, <c>false</c>.
        /// </value>
        bool IsExecuting { get; }
    }
}