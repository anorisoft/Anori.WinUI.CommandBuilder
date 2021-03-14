// -----------------------------------------------------------------------
// <copyright file="ParameterObserverNode.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;
    using System.Reflection;

    using Anori.Common;
    using Anori.Extensions;

    /// <summary>
    ///     Represents each node of nested properties expression and takes care of
    ///     subscribing/unsubscribing INotifyPropertyChanged.PropertyChanged listeners on it.
    /// </summary>
    internal class ParameterObserverNode : IParameterObserverNode

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
        ///     Initializes a new instance of the <see cref="ParameterObserverNode" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">propertyInfo</exception>
        public ParameterObserverNode(PropertyInfo propertyInfo, Action action)
        {
            this.PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
            this.action = () =>
                {
                    action.Raise();
                    if (this.Next == null)
                    {
                        return;
                    }

                    this.Next.UnsubscribeListener();
                    this.GenerateNextNode();
                };
        }

        /// <summary>
        ///     Gets or sets the next.
        /// </summary>
        /// <value>
        ///     The next.
        /// </value>
        public IParameterObserverNode Next { get; set; }

        /// <summary>
        ///     Gets the property information.
        /// </summary>
        /// <value>
        ///     The property information.
        /// </value>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        ///     Subscribes the listener for.
        /// </summary>
        /// <param name="parameter">The property changed.</param>
        public void SubscribeListenerFor(IReadOnlyParameter parameter)
        {
            this.parameter = parameter;
            this.parameter.ValueChanged += this.OnValueChanged;

            if (this.Next != null)
            {
                this.GenerateNextNode();
            }
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

            this.Next?.UnsubscribeListener();
        }

        /// <summary>
        ///     Generates the next node.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Trying to subscribe ValueChanged listener in object that "
        ///     + $"owns '{this.Next.PropertyInfo.Name}' property, but the object does not implements IReadOnlyParameter.
        /// </exception>
        private void GenerateNextNode()
        {
            var value = this.parameter.Value;
            if (value == null)
            {
                return;
            }

            var nextParameter = this.PropertyInfo.GetValue(value);
            if (nextParameter == null)
            {
                return;
            }

            if (!(nextParameter is IReadOnlyParameter parameter1))
            {
                if (this.Next is ParameterObserverNode next)
                {
                    throw new InvalidOperationException(
                        "Trying to subscribe ValueChanged listener in object that "
                        + $"owns '{next.PropertyInfo.Name}' property, but the object does not implements IReadOnlyParameter.");
                }

                throw new InvalidOperationException(
                    "Trying to subscribe ValueChanged listener in object that, but the object does not implements IReadOnlyParameter.");
            }

            this.Next.SubscribeListenerFor(parameter1);
        }

        /// <summary>
        ///     Called when [value changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs{ôbject}" /> instance containing the event data.</param>
        private void OnValueChanged(object sender, EventArgs<object> e) => this.action.Raise();
    }
}