// -----------------------------------------------------------------------
// <copyright file="IParameterObserverNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using Anori.Parameters;

    /// <summary>
    /// The Parameter Observer Node interface.
    /// </summary>
    internal interface IParameterObserverNode
    {
        /// <summary>
        /// Unsubscribes the listener.
        /// </summary>
        public void UnsubscribeListener();

        /// <summary>
        /// Subscribes the listener for.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void SubscribeListenerFor(IReadOnlyParameter parameter);
    }
}