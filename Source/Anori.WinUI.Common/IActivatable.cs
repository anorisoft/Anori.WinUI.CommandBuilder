// -----------------------------------------------------------------------
// <copyright file="IActivatable.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    /// <summary>
    /// </summary>
    public interface IActivatable : IActivated

    {
        /// <summary>
        ///     Activates this instance.
        /// </summary>
        void Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        void Deactivate();
    }

    public interface IActivatable<out TSelf> : IActivated

    {
        /// <summary>
        ///     Activates this instance.
        /// </summary>
        TSelf Activate();

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        TSelf Deactivate();
    }
}