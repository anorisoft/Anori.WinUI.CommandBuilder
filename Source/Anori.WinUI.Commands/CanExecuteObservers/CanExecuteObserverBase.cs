// -----------------------------------------------------------------------
// <copyright file="CanExecuteObserverBase.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.CanExecuteObservers
{
    using System;

    using Anori.WinUI.Commands.Interfaces;

    public abstract class CanExecuteObserverBase : PropertyObserverBase, ICanExecuteObserver
    {
        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public Func<bool> CanExecute { get; protected set; }
    }
}