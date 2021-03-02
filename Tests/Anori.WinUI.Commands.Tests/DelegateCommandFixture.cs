// -----------------------------------------------------------------------
// <copyright file="DelegateCommandFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using NUnit.Framework;

    using System;
    using System.Threading;
    using System.Windows.Input;

    using Anori.WinUI.Common;

    /// <summary>
    ///     Summary description for DelegateCommandFixture
    /// </summary>
    [TestFixture]
    public class DelegateCommandFixture
    {
        [SetUp]
        public void Init()
        {
            SynchronizationContext.SetSynchronizationContext(new TestSynchronizationContext());
        }

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
        public void CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command = new ActivatablePropertyObserverCommand(handlers.Execute, handlers.CanExecute);
            command.Activate();
            handlers.CanExecuteReturnValue = true;
            var canExecute = command.CanExecute();

            Assert.AreEqual(handlers.CanExecuteReturnValue, canExecute);
        }

        [Test]
        public void CanExecuteOfObjectReturnsTrueWithouthCanExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatablePropertyObserverCommand<object>(handlers.Execute);

            var retVal = command.CanExecute(null);

            Assert.True(retVal);
        }

        [Test]
        public void CanExecuteReturnsTrueWithouthCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command = new ActivatablePropertyObserverCommand(handlers.Execute);
            command.Activate();
            var canExecute = command.CanExecute();

            Assert.True(canExecute);
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
        public void CanRemoveCanExecuteChangedHandler()
        {
            var command = new ActivatablePropertyObserverCommand(() => { });

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
        public void DelegateCommandGenericShouldThrowIfCanExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(() => { }, null);
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
        public void DelegateCommandShouldThrowIfAllDelegatesAreNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(null, null);
                    });
        }

        [Test]
        public void DelegateCommandShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(null);
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
        public void GenericDelegateCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new ActivatablePropertyObserverCommand(() => { });

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
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            commandTestObject.RaisePropertyChanged(string.Empty);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandObservingPropertyShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    () => commandTestObject.IntProperty);
            command.Activate();
            command.CanExecuteChanged += delegate
            {
                canExecuteChangedRaised = true;
            };
            commandTestObject.IntProperty = 5;
            commandTestObject.RaisePropertyChanged("IntProperty");

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectObservingPropertyWithOwnerShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(
                    commandTestObject,
                    o => o.IntProperty);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };
            commandTestObject.RaisePropertyChanged(string.Empty);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandObservingPropertyWithOwnerShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    commandTestObject,
                    o => o.IntProperty);

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
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandObservingPropertyShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandOfObjectObservingPropertyWithOwnerShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(
                    commandTestObject,
                    o => o.IntProperty);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandObservingPropertyWithOwnerShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    commandTestObject,
                    o => o.IntProperty);

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
        public void GenericDelegateCommandPropertyObserverUnsubscribeUnusedListeners()
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

            var command = new ActivatablePropertyObserverCommand(() => { })
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
        public void GenericDelegateCommandPropertyObserverWithOwnerUnsubscribeUnusedListeners()
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

            var command = new ActivatablePropertyObserverCommand(() => { })
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
        public void GenericDelegateCommandShouldNotObserveDuplicateCanExecute()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        ICommand command = new ActivatablePropertyObserverCommand(() => { })
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
        public void GenericDelegateCommandShouldNotObserveWithOwnerDuplicateCanExecute()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        ICommand command = new ActivatablePropertyObserverCommand(() => { })
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
        public void GenericDelegateCommandShouldNotObserveDuplicateProperties()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(() => { })
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
        public void GenericDelegateCommandShouldNotObserveWithOwnerDuplicateProperties()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(() => { })
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
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesCanExecute(
                    commandTestObject.BoolPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void GenericDelegateCommandShouldObserveCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesCanExecute(
                    commandTestObject.BoolPropertyExpression).Activate();
  
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
                new ActivatablePropertyObserverCommand<object>(o1 => { }).ObservesCanExecute(
                    commandTestObject,
                    o2 => o2.BoolProperty);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void GenericDelegateCommandShouldObserveWithOwnerCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesCanExecute(
                    commandTestObject,
                    o2 => o2.BoolProperty).Activate();
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
        public void GenericDelegateCommandShouldObserveCanExecuteAndObserveOtherProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command = new ActivatablePropertyObserverCommand(() => { })
                .ObservesCanExecute(commandTestObject.BoolPropertyExpression)
                .ObservesProperty(commandTestObject.IntPropertyExpression).Activate();
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
        public void GenericDelegateCommandShouldObserveComplexPropertyWhenParentPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = new ComplexType() };
            var command = new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
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
        public void GenericDelegateCommandShouldObserveComplexPropertyWhenRootPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = null };

            var command = new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
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
        public void GenericDelegateCommandShouldObserveMultipleProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            var command = new ActivatablePropertyObserverCommand(() => { })
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
                new ActivatablePropertyObserverCommand<object>(o => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void GenericDelegateCommandShouldObserveOneProperty()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

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
                new ActivatablePropertyObserverCommand<int?>(o => { }).ObservesCanExecute(
                    commandTestObject.BoolPropertyExpression);

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
            var command = new ActivatablePropertyObserverCommand<object>(this.DoNothingOfObject);
            command.IsActiveChanged += delegate { fired = true; };
            command.Activate();

            Assert.True(fired);
        }

        [Test]
        public void IsActivePropertyChangeFiresEvent()
        {
            var fired = false;
            var command = new ActivatablePropertyObserverCommand(this.DoNothing);
            command.IsActiveChanged += delegate { fired = true; };
            command.Activate();

            Assert.True(fired);
        }

        [Test]
        public void IsActivePropertyIsFalseByDefaultOfObject()
        {
            var command = new ActivatablePropertyObserverCommand<object>(this.DoNothingOfObject);
            Assert.False(command.IsActive);
        }

        [Test]
        public void IsActivePropertyIsFalseByDefault()
        {
            var command = new ActivatablePropertyObserverCommand(this.DoNothing);
            Assert.False(command.IsActive);
        }

        [Test]
        public void NonGenericDelegateCommandCanExecuteShouldInvokeCanExecuteFunc()
        {
            var invoked = false;
            var command = new ActivatablePropertyObserverCommand(
                () => { },
                () =>
                    {
                        invoked = true;
                        return true;
                    });
            command.Activate();
            var canExecute = command.CanExecute();

            Assert.True(invoked);
            Assert.True(canExecute);
        }

        [Test]
        public void NonGenericDelegateCommandExecuteShouldInvokeExecuteAction()
        {
            var executed = false;
            var command = new ActivatablePropertyObserverCommand(() => { executed = true; });
            command.Activate();
            command.Execute();

            Assert.True(executed);
        }

        [Test]
        public void NonGenericDelegateCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            var command = new ActivatablePropertyObserverCommand(() => { });

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void NonGenericDelegateCommandObservingPropertyShouldRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(string.Empty);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void NonGenericDelegateCommandObservingPropertyShouldRaiseOnNullPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void NonGenericDelegateCommandPropertyObserverUnsubscribeUnusedListeners()
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

            var command = new ActivatablePropertyObserverCommand(() => { })
                .ObservesProperty(commandTestObject.ComplexPropertyIntPropertyExpression)
                .ObservesProperty(
                    commandTestObject.ComplexPropertyInnerComplexPropertyInnerComplexPropertyIntPropertyExpression)
                .ObservesProperty(commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);
            command.Activate();
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
        public void NonGenericDelegateCommandShouldDefaultCanExecuteToTrue()
        {
            var command = new ActivatablePropertyObserverCommand(() => { });
            command.Activate();
            Assert.True(command.CanExecute());
        }

        [Test]
        public void NonGenericDelegateCommandShouldDefaultCanExecuteToFalse()
        {
            var command = new ActivatablePropertyObserverCommand(() => { });
            Assert.False(command.CanExecute());
        }

        [Test]
        public void NonGenericDelegateCommandShouldNotObserveDuplicateCanExecute()
        {
            var commandTestObject = new CommandTestObject();
            Assert.Throws<ArgumentException>(
                () =>
                    {
                        ICommand command = new ActivatablePropertyObserverCommand(() => { })
                            .ObservesCanExecute(commandTestObject.BoolPropertyExpression)
                            .ObservesCanExecute(commandTestObject.BoolPropertyExpression);
                    });
        }

        [Test]
        public void NonGenericDelegateCommandShouldNotObserveDuplicateProperties()
        {
            var commandTestObject = new CommandTestObject();

            Assert.Throws<ArgumentException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(() => { })
                            .ObservesProperty(commandTestObject.IntPropertyExpression)
                            .ObservesProperty(commandTestObject.IntPropertyExpression);
                    });
        }

        [Test]
        public void NonGenericDelegateCommandShouldObserveCanExecute()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesCanExecute(
                    commandTestObject.BoolPropertyExpression).Activate();

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
            Assert.False(command.CanExecute(null));

            commandTestObject.BoolProperty = true;

            Assert.True(canExecuteChangedRaised);
            Assert.True(command.CanExecute(null));
        }

        [Test]
        public void NonGenericDelegateCommandShouldObserveCanExecuteAndObserveOtherProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            ICommand command = new ActivatablePropertyObserverCommand(() => { })
                .ObservesCanExecute(commandTestObject.BoolPropertyExpression)
                .ObservesProperty(commandTestObject.IntPropertyExpression).Activate();

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
        public void NonGenericDelegateCommandShouldObserveComplexPropertyWhenParentPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = new ComplexType() };

            var command = new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaise = true; };

            var newComplexObject = new ComplexType { InnerComplexProperty = new ComplexType { IntProperty = 10 } };

            commandTestObject.ComplexProperty.InnerComplexProperty = newComplexObject;

            Assert.True(canExecuteChangedRaise);
        }

        [Test]
        public void NonGenericDelegateCommandShouldObserveComplexPropertyWhenRootPropertyIsNull()
        {
            var canExecuteChangedRaise = false;
            var commandTestObject = new CommandTestObject { ComplexProperty = null };

            var command = new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaise = true; };

            var newComplexObject = new ComplexType { InnerComplexProperty = new ComplexType { IntProperty = 10 } };

            commandTestObject.ComplexProperty = newComplexObject;

            Assert.True(canExecuteChangedRaise);
        }

        [Test]
        public void NonGenericDelegateCommandShouldObserveMultipleProperties()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            var command = new ActivatablePropertyObserverCommand(() => { })
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
        public void NonGenericDelegateCommandShouldObserveOneComplexProperty()
        {
            var commandTestObject = new CommandTestObject
            {
                ComplexProperty =
                                                new ComplexType { InnerComplexProperty = new ComplexType() }
            };

            var canExecuteChangedRaised = false;

            var command = new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                commandTestObject.ComplexPropertyInnerComplexPropertyIntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.ComplexProperty.InnerComplexProperty.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void NonGenericDelegateCommandShouldObserveOneProperty()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            var command =
                new ActivatablePropertyObserverCommand(() => { }).ObservesProperty(
                    commandTestObject.IntPropertyExpression);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.IntProperty = 10;

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void NonGenericDelegateCommandThrowsIfCanExecuteDelegateIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(() => { }, null);
                    });
        }

        [Test]
        public void NonGenericDelegateCommandThrowsIfExecuteDelegateIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(null);
                    });
        }

        [Test]
        public void NonGenericDelegateThrowsIfDelegatesAreNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatablePropertyObserverCommand(null, null);
                    });
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

        internal void DoNothingOfObject(object param)
        {
        }

        internal void DoNothing()
        {
        }
    }
}