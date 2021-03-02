// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;

    using CanExecuteChangedTests;

    public interface IReadOnlyParameter
    {
        object Value { get; }

        event EventHandler<EventArgs<object>> ValueChanged;
    }
}