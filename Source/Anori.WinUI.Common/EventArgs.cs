// -----------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace CanExecuteChangedTests
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T value)
        {
            this.Value = value;
        }

        public T Value { get; }
    }
}