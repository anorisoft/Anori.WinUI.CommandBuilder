// -----------------------------------------------------------------------
// <copyright file="ParameterObserverEndNode{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    using CanExecuteChangedTests;

    using System;

    /// <summary>
    ///     Parameter Observer End Node
    /// </summary>
    /// <seealso cref="Anorisoft.WinUI.Common.Parameters.IParameterObserverNode" />
    internal class ParameterObserverEndNode<T> : IParameterObserverNode<T>

    {
        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action<T> action;

        /// <summary>
        ///     The notify property changed
        /// </summary>
        private IReadOnlyParameter<T> parameter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserverEndNode" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">propertyInfo</exception>
        public ParameterObserverEndNode(Action<T> action) => this.action = v => action.Raise(v);

        /// <summary>
        ///     Subscribes the listener for.
        /// </summary>
        /// <param name="parameter">The property changed.</param>
        public void SubscribeListenerFor(IReadOnlyParameter<T> parameter)
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
        private void OnValueChanged(object sender, EventArgs<T> e) => this.action.Raise(e.Value);
    }
}