// -----------------------------------------------------------------------
// <copyright file="ObservesPropertyCommandOfTFixture.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

[assembly:
    InternalsVisibleTo(
        "Anori.WinUI.Commands.GUITest, "
        + "PublicKey="
        + "0024000004800000940000000602000000240000525341310004000001000100a520658730454f"
        + "b71a447c87dcb713412746dd0b04a1a1afea4067c991bc260f965eb7481148266358fc635ca839"
        + "5b78375f4cf69097188ab3cb2c27a52d1812872edd13157ed84b651f0462accdb31b65dfc4a352"
        + "2c8ada5c895c24738b342f759ac7ad33086e44a631a8884e1e6eb526e7e4e7170a52b723fe3c0d"
        + "db55b3c2")]

namespace Anori.WinUI.Commands.Tests
{
using Anori.WinUI.Commands.Commands;    using Anori.WinUI.Commands.CanExecuteObservers;
    using Anori.WinUI.Commands.Interfaces;

    using NUnit.Framework;

    using System;

using Anori.WinUI.Commands.Exceptions;

using ICommand = System.Windows.Input.ICommand;

    internal class DummyObserver : PropertyObserverBase<DummyObserver>, IPropertyObserver
    {
        public override event Action Update;
    }

    /// <summary>
    ///     Summary description for ObservableCommandFixture
    /// </summary>
    [TestFixture]
    public class ObservesPropertyCommandOfTFixture
    {
        [Test]
        public void GenericObservableCommandOfNullableIntWithNullableParameterShouldObserveCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesCanExecute(commandTestObject.BoolPropertyExpression);

