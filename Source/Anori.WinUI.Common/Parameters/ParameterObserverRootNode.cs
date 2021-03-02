// -----------------------------------------------------------------------
// <copyright file="ParameterObserverRootNode.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;
    using System.Reflection;

    internal class ParameterObserverSingleNode<TOwner> : ParameterObserverEndNode, IParameterObserverRootNode<TOwner>
    {
        public ParameterObserverSingleNode(Action action, TOwner owner, IReadOnlyParameter parameter)
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
        public IReadOnlyParameter Parameter { get; }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public TOwner Owner { get; }

        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Parameter);
        }
    }

    internal class ParameterObserverRootNode<TOwner> : ParameterObserverNode, IParameterObserverRootNode<TOwner>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RootPropertyObserverNode" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="action">The action.</param>
        /// <param name="owner">The owner.</param>
        public ParameterObserverRootNode(
            PropertyInfo propertyInfo,
            Action action,
            TOwner owner,
            IReadOnlyParameter parameter)
            : base(propertyInfo, action)
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
        public IReadOnlyParameter Parameter { get; }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public TOwner Owner { get; }

        /// <summary>
        ///     Subscribes the listener for owner.
        /// </summary>
        public void SubscribeListenerForOwner()
        {
            this.SubscribeListenerFor(this.Parameter);
        }
    }
}