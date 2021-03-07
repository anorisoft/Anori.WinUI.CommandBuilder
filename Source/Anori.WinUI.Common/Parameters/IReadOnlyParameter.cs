// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;

    public interface IReadOnlyParameter
    {
        object Value { get; }

        event EventHandler<EventArgs<object>> ValueChanged;
    }
}