            ICommand command = new ActivatableCanExecuteObserverCommand<int?>(o => { }, observer1).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void GenericObservableCommandOfObjectObservingPropertyShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var observer = new PropertyObserverFactory().ObservesProperty(commandTestObject.IntPropertyExpression);
            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            commandTestObject.RaisePropertyChanged(string.Empty);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericObservableCommandOfObjectObservingPropertyShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var observer = new PropertyObserverFactory().ObservesProperty(commandTestObject.IntPropertyExpression);
            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericObservableCommandOfObjectObservingPropertyWithOwnerShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var observer = new PropertyObserverFactory().ObservesProperty(commandTestObject, o => o.IntProperty);

            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            commandTestObject.RaisePropertyChanged(string.Empty);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericObservableCommandOfObjectObservingPropertyWithOwnerShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var observer = new PropertyObserverFactory().ObservesProperty(commandTestObject, o => o.IntProperty);
            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericObservableCommandOfObjectPropertyObserverUnsubscribeUnusedListeners()
        {
            var canExecuteChangedRaiseCount = 0;
            var commandTestObject = new CommandTestObject
            {
                ComplexProperty = new ComplexType
                {
                    IntProperty = 1,
                    InnerComplexProperty = new ComplexType
                    {
                        IntProperty = 1,
                        InnerComplexProperty =
                                                                                  new ComplexType
                                                                                  {
                                                                                      IntProperty = 1
                                                                                  }
                    }
                }
            };
            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(commandTestObject.ComplexPropertyIntPropertyExpression);
            var observer2 = factory.ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);
            var observer3 = factory.ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyInnerComplexPropertyIntPropertyExpression);
            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1, observer2, observer3).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaiseCount++; };

            commandTestObject.ComplexProperty.IntProperty = 2;
            commandTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty = 2;
            commandTestObject.ComplexProperty.InnerComplexProperty.IntProperty = 2;

            Assert.AreEqual(3, canExecuteChangedRaiseCount);

            var innerInnerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty;
            var innerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty;
            var complexProp = commandTestObject.ComplexProperty;

            commandTestObject.ComplexProperty = new ComplexType
            {
                InnerComplexProperty = new ComplexType
                {
                    InnerComplexProperty =
                                                                                           new ComplexType()
                }
            };

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());

            innerInnerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty;
            innerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty;
            complexProp = commandTestObject.ComplexProperty;

            commandTestObject.ComplexProperty = null;

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());
        }

        [Test]
        public void GenericObservableCommandOfObjectPropertyObserverWithOwnerUnsubscribeUnusedListeners()
        {
            var canExecuteChangedRaiseCount = 0;
            var commandTestObject = new CommandTestObject
            {
                ComplexProperty = new ComplexType
                {
                    IntProperty = 1,
                    InnerComplexProperty = new ComplexType
                    {
                        IntProperty = 1,
                        InnerComplexProperty =
                                                                                  new ComplexType
                                                                                  {
                                                                                      IntProperty = 1
                                                                                  }
                    }
                }
            };

            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(commandTestObject, o => o.ComplexProperty.IntProperty);
            var observer2 = factory.ObservesProperty(
                commandTestObject,
                o => o.ComplexProperty.InnerComplexProperty.IntProperty);
            var observer3 = factory.ObservesProperty(
                commandTestObject,
                o => o.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty);
            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1, observer2, observer3).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaiseCount++; };

            commandTestObject.ComplexProperty.IntProperty = 2;
            commandTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty = 2;
            commandTestObject.ComplexProperty.InnerComplexProperty.IntProperty = 2;

            Assert.AreEqual(3, canExecuteChangedRaiseCount);

            var innerInnerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty;
            var innerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty;
            var complexProp = commandTestObject.ComplexProperty;

            commandTestObject.ComplexProperty = new ComplexType
            {
                InnerComplexProperty = new ComplexType
                {
                    InnerComplexProperty =
                                                                                           new ComplexType()
                }
            };

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());

            innerInnerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty.InnerComplexProperty;
            innerComplexProp = commandTestObject.ComplexProperty.InnerComplexProperty;
            complexProp = commandTestObject.ComplexProperty;

            commandTestObject.ComplexProperty = null;

            Assert.AreEqual(0, innerInnerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, innerComplexProp.GetPropertyChangedSubscribedLength());
            Assert.AreEqual(0, complexProp.GetPropertyChangedSubscribedLength());
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldNotObserveDuplicateProperties()
        {
            var commandTestObject = new CommandTestObject();
            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(commandTestObject.IntPropertyExpression);
            var observer2 = factory.ObservesProperty(commandTestObject.IntPropertyExpression);

            Assert.Throws<CommandBuilderException > (
                                                         () =>
                                                             {
                                                                 var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1, observer2);
                                                             });
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldNotObserveWithOwnerDuplicateProperties()
        {
            var commandTestObject = new CommandTestObject();

            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(commandTestObject, o => o.IntProperty);
            var observer2 = factory.ObservesProperty(commandTestObject, o => o.IntProperty);

            Assert.Throws<CommandBuilderException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1, observer2);
                    });
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldObserveCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var factory = new PropertyObserverFactory();
            var canExecuteObserver = factory.ObservesCanExecute(commandTestObject.BoolPropertyExpression);

            ICommand command = new ActivatableCanExecuteObserverCommand<object>(o => { }, canExecuteObserver).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldObserveCanExecuteAndObserveOtherProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var factory = new PropertyObserverFactory();
            var canExecuteObserver = factory.ObservesCanExecute(commandTestObject, o => o.BoolProperty);
            var observer1 = factory.ObservesProperty(commandTestObject, o => o.IntProperty);

            ICommand command =
                new ActivatableCanExecuteObserverCommand<object>(o => { }, canExecuteObserver, observer1).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            canExecuteChangedRaised = false;
            Assert.False(canExecuteChangedRaised);

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldObserveComplexPropertyWhenParentPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = new ComplexType() };
            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaise = true; };

            var newComplexObject = new ComplexType { InnerComplexProperty = new ComplexType { IntProperty = 10 } };

            commandTestObject.ComplexProperty.InnerComplexProperty = newComplexObject;

            Assert.True(canExecuteChangedRaise);
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldObserveComplexPropertyWhenRootPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = null };
            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaise = true; };

            var newComplexObject = new ComplexType { InnerComplexProperty = new ComplexType { IntProperty = 10 } };

            commandTestObject.ComplexProperty = newComplexObject;

            Assert.True(canExecuteChangedRaise);
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldObserveMultipleProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(commandTestObject.IntPropertyExpression);
            var observer2 = factory.ObservesProperty(commandTestObject.BoolPropertyExpression);

            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1, observer2).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);

            canExecuteChangedRaised = false;

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldObserveOneProperty()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var factory = new PropertyObserverFactory();
            var observer1 = factory.ObservesProperty(commandTestObject.IntPropertyExpression);

            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, observer1).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericObservableCommandOfObjectShouldObserveWithOwnerCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var factory = new PropertyObserverFactory();
            var canExecuteObserver = factory.ObservesCanExecute(commandTestObject, o => o.BoolProperty);

            ICommand command = new ActivatableCanExecuteObserverCommand<object>(o => { }, canExecuteObserver).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, new DummyObserver());
            var canExecuteChangedRaised = false;
            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            command.RaisePropertyChanged();

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void ShouldPassParameterInstanceOnCanExecute()
        {
            var canExecuteCalled = false;
            var testClass = new MyClass();
            ICommand command = new ActivatableCanExecuteObserverCommand<MyClass>(
                p => { },
                delegate (MyClass parameter)
                    {
                        Assert.AreSame(testClass, parameter);
                        canExecuteCalled = true;
                        return true;
                    },
                new DummyObserver());

            command.CanExecute(testClass);
            Assert.True(canExecuteCalled);
        }

        [Test]
        public void ShouldPassParameterInstanceOnExecute()
        {
            var executeCalled = false;
            var testClass = new MyClass();
            ICommand command = new ActivatableCanExecuteObserverCommand<MyClass>(
                delegate (MyClass parameter)
                    {
                        Assert.AreSame(testClass, parameter);
                        executeCalled = true;
                    },
                new DummyObserver());

            command.Execute(testClass);
            Assert.True(executeCalled);
        }

        [Test]
        public void ShouldThrowIfAllDelegatesAreNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand<object>(null, null as IPropertyObserver);
                    });
        }

        [Test]
        public void ShouldThrowIfCanExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand<object>(
                            o => { },
                            null as IPropertyObserver);
                    });
        }

        [Test]
        public void ShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand<object>(null, new DummyObserver());
                    });
        }

        [Test]
        public void WhenConstructedWithGenericTypeIsNonNullableValueType_Throws()
        {
            var command = new ActivatableCanExecuteObserverCommand<int>(param => { }, new DummyObserver());
            command.Execute(1);
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfNullable_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new ActivatableCanExecuteObserverCommand<int?>(param => { }, new DummyObserver());

            // verify
            Assert.NotNull(actual);
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfObject_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new ActivatableCanExecuteObserverCommand<object>(param => { }, new DummyObserver());

            // verify
            Assert.NotNull(actual);
        }

        internal void DoNothing(object param)
        {
        }
    }
}