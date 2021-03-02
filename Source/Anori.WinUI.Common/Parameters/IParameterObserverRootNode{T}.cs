// -----------------------------------------------------------------------
// <copyright file="IParameterObserverRootNode{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    internal interface IParameterObserverRootNode<T> : IParameterObserverNode<T>
    {
        object Owner { get; }

        void SubscribeListenerForOwner();
    }
}