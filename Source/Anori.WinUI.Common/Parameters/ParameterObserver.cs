// -----------------------------------------------------------------------
// <copyright file="ParameterObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using JetBrains.Annotations;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public sealed class ParameterObserver<TValue, TOwner> : ParameterObserverBase<TOwner>
    {
        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action<TValue> action;

        /// <summary>
        ///     The property propertyGetter
        /// </summary>
        private readonly Func<TValue> propertyGetter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver{TValue, TOwner}" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     action
        ///     or
        ///     propertyGetter
        /// </exception>
        internal ParameterObserver(
            [NotNull] Expression<Func<TValue>> propertyExpression,
            [NotNull] Action<TValue> action)
            : base(propertyExpression?.Body)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.propertyGetter = propertyExpression.Compile();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver{TValue, TOwner}" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">action</exception>
        internal ParameterObserver(
            [NotNull] TOwner owner,
            [NotNull] Expression<Func<TOwner, TValue>> propertyExpression,
            [NotNull] Action<TValue> action)
            : base(owner, propertyExpression?.Body)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.propertyGetter = () =>
                propertyExpression.Compile()(this.Owner)
                ?? throw new ArgumentNullException(nameof(this.propertyGetter));
        }

        /// <summary>
        ///     Calls the action.
        /// </summary>
        protected override void CallAction() => this.action.Raise(this.propertyGetter());
    }

    /// <summary>
    ///     Provide a way to observe property changes of INotifyPropertyChanged objects and invokes a
    ///     custom action when the PropertyChanged event is fired.
    /// </summary>
    public sealed class ParameterObserver : IEquatable<ParameterObserver>, IDisposable
    {
        /// <summary>
        ///     The owner string
        /// </summary>
        private const string OwnerString = "owner";

        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action action;

        /// <summary>
        ///     The parameterObserverRoot node
        /// </summary>
        private readonly IParameterObserverRootNode<object> parameterObserverRootNode;

        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression propertyExpression;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException">
        ///     propertyExpression
        ///     or
        ///     action
        /// </exception>
        private ParameterObserver([NotNull] Expression propertyExpression, [NotNull] Action action)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.ExpressionString = propertyExpression.ToString();
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            (this.parameterObserverRootNode, this.ExpressionString) = this.CreateChain();
            this.Owner = this.parameterObserverRootNode.Owner;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterObserver" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     propertyExpression
        ///     or
        ///     action
        ///     or
        ///     owner
        /// </exception>
        private ParameterObserver(
            [NotNull] object owner,
            [NotNull] Expression propertyExpression,
            [NotNull] Action action)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
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
        public object Owner { get; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Unsubscribe();
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public bool Equals(ParameterObserver other)
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
        ///     Observes a property that implements INotifyPropertyChanged, and automatically calls a custom action on
        ///     property changed notifications. The given expression must be in this form: "() =&gt;
        ///     Prop.NestedProp.PropToObserve".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression">
        ///     Expression representing property to be observed. Ex.: "() =&gt;
        ///     Prop.NestedProp.PropToObserve".
        /// </param>
        /// <param name="action">Action to be invoked when PropertyChanged event occours.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterObserver Observes<T>(
            Expression<Func<T>> propertyExpression,
            Action action,
            bool isAutoSubscribe = true)
        {
            var observer = new ParameterObserver(propertyExpression.Body, action);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
        }

        /// <summary>
        ///     Observes the specified owner.
        /// </summary>
        /// <typeparam name="TOwner">The type of the owner.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="owner">The owner.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        /// <param name="isAutoSubscribe">if set to <c>true</c> [is automatic subscribe].</param>
        /// <returns></returns>
        public static ParameterObserver Observes<TOwner, T>(
            TOwner owner,
            Expression<Func<TOwner, T>> propertyExpression,
            Action action,
            bool isAutoSubscribe = true)
        {
            var observer = new ParameterObserver(owner, propertyExpression.Body, action);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
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
        public void Subscribe()
        {
            this.parameterObserverRootNode.SubscribeListenerForOwner();
        }

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        public void Unsubscribe()
        {
            this.parameterObserverRootNode.UnsubscribeListener();
        }

        /// <summary>
        ///     Creates the graph.
        /// </summary>
        /// <param name="expression">The expression.</param>
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
        private (ParameterObserverRootNode<object>, string) CreateChain()
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

            var owner = constantExpression.Value;
            IReadOnlyParameter parameter;
            if (constantExpression.Value is IReadOnlyParameter o)
            {
                parameter = o;
            }
            else if (rootPropertyInfo.GetValue(constantExpression.Value) is IReadOnlyParameter p)
            {
                parameter = p;
            }
            else
            {
                throw new InvalidOperationException(
                  "Trying to subscribe PropertyChanged listener in object that "
                  + $"owns '{rootPropertyInfo.Name}' property, but the object does not implements IReadOnlyParameter.");
            }

            var root = new ParameterObserverRootNode<object>(rootPropertyInfo, this.action, owner, parameter);

            ParameterObserverNode previousNode = root;
            foreach (var currentNode in propertyInfos.Select(name => new ParameterObserverNode(name, this.action)))
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
        private (IParameterObserverRootNode<object>, string) CreateChain(object owner)
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

            ParameterObserverRootNode<object> parameterObserverRoot;
            if (owner is IReadOnlyParameter p)
            {
                if (!propertyInfos.Any())
                {
                    return (new ParameterObserverSingleNode<object>(this.action, owner, p), expressionString);
                }

                parameterObserverRoot = new ParameterObserverRootNode<object>(rootPropertyInfo, this.action, owner, p);
            }
            else
            {
                if (!(rootPropertyInfo.GetValue(owner) is IReadOnlyParameter v))
                {
                    throw new Exception("No Parameter.");
                }

                if (!propertyInfos.Any())
                {
                    return (new ParameterObserverSingleNode<object>(this.action, owner, v), expressionString);
                }

                var propertyInfo = propertyInfos.Pop();
                if (typeof(IReadOnlyParameter).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    parameterObserverRoot = new ParameterObserverRootNode<object>(propertyInfo, this.action, owner, v);
                }
                else
                {
                    propertyInfo = propertyInfos.Pop();
                    if (typeof(IReadOnlyParameter).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        parameterObserverRoot = new ParameterObserverRootNode<object>(
                            propertyInfo,
                            this.action,
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
                .Select(currentNode => new ParameterObserverNode(currentNode, this.action)))
            {
                previousNode.Next = node;
                previousNode = node;
            }

            var endNode = new ParameterObserverEndNode(this.action);
            previousNode.Next = endNode;

            return (parameterObserverRoot, expressionString);
        }
    }
}