// -----------------------------------------------------------------------
// <copyright file="IParameterObserverRootNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    internal interface IParameterObserverRootNode<out TOwner> : IParameterObserverNode
    {
        TOwner Owner { get; }

        void SubscribeListenerForOwner();
    }
}