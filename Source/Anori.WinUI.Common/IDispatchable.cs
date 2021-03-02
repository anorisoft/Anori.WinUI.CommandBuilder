// -----------------------------------------------------------------------
// <copyright file="IDispatchable.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Windows.Threading;

namespace Anori.WinUI.Common
{

    public interface IDispatchable
    {
        Dispatcher Dispatcher { get; }
    }
}