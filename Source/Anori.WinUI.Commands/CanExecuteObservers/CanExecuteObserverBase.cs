// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserverBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;

    using Anori.ExpressionObservers.Interfaces;
    using Anori.WinUI.Commands.Interfaces;

    /// <summary>
    /// CanExecute Observer Base.
    /// </summary>
    /// <typeparam name="TPropertyObserver">The type of the property observer.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.CanExecuteObservers.PropertyObserverBase{TPropertyObserver}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.ICanExecuteObserver" />
    internal abstract class CanExecuteObserverBase<TPropertyObserver> : PropertyObserverBase<TPropertyObserver>, ICanExecuteObserver
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