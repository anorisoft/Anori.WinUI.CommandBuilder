// -----------------------------------------------------------------------
// <copyright file="IParameterObserverRootNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    /// <summary>
    /// The Parameter Observer Root Node interface.
    /// </summary>
    /// <typeparam name="TOwner">The type of the owner.</typeparam>
    /// <seealso cref="Anori.WinUI.Common.Parameters.IParameterObserverNode" />
    internal interface IParameterObserverRootNode<out TOwner> : IParameterObserverNode
    {
        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        TOwner Owner { get; }

        /// <summary>
        /// Subscribes the listener for owner.
        /// </summary>
        void SubscribeListenerForOwner();
    }
}