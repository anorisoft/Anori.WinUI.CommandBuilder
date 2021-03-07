// -----------------------------------------------------------------------
// <copyright file="ICanExecute.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using System;

    using JetBrains.Annotations;

    /// <summary>
    ///     CanExecute Interface.
    /// </summary>
    public interface ICanExecute
    {
        /// <summary>
        ///     Gets the can execute.
        /// </summary>
        /// <value>
        ///     The can execute.
        /// </value>
        [NotNull]
        Func<bool> CanExecute { get; }
    }
}