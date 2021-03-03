// -----------------------------------------------------------------------
// <copyright file="ObservesNotifyPropertyChangedOfTFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------


using Anori.ExpressionObservers;

namespace Anori.WinUI.Commands.Tests
{
    using NUnit.Framework;

    /// <summary>
    ///     Summary description for ObservableCommandFixture
    /// </summary>
    [TestFixture]
    public class ObservesNotifyPropertyChangedOfTFixture
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
            Assert.False((object)observer1 == (object)observer2);
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
            Assert.True((object)observer1 != (object)observer2);
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
            var notifyPropertyChangedTestObject = new NotifyPropertyChangedTestObject();
            using var observer1 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject.IntPropertyExpression,
                () => { });
            using var observer2 = PropertyObserver.Observes(
                notifyPropertyChangedTestObject,
                o => o.IntProperty,
                () => { });
            Assert.True(observer1 == observer2);
        }

        [Test]
        public void NotifyPropertyChanged_Expression_Integer_AutoActivate_True()
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
                () => actionRaised = true, false);
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