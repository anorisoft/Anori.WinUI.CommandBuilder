﻿// -----------------------------------------------------------------------
// <copyright file="ParameterObserverEndNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using CanExecuteChangedTests;

    using System;

    /// <summary>
    ///     Parameter Observer End Node
    /// </summary>
    /// <seealso cref="Anori.WinUI.Common.Parameters.IParameterObserverNode" />
    internal class ParameterObserverEndNode : IParameterObserverNode

    {
        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action action;

        /// <summary>
        ///     The notify property changed
        /// </summary>
        private IReadOnlyParameter parameter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserverEndNode" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">propertyInfo</exception>
        public ParameterObserverEndNode(Action action) => this.action = () => action.Raise();

        /// <summary>
        ///     Subscribes the listener for.
        /// </summary>
        /// <param name="parameter">The property changed.</param>
        public void SubscribeListenerFor(IReadOnlyParameter parameter)
        {
            this.parameter = parameter;
            this.parameter.ValueChanged += this.OnValueChanged;
        }

        /// <summary>
        ///     Unsubscribes the listener.
        /// </summary>
        public void UnsubscribeListener()
        {
            if (this.parameter != null)
            {
                this.parameter.ValueChanged -= this.OnValueChanged;
            }
        }

        /// <summary>
        ///     Called when [value changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs{System.Object}" /> instance containing the event data.</param>
        private void OnValueChanged(object sender, EventArgs<object> e) => this.action.Raise();
    }
}