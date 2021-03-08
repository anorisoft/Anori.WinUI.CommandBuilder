// -----------------------------------------------------------------------
// <copyright file="IActivated.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    using System;

    /// <summary>
    ///     Activated Interface.
    /// </summary>
    public interface IActivated
    {
        /// <summary>
        ///     Occurs when [is active changed].
        /// </summary>
        event EventHandler<EventArgs<bool>> IsActiveChanged;

        /// <summary>
        ///     Gets a value indicating whether the object is active.
        /// </summary>
        /// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
        bool IsActive { get; }
    }
}