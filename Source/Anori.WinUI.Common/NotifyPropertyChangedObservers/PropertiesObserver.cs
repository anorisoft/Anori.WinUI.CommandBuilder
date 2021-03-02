// -----------------------------------------------------------------------
// <copyright file="PropertyObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common.NotifyPropertyChangedObservers
{
    using JetBrains.Annotations;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    ///     Provide a way to observe property changes of INotifyPropertyChanged objects and invokes a
    ///     custom action when the PropertyChanged event is fired.
    /// </summary>
    public sealed class PropertiesObserver : IEquatable<PropertiesObserver>, IDisposable
    {
        /// <summary>
        /// The owner string
        /// </summary>
        private const string OwnerString = "owner";

        /// <summary>
        ///     The action
        /// </summary>
        private readonly Action action;

        /// <summary>
        ///     The property expression
        /// </summary>
        private readonly Expression propertyExpression;

        /// <summary>
        ///     The root node
        /// </summary>
        private readonly IList<RootPropertyObserverNode> rootNodes;

        internal readonly IList<INotifyPropertyChanged> Owners = new List<INotifyPropertyChanged>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver" /> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="action">The action.</param>
        private PropertiesObserver([NotNull] Expression propertyExpression, [NotNull] Action action)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.ExpressionString = propertyExpression.ToString();
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            (this.rootNodes, this.ExpressionString) = this.CreateChain();
            this.Owners.Add(this.rootNodes.Owner);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyObserver" /> class.
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
        private PropertiesObserver(
            [NotNull] INotifyPropertyChanged owner,
            [NotNull] Expression propertyExpression,
            [NotNull] Action action)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.Owners.Add(owner ?? throw new ArgumentNullException(nameof(owner)));
            (this.rootNodes, this.ExpressionString) = this.CreateChain(Owners);
        }

        private PropertiesObserver(
          [NotNull] INotifyPropertyChanged owner1,
          [NotNull] INotifyPropertyChanged owner2,
          [NotNull] Expression propertyExpression,
          [NotNull] Action action)
        {
            this.propertyExpression = propertyExpression ?? throw new ArgumentNullException(nameof(propertyExpression));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.Owners.Add(owner1 ?? throw new ArgumentNullException(nameof(owner1)));
            this.Owners.Add(owner2 ?? throw new ArgumentNullException(nameof(owner2)));
            (this.rootNodes, this.ExpressionString) = this.CreateChain(Owners);
        }

        /// <summary>
        ///     The expression
        /// </summary>
        public string ExpressionString { get; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Unsubscribe();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise,
        /// <see langword="false" />.
        /// </returns>
        public bool Equals(PropertiesObserver other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(this.ExpressionString, other.ExpressionString) && Equals(this.Owners, other.Owners);
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
        public static PropertiesObserver Observes<T>(
            Expression<Func<T>> propertyExpression,
            Action action,
            bool isAutoSubscribe = true)
        {
            var observer = new PropertiesObserver(propertyExpression.Body, action);

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
        public static PropertiesObserver Observes<TOwner, T>(
            TOwner owner,
            Expression<Func<TOwner, T>> propertyExpression,
            Action action,
            bool isAutoSubscribe = true)
            where TOwner : INotifyPropertyChanged
        {
            var observer = new PropertiesObserver(owner, propertyExpression.Body, action);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
        }

        public static PropertiesObserver Observes<TOwner1, TOwner2, T>(
           TOwner1 owner1,
           TOwner2 owner2,
           Expression<Func<TOwner1, TOwner2, T>> propertyExpression,
           Action action,
           bool isAutoSubscribe = true)
           where TOwner1 : INotifyPropertyChanged
           where TOwner2 : INotifyPropertyChanged
        {
            var observer = new PropertiesObserver(owner1, owner2, propertyExpression.Body, action);
            if (isAutoSubscribe)
            {
                observer.Subscribe();
            }

            return observer;
        }

        /// <summary>
        ///     Implements the operator !=.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator !=(PropertiesObserver lhs, PropertiesObserver rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        ///     Implements the operator ==.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>
        ///     The result of the operator.
        /// </returns>
        public static bool operator ==(PropertiesObserver lhs, PropertiesObserver rhs)
        {
            return lhs is { } && lhs.Equals(rhs);
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

            return this.Equals((PropertyObserver)obj);
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
                       ^ (this.Owners != null ? this.Owners.GetHashCode() : 0);
            }
        }

        /// <summary>
        ///     Subscribes this instance.
        /// </summary>
        public void Subscribe()
        {
            this.rootNodes.SubscribeListenerForOwner();
        }

        /// <summary>
        ///     Unsubscribes this instance.
        /// </summary>
        public void Unsubscribe()
        {
            this.rootNode.UnsubscribeListener();
        }

        /// <summary>
        /// Creates the graph.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Operation not supported for the given expression type {expression.Type}. "
        /// + "Only MemberExpression and ConstantExpression are currently supported.</exception>
        /// <exception cref="InvalidOperationException">Trying to subscribe PropertyChanged listener in object that "
        /// + $"owns '{rootPropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.</exception>
        /// <exception cref="System.NotSupportedException">Operation not supported for the given expression type. "
        /// + "Only MemberExpression and ConstantExpression are currently supported.</exception>
        /// <exception cref="System.InvalidOperationException">Trying to subscribe PropertyChanged listener in object that "
        /// + $"owns '{rootNode.PropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.</exception>
        private (RootPropertyObserverNode, string) CreateChain()
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
            if (!(constantExpression.Value is INotifyPropertyChanged owner))
            {
                throw new InvalidOperationException(
                    "Trying to subscribe PropertyChanged listener in object that "
                    + $"owns '{rootPropertyInfo.Name}' property, but the object does not implements INotifyPropertyChanged.");
            }

            var root = new RootPropertyObserverNode(rootPropertyInfo, this.action, owner);

            PropertyObserverNode previousNode = root;
            foreach (var currentNode in propertyInfos.Select(name => new PropertyObserverNode(name, this.action)))
            {
                previousNode.Next = currentNode;
                previousNode = currentNode;
            }

            return (root, expressionString);
        }

        /// <summary>
        /// Creates the chain.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">Operation not supported for the given expression type {expression.Type}. "
        ///                     + "Only MemberExpression and ConstantExpression are currently supported.</exception>
        private (IList<RootPropertyObserverNode>, string) CreateChain(IList<INotifyPropertyChanged> owners)
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

            if (!(expression is ParameterExpression parameterExpression))
            {
                throw new NotSupportedException(
                    $"Operation not supported for the given expression type {expression.Type}. "
                    + "Only MemberExpression and ConstantExpression are currently supported.");
            }

            var rootPropertyInfo = propertyInfos.Pop();
            expressionString = OwnerString + this.propertyExpression.ToString()
                                   .Remove(0, parameterExpression.ToString().Length);

            var roots = new List<RootPropertyObserverNode>();
            foreach (var owner in owners)
            {
                var root = new RootPropertyObserverNode(rootPropertyInfo, this.action, owner);
                roots.Add(root);
                PropertyObserverNode previousNode = root;
                foreach (var currentNode in propertyInfos.Select(name => new PropertyObserverNode(name, this.action)))
                {
                    previousNode.Next = currentNode;
                    previousNode = currentNode;
                }
            }

            return (roots, expressionString);
        }

        /// <summary>
        ///     Initializes the listeners.
        /// </summary>
        //private void InitializeListeners()
        //{
        //    this.rootNode = this.CreateChain();
        //}
    }
}