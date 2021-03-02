// -----------------------------------------------------------------------
// <copyright file="DelegateCommandFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using NUnit.Framework;

    using System;
    using System.Windows.Input;

    /// <summary>
    ///     Summary description for DelegateCommandFixture
    /// </summary>
    [TestFixture]
    public class DelegateCommandOfTFixture
    {
        [Test]
        public void CanExecuteOfObjectCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatablePropertyObserverCommand<object>(handlers.Execute, handlers.CanExecute);
            var parameter = new object();

            handlers.CanExecuteReturnValue = true;
            var retVal = command.CanExecute(parameter);

            Assert.AreSame(parameter, handlers.CanExecuteParameter);
            Assert.AreEqual(handlers.CanExecuteReturnValue, retVal);
        }

        [Test]
        public void CanExecuteOfObjectReturnsTrueWithoutCanExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatablePropertyObserverCommand<object>(handlers.Execute);

            var retVal = command.CanExecute(null);

            Assert.True(retVal);
        }

        [Test]
        public void CanRemoveCanExecuteOfObjectChangedHandler()
        {
            var command = new ActivatablePropertyObserverCommand<object>(o => { });

            var canExecuteChangedRaised = false;

            void Handler(object s, EventArgs e) => canExecuteChangedRaised = true;

            command.CanExecuteChanged += Handler;
            command.CanExecuteChanged -= Handler;
            command.RaiseCanExecuteChanged();

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void DelegateCommandOfObjectGenericShouldThrowIfCanExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<object>(o => { }, null);
                    });
        }

        [Test]
        public void DelegateCommandOfObjectShouldThrowIfAllDelegatesAreNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<object>(null, null);
                    });
        }

        [Test]
        public void ExecuteCallsPassedInExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatablePropertyObserverCommand<object>(handlers.Execute);
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(parameter, handlers.ExecuteParameter);
        }

        [Test]
        public void GenericDelegateCommandOfObjectNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new ActivatablePropertyObserverCommand<object>(o => { });

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectObservingPropertyShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            commandTestObject.RaisePropertyChanged(string.Empty);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectObservingPropertyWithOwnerShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(commandTestObject, o => o.IntProperty);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            commandTestObject.RaisePropertyChanged(string.Empty);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectObservingPropertyShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectObservingPropertyWithOwnerShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(commandTestObject, o => o.IntProperty);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectPropertyObserverUnsubscribeUnusedListeners()
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

            var command = new ActivatablePropertyObserverCommand<object>(o => { })
                .ObservesProperty(commandTestObject.ComplexPropertyIntPropertyExpression)
                .ObservesProperty(
                    commandTestObject.ComplexPropertyInnerComplexPropertyInnerComplexPropertyIntPropertyExpression)
                .ObservesProperty(commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

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
        public void GenericDelegateCommandOfObjectPropertyObserverWithOwnerUnsubscribeUnusedListeners()
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

            var command = new ActivatablePropertyObserverCommand<object>(o => { })
                .ObservesProperty(commandTestObject, o => o.ComplexProperty.IntProperty)
                .ObservesProperty(
                    commandTestObject,
                    o => o.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty)
                .ObservesProperty(commandTestObject, o => o.ComplexProperty.InnerComplexProperty.IntProperty);

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
        public void GenericDelegateCommandOfObjectShouldNotObserveDuplicateCanExecute()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        ICommand command = new ActivatablePropertyObserverCommand<object>(o => { })
                            .ObservesCanExecute(commandTestObject.BoolPropertyExpression)
                            .ObservesCanExecute(commandTestObject.BoolPropertyExpression);
                    });
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldNotObserveWithOwnerDuplicateCanExecute()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        ICommand command = new ActivatablePropertyObserverCommand<object>(o => { })
                            .ObservesCanExecute(commandTestObject, o1 => o1.BoolProperty)
                            .ObservesCanExecute(commandTestObject, o2 => o2.BoolProperty);
                    });
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldNotObserveDuplicateProperties()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<object>(o => { })
                            .ObservesProperty(commandTestObject.IntPropertyExpression)
                            .ObservesProperty(commandTestObject.IntPropertyExpression);
                    });
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldNotObserveWithOwnerDuplicateProperties()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<object>(o => { })
                            .ObservesProperty(commandTestObject, o => o.IntProperty)
                            .ObservesProperty(commandTestObject, o => o.IntProperty);
                    });
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldObserveCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command =
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesCanExecute(commandTestObject.BoolPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldObserveWithOwnerCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command =
                new ActivatablePropertyObserverCommand<object>(o1 => { }).ObservesCanExecute(commandTestObject, o2 => o2.BoolProperty);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldObserveCanExecuteAndObserveOtherProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command = new ActivatablePropertyObserverCommand<object>(o => { })
                .ObservesCanExecute(commandTestObject.BoolPropertyExpression)
                .ObservesProperty(commandTestObject.IntPropertyExpression);

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
        public void GenericDelegateCommandOfObjectShouldObserveComplexPropertyWhenParentPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = new ComplexType() };
            var command = new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaise = true; };

            var newComplexObject = new ComplexType { InnerComplexProperty = new ComplexType { IntProperty = 10 } };

            commandTestObject.ComplexProperty.InnerComplexProperty = newComplexObject;

            Assert.True(canExecuteChangedRaise);
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldObserveComplexPropertyWhenRootPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = null };

            var command = new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaise = true; };

            var newComplexObject = new ComplexType { InnerComplexProperty = new ComplexType { IntProperty = 10 } };

            commandTestObject.ComplexProperty = newComplexObject;

            Assert.True(canExecuteChangedRaise);
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldObserveMultipleProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            var command = new ActivatablePropertyObserverCommand<object>(o => { })
                .ObservesProperty(commandTestObject.IntPropertyExpression)
                .ObservesProperty(commandTestObject.BoolPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);

            canExecuteChangedRaised = false;

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectShouldObserveOneProperty()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfNullableIntWithNullableParameterShouldObserveCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command =
                new ActivatablePropertyObserverCommand<int?>(o => { }).ObservesCanExecute(commandTestObject.BoolPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void IsActivePropertyChangeFiresEventOfObject()
        {
            var fired = false;
            var command = new ActivatablePropertyObserverCommand<object>(this.DoNothing);
            command.IsActiveChanged += delegate { fired = true; };
            command.Activate();

            Assert.True(fired);
        }

        [Test]
        public void IsActivePropertyIsFalseByDefaultOfObject()
        {
            var command = new ActivatablePropertyObserverCommand<object>(this.DoNothing);
            Assert.False(command.IsActive);
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatablePropertyObserverCommand<object>(handlers.Execute);
            var canExecuteChangedRaised = false;
            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            command.RaiseCanExecuteChanged();

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void ShouldPassParameterInstanceOnCanExecute()
        {
            var canExecuteCalled = false;
            var testClass = new MyClass();
            ICommand command = new ActivatablePropertyObserverCommand<MyClass>(
                p => { },
                delegate (MyClass parameter)
                    {
                        Assert.AreSame(testClass, parameter);
                        canExecuteCalled = true;
                        return true;
                    });

            command.CanExecute(testClass);
            Assert.True(canExecuteCalled);
        }

        [Test]
        public void ShouldPassParameterInstanceOnExecute()
        {
            var executeCalled = false;
            var testClass = new MyClass();
            ICommand command = new ActivatablePropertyObserverCommand<MyClass>(
                delegate (MyClass parameter)
                    {
                        Assert.AreSame(testClass, parameter);
                        executeCalled = true;
                    });

            command.Execute(testClass);
            Assert.True(executeCalled);
        }

        [Test]
        public void ShouldThrowIfAllDelegatesAreNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<object>(null, null);
                    });
        }

        [Test]
        public void ShouldThrowIfCanExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<object>(o => { }, null);
                    });
        }

        [Test]
        public void ShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<object>(null);
                    });
        }

        [Test]
        public void WhenConstructedWithGenericTypeIsNonNullableValueType_Throws()
        {
            Assert.Throws<InvalidCastException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand<int>(param => { });
                    });
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfNullable_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new ActivatablePropertyObserverCommand<int?>(param => { });

            // verify
            Assert.NotNull(actual);
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfObject_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new ActivatablePropertyObserverCommand<object>(param => { });

            // verify
            Assert.NotNull(actual);
        }

        internal void DoNothing(object param)
        {
        }
    }
}