// -----------------------------------------------------------------------
// <copyright file="CommandManagerCommandOfTFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WinUI.Commands.Commands;

namespace Anori.WinUI.Commands.Tests
{
    using Anori.WinUI.Commands.Interfaces;

    using NUnit.Framework;

    using System;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Anori.WinUI.Commands.CanExecuteObservers;

    using ICommand = System.Windows.Input.ICommand;

    /// <summary>
    ///     Summary description for ObservableCommandFixture
    /// </summary>
    [TestFixture]
    public class CommandManagerCommandOfTFixture
    {
        [Test]
        public void CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(
                handlers.Execute,
                handlers.CanExecute,
                new CommandManagerObserver());
            var parameter = new object();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(parameter);

            Assert.AreSame(parameter, handlers.CanExecuteParameter);
            Assert.AreEqual(handlers.CanExecuteReturnValue, actual);
        }

        [Test]
        public void CanExecuteReturnsTrueWithoutCanExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, new CommandManagerObserver());

            var condition = command.CanExecute(null);

            Assert.True(condition);
        }

        [Test]
        public void CanRemoveCanExecuteChangedHandler()
        {
            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, new CommandManagerObserver());
            var canExecuteChangedRaised = false;

            void Handler(object s, EventArgs e) => canExecuteChangedRaised = true;

            command.CanExecuteChanged += Handler;
            command.CanExecuteChanged -= Handler;

            CommandManager.InvalidateRequerySuggested();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void ExecuteCallsPassedInExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, new CommandManagerObserver()) as ICommand;
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(parameter, handlers.ExecuteParameter);
        }

        [Test]
        public void ExecuteCallsOfTPassedInExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, new CommandManagerObserver());
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(parameter, handlers.ExecuteParameter);
        }

        [Test]
        public void ExecuteCallsCanExecuteTrue()
        {
            var handlers = new DelegateObjectHandlers();
            var command =
                new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, o => true, new CommandManagerObserver()) as ICommand;
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(parameter, handlers.ExecuteParameter);
        }

        [Test]
        public void ExecuteCallsOfTCanExecuteTrue()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, o => true, new CommandManagerObserver());
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(parameter, handlers.ExecuteParameter);
        }

        [Test]
        public void ExecuteCallsCanExecuteFalse()
        {
            var handlers = new DelegateObjectHandlers();
            var command =
                new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, o => false, new CommandManagerObserver()) as ICommand;
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(null, handlers.ExecuteParameter);
        }

        [Test]
        public void ExecuteCallsOfTCanExecuteFalse()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, o => false, new CommandManagerObserver());
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(null, handlers.ExecuteParameter);
        }

        [Test]
        public void GenericObservableCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, new CommandManagerObserver());

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedNoRaiseCanExecuteChanged()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute, o => true, new CommandManagerObserver());
            var canExecuteChangedRaised = false;

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var canExecuteChangedRaised = false;

            var handlers = new DelegateObjectHandlers();
            var command = new ActivatableCanExecuteObserverCommand<object>(handlers.Execute,true, o => false, new CommandManagerObserver());

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            CommandManager.InvalidateRequerySuggested();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void ShouldPassParameterInstanceOnCanExecute()
        {
            var canExecuteCalled = false;
            var testClass = new MyClass();
            ICommand command = new ActivatableCanExecuteObserverCommand<MyClass>(
                p => { },
                delegate(MyClass parameter)
                    {
                        Assert.AreSame(testClass, parameter);
                        canExecuteCalled = true;
                        return true;
                    },
                new CommandManagerObserver());

            command.CanExecute(testClass);

            Assert.True(canExecuteCalled);
        }

        [Test]
        public void ShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand<object>(null, new CommandManagerObserver());
                    });
        }

        [Test]
        public void ShouldThrowIfExecuteMethodDelegateNullAndObservableCommandNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand<object>(null, null as IPropertyObserver);
                    });
        }

        [Test]
        public void ShouldThrowIfObservableCommandNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new ActivatableCanExecuteObserverCommand<object>(o => { }, null as IPropertyObserver);
                    });
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfNullable_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new ActivatableCanExecuteObserverCommand<int?>(param => { }, new CommandManagerObserver());

            // verify
            Assert.NotNull(actual);
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfObject_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new ActivatableCanExecuteObserverCommand<object>(param => { }, new CommandManagerObserver());

            // verify
            Assert.NotNull(actual);
        }
    }
}