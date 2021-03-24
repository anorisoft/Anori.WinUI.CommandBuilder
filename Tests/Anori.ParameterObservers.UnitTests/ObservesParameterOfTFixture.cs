// -----------------------------------------------------------------------
// <copyright file="ObservesNotifyPropertyChangedOfTFixture - Copy.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.UnitTests
{
    using System;
    using System.Linq.Expressions;

    using Anori.Parameters;

    using NUnit.Framework;

    /// <summary>
    ///     Summary description for ObservableCommandFixture
    /// </summary>
    [TestFixture]
    public class ObservesParameterOfTFixture
    {
       

        [Test]
        public void NotifyPropertyChanged_Equal_2_Test()
        {
            var notifyPropertyChangedTestObject = new Parameter<ParameterTestObject>{ Value = new ParameterTestObject()};
            var expression =  notifyPropertyChangedTestObject.Value.IntPropertyExpression;
            using var observer1 = ParameterObserver.Observes(expression, () => { });
            using var observer2 = ParameterObserver.Observes(notifyPropertyChangedTestObject.Value, o => o.IntParameter,
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
        //[Test]
        //public void NotifyPropertyChanged_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True2()
        //{
        //    var value = 0;
        //    var notifyPropertyChangedTestObject = new ParameterTestObject();
        //    notifyPropertyChangedTestObject.IntParameter.Value = 1;
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

        //    using var observer1 = ParameterObserver.Observes(
        //        notifyPropertyChangedTestObject,
        //        o => o.ComplexProperty.Value.IntProperty.Value,
        //        v => value = v);

        //    Assert.AreEqual(0, value);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
        //    Assert.AreEqual(2, value);
        //}

        //[Test]
        //public void NotifyPropertyChanged_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        //{
        //    var value = 0;
        //    var notifyPropertyChangedTestObject = new ParameterTestObject();
        //    notifyPropertyChangedTestObject.IntParameter.Value = 1;
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

        //    using var observer1 = ParameterObserverFactory.Observes(
        //        notifyPropertyChangedTestObject,
        //        o => o.ComplexProperty.Value.IntProperty.Value);
        //    Assert.AreEqual(0, observer1.SubscribedLength);
        //    void Ev(int v) => value = v;
        //    observer1.ParameterChanged += Ev;
        //    Assert.AreEqual(1, observer1.SubscribedLength);

        //    Assert.AreEqual(0, value);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
        //    Assert.AreEqual(2, value);
        //    observer1.ParameterChanged -= Ev;
        //    Assert.AreEqual(0, observer1.SubscribedLength);
        //}

        //[Test]
        //public void
        //    ReactiveParameterObservers_ParameterObserver_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        //{
        //    var value = 1;
        //    var notifyPropertyChangedTestObject = new ParameterTestObject();
        //    notifyPropertyChangedTestObject.IntParameter.Value = 1;
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

        //    using var observer1 = Common.Parameters.Reactive.ObserverFactory.Observes(
        //        notifyPropertyChangedTestObject,
        //        o => o.ComplexProperty.Value.IntProperty.Value);
        //    using var d = observer1.Subscribe(v => value = v);

        //    Assert.AreEqual(1, value);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
        //    Assert.AreEqual(2, value);
        //}

        //[Test]
        //public void
        //    ReactiveParameterObservers_BehaviorParameterObserver_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        //{
        //    var value = 0;
        //    var notifyPropertyChangedTestObject = new ParameterTestObject();
        //    notifyPropertyChangedTestObject.IntParameter.Value = 1;
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

        //    using var observer1 = Common.Parameters.Reactive.BehaviorObserverFactory.Observes(
        //        notifyPropertyChangedTestObject,
        //        o => o.ComplexProperty.Value.IntProperty.Value);
        //    using var d = observer1.Subscribe(v => value = v);

        //    Assert.AreEqual(1, value);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
        //    Assert.AreEqual(2, value);
        //}

        //[Test]
        //public void
        //    ReactiveParameterObservers_ReplayParameterObserver_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True4()
        //{
        //    var value = (int?)null;
        //    var count = 0;
        //    var notifyPropertyChangedTestObject = new ParameterTestObject();

        //    using var observer1 = Common.Parameters.Reactive.ReplayObserverFactory.Observes(
        //        notifyPropertyChangedTestObject,
        //        o => o.ComplexProperty.Value.IntProperty.Value);

        //    var a = notifyPropertyChangedTestObject.ComplexProperty.Value?.IntProperty.Value;
        //    Expression<Func<ParameterTestObject, int?>> exp = (o => o.ComplexProperty.Value.IntProperty.Value);
        //    //  var x = exp.Compile()(notifyPropertyChangedTestObject);
        //    notifyPropertyChangedTestObject.IntParameter.Value = 1;
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;

        //    Assert.AreEqual(null, value);
        //    Assert.AreEqual(0, count);

        //    using var d = observer1.Subscribe(
        //        v =>
        //            {
        //                count++;
        //                value = v;
        //            });

        //    Assert.AreEqual(2, value);
        //    Assert.AreEqual(3, count);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 3;
        //    Assert.AreEqual(3, value);
        //    Assert.AreEqual(4, count);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = null;
        //    Assert.AreEqual(null, value);
        //}

        //[Test]
        //public void NotifyPropertyChanged_OwnerExpression_ComplexProperty_Value_Integer_AutoActivate_True3()
        //{
        //    var value = 0;
        //    var notifyPropertyChangedTestObject = new ParameterTestObject();
        //    notifyPropertyChangedTestObject.IntParameter.Value = 1;
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = new ComplexParameterType();
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 1;

        //    var a = new ComplexParameterType();
        //    a.IntProperty.Value = 1;

        //    using var observer1 = ParameterObserverFactory.Observes(
        //        notifyPropertyChangedTestObject,
        //        o => o.ComplexProperty.Value.IntProperty.Value,
        //        v => value = v);

        //    Assert.AreEqual(0, value);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value.IntProperty.Value = 2;
        //    Assert.AreEqual(2, value);
        //    notifyPropertyChangedTestObject.ComplexProperty.Value = a;
        //    Assert.AreEqual(1, value);
        //}

     
    }
}