// -----------------------------------------------------------------------
// <copyright file="IParameterObserverRootNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
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