// -----------------------------------------------------------------------
// <copyright file="DirectCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

[assembly:
    InternalsVisibleTo(
        "Anori.WinUI.CanExecuteChangedTests, "
        + "PublicKey="
        + "0024000004800000940000000602000000240000525341310004000001000100a520658730454f"
        + "b71a447c87dcb713412746dd0b04a1a1afea4067c991bc260f965eb7481148266358fc635ca839"
        + "5b78375f4cf69097188ab3cb2c27a52d1812872edd13157ed84b651f0462accdb31b65dfc4a352"
        + "2c8ada5c895c24738b342f759ac7ad33086e44a631a8884e1e6eb526e7e4e7170a52b723fe3c0d"
        + "db55b3c2")]
[assembly:
    InternalsVisibleTo(
        "Anori.WinUI.Commands.Tests, "
        + "PublicKey="
        + "0024000004800000940000000602000000240000525341310004000001000100a520658730454f"
        + "b71a447c87dcb713412746dd0b04a1a1afea4067c991bc260f965eb7481148266358fc635ca839"
        + "5b78375f4cf69097188ab3cb2c27a52d1812872edd13157ed84b651f0462accdb31b65dfc4a352"
        + "2c8ada5c895c24738b342f759ac7ad33086e44a631a8884e1e6eb526e7e4e7170a52b723fe3c0d"
        + "db55b3c2")]
[assembly:
    InternalsVisibleTo(
        "Anori.WinUI.Commands.GUITest, "
        + "PublicKey="
        + "0024000004800000940000000602000000240000525341310004000001000100a520658730454f"
        + "b71a447c87dcb713412746dd0b04a1a1afea4067c991bc260f965eb7481148266358fc635ca839"
        + "5b78375f4cf69097188ab3cb2c27a52d1812872edd13157ed84b651f0462accdb31b65dfc4a352"
        + "2c8ada5c895c24738b342f759ac7ad33086e44a631a8884e1e6eb526e7e4e7170a52b723fe3c0d"
        + "db55b3c2")]

namespace Anori.WinUI.Commands.Commands
{
    using System;

    using Anori.Extensions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    /// <summary>
    /// Direct Command.
    /// </summary>
    /// <seealso cref="SyncCommandBase" />
    /// <seealso cref="IRaiseCanExecuteCommand" />
    /// <seealso cref="System.Windows.Input.ICommand" />
    internal class DirectCommand : SyncCommandBase, IRaiseCanExecuteCommand
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
        /// <exception cref="System.ArgumentNullException">canExecute is null.</exception>
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