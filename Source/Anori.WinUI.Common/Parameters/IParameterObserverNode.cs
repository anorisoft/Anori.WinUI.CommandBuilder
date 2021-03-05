// -----------------------------------------------------------------------
// <copyright file="IParameterObserverNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    internal interface IParameterObserverNode
    {
        public void UnsubscribeListener();

        public void SubscribeListenerFor(IReadOnlyParameter parameter);
    }
}