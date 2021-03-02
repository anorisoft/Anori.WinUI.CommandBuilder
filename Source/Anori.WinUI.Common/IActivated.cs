﻿// -----------------------------------------------------------------------
// <copyright file="IActivated.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;

    using CanExecuteChangedTests;

    public interface IActivated
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
        bool IsActive { get; }

        /// <summary>
        ///     Notifies that the value for <see cref="IsActive" /> property has changed.
        /// </summary>
        event EventHandler<EventArgs<bool>> IsActiveChanged;
    }
}