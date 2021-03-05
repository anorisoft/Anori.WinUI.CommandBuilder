// -----------------------------------------------------------------------
// <copyright file="IDispatchableContext.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System.Threading;

    /// <summary>
    /// Dispatchable Context Interface.
    /// </summary>
    public interface IDispatchableContext
    {
        /// <summary>
        /// Gets the synchronization context.
        /// </summary>
        /// <value>
        /// The synchronization context.
        /// </value>
        SynchronizationContext SynchronizationContext { get; }
    }
}