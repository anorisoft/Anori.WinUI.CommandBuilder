// -----------------------------------------------------------------------
// <copyright file="ObservesNotifyPropertyChangedOfTFixture - Copy.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.ExpressionObservers;

namespace Anori.WinUI.Commands.Tests
{
    using System;
    using System.Linq.Expressions;

    using Anori.Parameters;
    using Anori.WinUI.Common.Parameters;

    using NUnit.Framework;

    /// <summary>
    ///     Summary description for ObservableCommandFixture
    /// </summary>
    [TestFixture]
    public class ObservesParameterOfTFixture
    {
        [Test]
        public void NotifyPropertyChanged_Different_EqualOperator_Test()
        {
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.BoolPropertyExpression,
                () => { });
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            Assert.False(observer1.Equals(observer2));
        }

        [Test]
        public void NotifyPropertyChanged_Different_NotEqualOperator_Test()
        {
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.BoolPropertyExpression,
                () => { });
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            Assert.True((object)observer1 != observer2);
        }

        [Test]
        public void NotifyPropertyChanged_Equal_1_Test()
        {
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            Assert.True(observer1.Equals(observer2));
        }

        [Test]
        public void NotifyPropertyChanged_Equal_2_Test()
        {
            var notifyPropertyChangedTestObject = new Parameter<ParameterTestObject>{ Value = new ParameterTestObject()};
            var expression =  notifyPropertyChangedTestObject.Value.IntPropertyExpression;
            using var observer1 = ParameterObserver.Observes(expression, () => { });
            using var observer2 = ParameterObserver.Observes(
                notifyPropertyChangedTestObject.Value,
                o => o.IntParameter,
                () => { });
            Assert.True(observer1.Equals(observer2));
        }

        [Test]
        public void NotifyPropertyChanged_Expression_Integer_AutoActivate_True()
        {
            var actionRaised = false;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            using var observer1 = ParameterObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => actionRaised = true);
            Assert.False(actionRaised);
            notifyPropertyChangedTestObject.IntParameter.Value = 2;
            Assert.True(actionRaised);
        }

        [Test]
        public void NotifyPropertyChanged_OwnerExpression_Integer_AutoActivate_True()
        {
            var actionRaised = false;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            using var observer1 = ParameterObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.IntParameter,
                () => actionRaised = true);
            Assert.False(actionRaised);
            notifyPropertyChangedTestObject.IntParameter.Value = 2;
            Assert.True(actionRaised);
        }

        [Test]
        public void NotifyPropertyChanged_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True()
        {
            var actionRaised = false;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            notifyPropertyChangedTestObject.IntParameter.Value = 10;
            notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 11;

            using var observer1 = ParameterObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.Value.IntProperty.Value,
                () => { actionRaised = true; });
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.ValueChanged += (sender, args) =>
                {
                    var i = 1;
                };
            Assert.False(actionRaised);
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
            Assert.True(actionRaised);
        }

        /// <summary>
        ///     Notifies the property changed owner expression complex property value integer automatic activate true2.
        /// </summary>
        [Test]
        public void NotifyPropertyChanged_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True2()
        {
            var value = 0;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            notifyPropertyChangedTestObject.IntParameter.Value = 1;
            notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

            using var observer1 = ParameterObserverFactory.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.Value.IntProperty.Value,
                v => value = v);

            Assert.AreEqual(0, value);
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
            Assert.AreEqual(2, value);
        }

        [Test]
        public void NotifyPropertyChanged_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        {
            var value = 0;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            notifyPropertyChangedTestObject.IntParameter.Value = 1;
            notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

            using var observer1 = ParameterObserverFactory.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.Value.IntProperty.Value);
            Assert.AreEqual(0, observer1.SubscribedLength);
            void Ev(int v) => value = v;
            observer1.ParameterChanged += Ev;
            Assert.AreEqual(1, observer1.SubscribedLength);

            Assert.AreEqual(0, value);
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
            Assert.AreEqual(2, value);
            observer1.ParameterChanged -= Ev;
            Assert.AreEqual(0, observer1.SubscribedLength);
        }

        [Test]
        public void
            ReactiveParameterObservers_ParameterObserver_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        {
            var value = 1;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            notifyPropertyChangedTestObject.IntParameter.Value = 1;
            notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

            using var observer1 = Common.Parameters.Reactive.ObserverFactory.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.Value.IntProperty.Value);
            using var d = observer1.Subscribe(v => value = v);

            Assert.AreEqual(1, value);
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
            Assert.AreEqual(2, value);
        }

        [Test]
        public void
            ReactiveParameterObservers_BehaviorParameterObserver_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        {
            var value = 0;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            notifyPropertyChangedTestObject.IntParameter.Value = 1;
            notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

            using var observer1 = Common.Parameters.Reactive.BehaviorObserverFactory.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.Value.IntProperty.Value);
            using var d = observer1.Subscribe(v => value = v);

            Assert.AreEqual(1, value);
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
            Assert.AreEqual(2, value);
        }

        [Test]
        public void
            ReactiveParameterObservers_ReplayParameterObserver_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        {
            var value = (int?)null;
            var count = 0;
            var notifyPropertyChangedTestObject = new ParameterTestObject();

            using var observer1 = Common.Parameters.Reactive.ReplayObserverFactory.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.Value.IntProperty.Value);

            var a = notifyPropertyChangedTestObject.ComplexProperty.Value?.IntProperty.Value;
            Expression<Func<ParameterTestObject, int?>> exp = (o => o.ComplexProperty.Value.IntProperty.Value);
            //  var x = exp.Compile()(notifyPropertyChangedTestObject);
            notifyPropertyChangedTestObject.IntParameter.Value = 1;
            notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;

            Assert.AreEqual(null, value);
            Assert.AreEqual(0, count);

            using var d = observer1.Subscribe(
                v =>
                    {
                        count++;
                        value = v;
                    });

            Assert.AreEqual(2, value);
            Assert.AreEqual(3, count);
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 3;
            Assert.AreEqual(3, value);
            Assert.AreEqual(4, count);
            notifyPropertyChangedTestObject.ComplexProperty.Value = null;
            Assert.AreEqual(null, value);
        }

        [Test]
        public void NotifyPropertyChanged_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True3()
        {
            var value = 0;
            var notifyPropertyChangedTestObject = new ParameterTestObject();
            notifyPropertyChangedTestObject.IntParameter.Value = 1;
            notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

            var a = new ComplexParameterType();
            a.IntProperty.Value = 1;

            using var observer1 = ParameterObserverFactory.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.Value.IntProperty.Value,
                v => value = v);

            Assert.AreEqual(0, value);
            notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
            Assert.AreEqual(2, value);
            notifyPropertyChangedTestObject.ComplexProperty.Value = a;
            Assert.AreEqual(1, value);
        }

        /// <summary>
        ///     Notifies the property changed expression observes integer and boolean test.
        /// </summary>
        [Test]
        public void NotifyPropertyChanged_Expression_ObservesIntegerAndBoolean_Test()
        {
            var actionIntegerRaised = false;
            var actionBooleanRaised = false;
            var notifyPropertyChangedTestObject =
                new NotifyPropertyChangedTestObject { IntProperty = 1, BoolProperty = false };
            using var integerObserver = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => actionIntegerRaised = true);

            using var booleanObserver = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.BoolPropertyExpression,
                () => actionBooleanRaised = true);
            booleanObserver.Subscribe(true);

            Assert.False(actionIntegerRaised);
            Assert.False(actionBooleanRaised);
            notifyPropertyChangedTestObject.BoolProperty = true;
            Assert.True(actionBooleanRaised);
            Assert.False(actionIntegerRaised);
            notifyPropertyChangedTestObject.IntProperty = 2;
            Assert.True(actionBooleanRaised);
            Assert.True(actionIntegerRaised);
        }

        [Test]
        public void NotifyPropertyChanged_Expressions_ComplexType_Test()
        {
            var canExecuteChangedRaiseCount = 0;
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject
            {
                ComplexProperty = new ComplexType
                {
                    IntProperty = 1,
                    InnerComplexProperty =
                                                                                        new ComplexType
                                                                                        {
                                                                                            IntProperty = 1,
                                                                                            InnerComplexProperty =
                                                                                                    new ComplexType
                                                                                                    {
                                                                                                        IntProperty
                                                                                                                = 1
                                                                                                    }
                                                                                        }
                }
            };
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.ComplexPropertyIntPropertyExpression,
                () => canExecuteChangedRaiseCount++);
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression,
                () => canExecuteChangedRaiseCount++);
            using var observer3 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject
                    .ComplexPropertyInnerComplexPropertyInnerComplexPropertyIntPropertyExpression,
                () => canExecuteChangedRaiseCount++);

            Assert.AreEqual(0, canExecuteChangedRaiseCount);
            notifyPropertyChangedTestObject.ComplexProperty.IntProperty = 2;
            Assert.AreEqual(1, canExecuteChangedRaiseCount);
            notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty.IntProperty = 2;
            Assert.AreEqual(2, canExecuteChangedRaiseCount);
            notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty = 2;
            Assert.AreEqual(3, canExecuteChangedRaiseCount);

            var innerInnerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty
                .InnerComplexProperty;
            var innerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty;
            var complexProp = notifyPropertyChangedTestObject.ComplexProperty;

            Assert.AreEqual(1, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(2, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(3, complexProp.GetPropertyChangedSubscribedLength());

            notifyPropertyChangedTestObject.ComplexProperty = new ComplexType
            {
                InnerComplexProperty = new ComplexType
                {
                    InnerComplexProperty =
                                                                                  new ComplexType()
                }
            };

            Assert.AreEqual(6, canExecuteChangedRaiseCount);

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());

            innerInnerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty
                .InnerComplexProperty;
            innerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty;
            complexProp = notifyPropertyChangedTestObject.ComplexProperty;

            Assert.AreEqual(1, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(2, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(3, complexProp.GetPropertyChangedSubscribedLength());

            notifyPropertyChangedTestObject.ComplexProperty = null;
            Assert.AreEqual(9, canExecuteChangedRaiseCount);

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());
        }

        [Test]
        public void NotifyPropertyChanged_Integer_AutoActivate()
        {
            var actionRaised = false;
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => actionRaised = true);
            Assert.False(actionRaised);
            notifyPropertyChangedTestObject.IntProperty = 2;
            Assert.True(actionRaised);
        }

        [Test]
        public void NotifyPropertyChanged_Integer_AutoActivate_False()
        {
            var actionRaised = false;
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => actionRaised = true,false);
            Assert.False(actionRaised);
            notifyPropertyChangedTestObject.IntProperty = 2;
            Assert.False(actionRaised);
        }

        [Test]
        public void NotifyPropertyChanged_Integer_Subscribe()
        {
            var actionRaisedCount = 0;
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject { IntProperty = 1 };
            using var observer = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => actionRaisedCount++, false);
            Assert.AreEqual(0, actionRaisedCount);
            notifyPropertyChangedTestObject.IntProperty = 2;
            Assert.AreEqual(0, actionRaisedCount);

            observer.Subscribe(true);
            notifyPropertyChangedTestObject.IntProperty = 3;
            Assert.AreEqual(1, actionRaisedCount);
            notifyPropertyChangedTestObject.IntProperty = 4;
            Assert.AreEqual(2, actionRaisedCount);

            observer.Unsubscribe();
            notifyPropertyChangedTestObject.IntProperty = 5;
            Assert.AreEqual(2, actionRaisedCount);
        }

        [Test]
        public void NotifyPropertyChanged_OwnerAndExpression_Integer_AutoActivate_True()
        {
            var actionRaised = false;
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer = PropertyObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.IntProperty,
                () => actionRaised = true);
            Assert.False(actionRaised);
            notifyPropertyChangedTestObject.IntProperty = 2;
            Assert.True(actionRaised);
        }

        [Test]
        public void NotifyPropertyChanged_OwnerAndExpression_ObservesIntegerAndBoolean_Test()
        {
            var actionIntegerRaised = false;
            var actionBooleanRaised = false;

            var notifyPropertyChangedTestObject =
                new NotifyPropertyChangedTestObject { IntProperty = 1, BoolProperty = false };

            using var integerObserver = PropertyObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.IntProperty,
                () => actionIntegerRaised = true);

            using var booleanObserver = PropertyObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.BoolProperty,
                () => actionBooleanRaised = true);

            Assert.False(actionIntegerRaised);
            Assert.False(actionBooleanRaised);
            notifyPropertyChangedTestObject.BoolProperty = true;
            Assert.True(actionBooleanRaised);
            Assert.False(actionIntegerRaised);
            notifyPropertyChangedTestObject.IntProperty = 2;
            Assert.True(actionBooleanRaised);
            Assert.True(actionIntegerRaised);
        }

        [Test]
        public void NotifyPropertyChanged_OwnerAndExpressions_ComplexType_Test()
        {
            var canExecuteChangedRaiseCount = 0;
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject
            {
                ComplexProperty = new ComplexType
                {
                    IntProperty = 1,
                    InnerComplexProperty =
                                                                                        new ComplexType
                                                                                        {
                                                                                            IntProperty = 1,
                                                                                            InnerComplexProperty =
                                                                                                    new ComplexType
                                                                                                    {
                                                                                                        IntProperty
                                                                                                                = 1
                                                                                                    }
                                                                                        }
                }
            };
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.IntProperty,
                () => canExecuteChangedRaiseCount++);
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.InnerComplexProperty.IntProperty,
                () => canExecuteChangedRaiseCount++);
            using var observer3 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty,
                () => canExecuteChangedRaiseCount++);

            Assert.AreEqual(0, canExecuteChangedRaiseCount);
            notifyPropertyChangedTestObject.ComplexProperty.IntProperty = 2;
            Assert.AreEqual(1, canExecuteChangedRaiseCount);
            notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty.IntProperty = 2;
            Assert.AreEqual(2, canExecuteChangedRaiseCount);
            notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty = 2;
            Assert.AreEqual(3, canExecuteChangedRaiseCount);

            var innerInnerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty
                .InnerComplexProperty;
            var innerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty;
            var complexProp = notifyPropertyChangedTestObject.ComplexProperty;

            Assert.AreEqual(1, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(2, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(3, complexProp.GetPropertyChangedSubscribedLength());

            notifyPropertyChangedTestObject.ComplexProperty = new ComplexType
            {
                InnerComplexProperty = new ComplexType
                {
                    InnerComplexProperty =
                                                                                  new ComplexType()
                }
            };

            Assert.AreEqual(6, canExecuteChangedRaiseCount);

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());

            innerInnerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty
                .InnerComplexProperty;
            innerComplexProp = notifyPropertyChangedTestObject.ComplexProperty.InnerComplexProperty;
            complexProp = notifyPropertyChangedTestObject.ComplexProperty;

            Assert.AreEqual(1, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(2, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(3, complexProp.GetPropertyChangedSubscribedLength());

            notifyPropertyChangedTestObject.ComplexProperty = null;
            Assert.AreEqual(9, canExecuteChangedRaiseCount);

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());
        }

        [Test]
        public void NotifyPropertyChanged_SameAre_EqualOperator_Test()
        {
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            Assert.True(observer1 == observer2);
        }

        [Test]
        public void NotifyPropertyChanged_SameAre_NotEqualOperator_Test()
        {
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            Assert.False(observer1 != observer2);
        }
    }
}