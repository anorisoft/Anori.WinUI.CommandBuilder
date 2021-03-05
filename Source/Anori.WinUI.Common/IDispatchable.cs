// -----------------------------------------------------------------------
// <copyright file="IDispatchable.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System.Windows.Threading;

    /// <summary>
    /// Dispatcher Interface.
    /// </summary>
    public interface IDispatchable
    {
        /// <summary>
        /// Gets the dispatcher.
        /// </summary>
        /// <value>
        /// The dispatcher.
        /// </value>
        Dispatcher Dispatcher { get; }
    }
}