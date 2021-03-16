// -----------------------------------------------------------------------
// <copyright file="ParameterObserverBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Anori.Parameters;

    using JetBrains.Annotations;

    /// <summary>
    ///     Provide a way to observe property changes of INotifyPropertyChanged objects and invokes a
    ///     custom action when the PropertyChanged event is fired.
    /// </summary>
    public abstract class ParameterObserverBase<TOwner> : IEquatable<ParameterObserverBase<TOwner>>, IDisposable
    {
        /// <summary>
        ///     The owner string
        /// </summary>
        private const string OwnerString = "owner";

        /// <summary>
        ///     The parameterObserverRoot node
        /// </summary>
        private readonly IParameterObserverRootNode<TOwner> parameterObserverRootNode;

        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression propertyExpression;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     propertyExpression
        ///     or
        ///     action
        /// </exception>
        protected ParameterObserverBase([NotNull] Expression propertyExpression)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.ExpressionString = propertyExpression.ToString();
            (this.parameterObserverRootNode, this.ExpressionString) = this.CreateChain();
            this.Owner = this.parameterObserverRootNode.Owner;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     propertyExpression
        ///     or
        ///     owner
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        ///     propertyExpression
        ///     or
        ///     action
        ///     or
        ///     owner
        /// </exception>
        protected ParameterObserverBase([NotNull] TOwner owner, [NotNull] Expression propertyExpression)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            (this.parameterObserverRootNode, this.ExpressionString) = this.CreateChain(owner);
        }

        /// <summary>
        ///     Gets the expression string.
        /// </summary>
        /// <value>
        ///     The expression string.
        /// </value>
        public string ExpressionString { get; }

        /// <summary>
        ///     Gets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        [CanBeNull]
        public TOwner Owner { get; }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public bool Equals(ParameterObserverBase<TOwner> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(this.ExpressionString, other.ExpressionString) && Equals(this.Owner, other.Owner);
        }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ParameterObserver)obj);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((this.ExpressionString != null ? this.ExpressionString.GetHashCode() : 0) * 397)
                       ^ (this.Owner != null ? this.Owner.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        public void SubscribeListener()
        {
            this.parameterObserverRootNode.SubscribeListenerForOwner();
        }

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        public void UnsubscribeListener()
        {
            this.parameterObserverRootNode.UnsubscribeListener();
        }

        /// <summary>
        ///     Calls the action.
        /// </summary>
        protected abstract void CallAction();

        /// <summary>
        ///     Creates the graph.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">
        ///     Operation not supported for the given expression type {expression.Type}. "
        ///     + "Only MemberExpression and ConstantExpression are currently supported.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Trying to subscribe PropertyChanged listener in object that "
        ///     + $"owns '{rootPropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        ///     Operation not supported for the given expression type. "
        ///     + "Only MemberExpression and ConstantExpression are currently supported.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        ///     Trying to subscribe PropertyChanged listener in object that "
        ///     + $"owns '{parameterObserverRootNode.PropertyInfo.Name}' property, but the object does not implements
        ///     INotifyPropertyChanged.
        /// </exception>
        private (ParameterObserverRootNode<TOwner>, string) CreateChain()
        {
            var expression = this.propertyExpression;
            var expressionString = "";
            var propertyInfos = new Stack<PropertyInfo>();
            while (expression is MemberExpression memberExpression)
            {
                if (!(memberExpression.Member is PropertyInfo propertyInfo))
                {
                    continue;
                }

                expression = memberExpression.Expression;
                propertyInfos.Push(propertyInfo);
            }

            if (!(expression is ConstantExpression constantExpression))
            {
                throw new NotSupportedException(
                    $"Operation not supported for the given expression type {expression.Type}. "
                    + "Only MemberExpression and ConstantExpression are currently supported.");
            }

            var rootPropertyInfo = propertyInfos.Pop();
            expressionString = OwnerString + this.propertyExpression.ToString()
                                   .Remove(0, constantExpression.ToString().Length);

            if (!(constantExpression.Value is IReadOnlyParameter parameter))
            {
                throw new InvalidOperationException(
                    "Trying to subscribe PropertyChanged listener in object that "
                    + $"owns '{rootPropertyInfo.Name}' property, but the object does not implements IReadOnlyParameter.");
            }

            if (!(constantExpression.Value is TOwner owner))
            {
                throw new InvalidOperationException(
                    "Trying to subscribe PropertyChanged listener in object that "
                    + $"owns '{rootPropertyInfo.Name}' property, but the object does not of type '{typeof(TOwner).Name}'.");
            }

            var root = new ParameterObserverRootNode<TOwner>(rootPropertyInfo, this.CallAction, owner, parameter);

            ParameterObserverNode previousNode = root;
            foreach (var currentNode in propertyInfos.Select(name => new ParameterObserverNode(name, this.CallAction)))
            {
                previousNode.Next = currentNode;
                previousNode = currentNode;
            }

            return (root, expressionString);
        }

        /// <summary>
        ///     Creates the chain.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">
        ///     Operation not supported for the given expression type {expression.Type}. "
        ///     + "Only MemberExpression and ConstantExpression are currently supported.
        /// </exception>
        private (IParameterObserverRootNode<TOwner>, string) CreateChain(TOwner owner)
        {
            var expression = this.propertyExpression;
            var expressionString = "";
            var propertyInfos = new Stack<PropertyInfo>();
            while (expression is MemberExpression memberExpression)
            {
                if (!(memberExpression.Member is PropertyInfo info))
                {
                    continue;
                }

                expression = memberExpression.Expression;
                propertyInfos.Push(info);
            }

            if (!(expression is ParameterExpression parameterExpression))
            {
                throw new NotSupportedException(
                    $"Operation not supported for the given expression type {expression.Type}. "
                    + "Only MemberExpression and ConstantExpression are currently supported.");
            }

            var rootPropertyInfo = propertyInfos.Pop();

            expressionString = OwnerString + this.propertyExpression.ToString()
                                   .Remove(0, parameterExpression.ToString().Length);

            ParameterObserverRootNode<TOwner> parameterObserverRoot;
            if (owner is IReadOnlyParameter p)
            {
                if (!propertyInfos.Any())
                {
                    return (new ParameterObserverSingleNode<TOwner>(this.CallAction, owner, p), expressionString);
                }

                parameterObserverRoot = new ParameterObserverRootNode<TOwner>(
                    rootPropertyInfo,
                    this.CallAction,
                    owner,
                    p);
            }
            else
            {
                if (!(rootPropertyInfo.GetValue(owner) is IReadOnlyParameter v))
                {
                    throw new Exception("No Parameter.");
                }

                if (!propertyInfos.Any())
                {
                    return (new ParameterObserverSingleNode<TOwner>(this.CallAction, owner, v), expressionString);
                }

                var propertyInfo = propertyInfos.Pop();
                if (typeof(IReadOnlyParameter).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    parameterObserverRoot = new ParameterObserverRootNode<TOwner>(
                        propertyInfo,
                        this.CallAction,
                        owner,
                        v);
                }
                else
                {
                    propertyInfo = propertyInfos.Pop();
                    if (typeof(IReadOnlyParameter).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        parameterObserverRoot = new ParameterObserverRootNode<TOwner>(
                            propertyInfo,
                            this.CallAction,
                            owner,
                            v);
                    }
                    else
                    {
                        throw new Exception("No Parameter 3.");
                    }
                }
            }

            ParameterObserverNode previousNode = parameterObserverRoot;
            foreach (var node in propertyInfos
                .Where(currentNode => typeof(IReadOnlyParameter).IsAssignableFrom(currentNode.PropertyType))
                .Select(currentNode => new ParameterObserverNode(currentNode, this.CallAction)))
            {
                previousNode.Next = node;
                previousNode = node;
            }

            var endNode = new ParameterObserverEndNode(this.CallAction);
            previousNode.Next = endNode;

            return (parameterObserverRoot, expressionString);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.UnsubscribeListener();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}