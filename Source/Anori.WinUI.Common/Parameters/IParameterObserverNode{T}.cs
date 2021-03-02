// -----------------------------------------------------------------------
// <copyright file="IParameterObserverNode{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    internal interface IParameterObserverNode<T>
    {
        public void UnsubscribeListener();

        public void SubscribeListenerFor(IReadOnlyParameter<T> parameter);
    }
}