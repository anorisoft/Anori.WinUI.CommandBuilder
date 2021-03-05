// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;

    using CanExecuteChangedTests;

    public interface IReadOnlyParameter<T> : IReadOnlyParameter
    {
        new T Value { get; }

        new event EventHandler<EventArgs<T>> ValueChanged;
    }
}