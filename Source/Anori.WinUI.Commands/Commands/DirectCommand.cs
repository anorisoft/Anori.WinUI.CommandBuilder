// -----------------------------------------------------------------------
// <copyright file="DirectCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;

    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    /// Direct Command.
    /// </summary>
    /// <seealso cref="SyncCommandBase" />
    /// <seealso cref="IRaiseCanExecuteCommand" />
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class DirectCommand : SyncCommandBase, IRaiseCanExecuteCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="System.ArgumentNullException">execute is null.</exception>
        public DirectCommand([NotNull] Action execute)
            : base(execute)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <exception cref="System.ArgumentNullException">canExecute</exception>
        public DirectCommand([NotNull] Action execute, [NotNull] Func<bool> canExecute)
            : base(execute, canExecute)
        {
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public sealed override event EventHandler CanExecuteChanged
        {
            add => this.Subscribe(value);
            remove => this.Unsubscribe(value);
        }

        /// <summary>
        ///     Occurs when [can execute changed internal].
        /// </summary>
#pragma warning disable S3264 // Events should be invoked
        private event EventHandler CanExecuteChangedInternal;

#pragma warning restore S3264 // Events should be invoked

        /// <summary>
        ///     Called when [can execute changed].
        /// </summary>
        public void RaiseCanExecuteChanged() => this.CanExecuteChangedInternal.RaiseEmpty(this);

        /// <summary>
        /// Subscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Subscribe(EventHandler value)
        {
            if (this.HasCanExecute)
            {
                this.CanExecuteChangedInternal += value;
            }
        }

        /// <summary>
        /// Unsubscribes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void Unsubscribe(EventHandler value)
        {
            if (this.HasCanExecute)
            {
                this.CanExecuteChangedInternal -= value;
            }
        }
    }
}