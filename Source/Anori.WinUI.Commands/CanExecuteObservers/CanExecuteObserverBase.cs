// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserverBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using Anori.WinUI.Commands.Interfaces;
    using System;

    /// <summary>
    ///     CanExecute Observer Base.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.PropertyObserverBase" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteObserver" />
    internal abstract class CanExecuteObserverBase : PropertyObserverBase, ICanExecuteObserver
    {
        /// <summary>
        ///     Gets or sets the can execute.
        /// </summary>
        /// <value>
        ///     The can execute.
        /// </value>
        public Func<bool> CanExecute { get; protected set; }
    }
}