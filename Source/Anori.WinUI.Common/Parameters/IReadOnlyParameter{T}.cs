// -----------------------------------------------------------------------
// <copyright file="IReadOnlyParameter{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
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