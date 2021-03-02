// -----------------------------------------------------------------------
// <copyright file="RootPropertyObserverNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.NotifyPropertyChangedObservers
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    internal class RootPropertyObserverNode : PropertyObserverNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootPropertyObserverNode"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <param name="owner">The owner.</param>
        public RootPropertyObserverNode(PropertyInfo propertyInfo, Action action, INotifyPropertyChanged owner)
            : base(propertyInfo, action)
        {
            this.Owner = owner;
        }

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public INotifyPropertyChanged Owner { get; }

        /// <summary>
        /// Subscribes the listener for owner.
        /// </summary>
        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Owner);
        }
    }
}