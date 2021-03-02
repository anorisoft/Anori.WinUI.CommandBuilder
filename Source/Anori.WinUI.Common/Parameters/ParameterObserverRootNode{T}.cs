// -----------------------------------------------------------------------
// <copyright file="ParameterObserverRootNode{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.Parameters
{
    using System;
    using System.Reflection;

    using Anorisoft.WinUI.Common.NotifyPropertyChangedObservers;

    internal class ParameterObserverSingleNode<T> : ParameterObserverEndNode<T>, IParameterObserverRootNode<T>
    {
        public ParameterObserverSingleNode(Action<T> action, object owner, IReadOnlyParameter<T> parameter)
            : base(action)
        {
            this.Owner = owner;
            this.Parameter = parameter;
        }

        /// <summary>
        ///     Gets the parameter.
        /// </summary>
        /// <value>
        ///     The parameter.
        /// </value>
        public IReadOnlyParameter<T> Parameter { get; }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public object Owner { get; }

        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Parameter);
        }
    }

    internal class ParameterObserverRootNode<T> : ParameterObserverNode<T>, IParameterObserverRootNode<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RootPropertyObserverNode" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <param name="owner">The owner.</param>
        public ParameterObserverRootNode(
            PropertyInfo propertyInfo,
            Action<T> action,
            object owner,
            IReadOnlyParameter changed,
            Action<T> getter)
            : base(propertyInfo, action)
        {
            this.Owner = owner;
            this.Changed = changed;
            this.Getter = getter;
        }

        /// <summary>
        ///     Gets the parameter.
        /// </summary>
        /// <value>
        ///     The parameter.
        /// </value>
        public Action<T> Getter { get; }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public object Owner { get; }

        /// <summary>
        /// Gets the changed.
        /// </summary>
        /// <value>
        /// The changed.
        /// </value>
        public IReadOnlyParameter Changed { get; }

        /// <summary>
        /// Subscribes the listener for owner.
        /// </summary>
        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Changed);
        }
    }
}