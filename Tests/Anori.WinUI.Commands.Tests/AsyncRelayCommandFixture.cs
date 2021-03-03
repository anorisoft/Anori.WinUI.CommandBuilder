// -----------------------------------------------------------------------
// <copyright file="AsyncRelayCommandFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;

namespace Anori.WinUI.Commands.Tests
{
    using Anori.WinUI.Common;

    using CanExecuteChangedTests;

    using NUnit.Framework;

    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using global::CanExecuteChangedTests;

    /// <summary>
    ///     Summary description for AsyncRelayCommandFixture
    /// </summary>
    [TestFixture]
    public class AsyncRelayCommandFixture
    {
        [Test]
        public void AsyncRelayCommandShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new AsyncRelayCommand(null);
                    });
        }

        [Test]
        public void CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new AsyncDelegateObjectHandlers();
            var command = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(async o => await handlers.Execute(o), handlers.CanExecute);
            var parameter = new object();

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute(parameter);

            Assert.AreSame(parameter, handlers.CanExecuteParameter);
            Assert.AreEqual(handlers.CanExecuteReturnValue, actual);
        }

        [Test]
        public void CanExecuteReturnsTrueWithoutCanExecuteDelegate()
        {
            var handlers = new AsyncDelegateObjectHandlers();
            var command = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(async o => await handlers.Execute(o));

            var condition = command.CanExecute(null);

            Assert.True(condition);
        }

        [Test]
        public void CanRemoveCanExecuteChangedHandler()
        {
            var command = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(async o => await Task.Yield());
            var canExecuteChangedRaised = false;

            void Handler(object s, EventArgs e) => canExecuteChangedRaised = true;

            command.CanExecuteChanged += Handler;
            command.CanExecuteChanged -= Handler;
            command.RaiseCanExecuteChanged();

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void ExecuteCallsPassedInExecuteDelegate()
        {
            var handlers = new AsyncDelegateObjectHandlers();
            using var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            var command = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(async o =>
            {
                await handlers.Execute(o);
                waitHandle.Set();
            }) as ICommand;
            var parameter = new object();

            command.Execute(parameter);
            waitHandle.WaitOne();
            Assert.AreSame(parameter, handlers.ExecuteParameter);
        }

        [Test]
        public void GenericAsyncRelayCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(async o => await Task.Yield());

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void NonGenericAsyncRelayCommandCanExecuteShouldInvokeCanExecuteFunc()
        {
            var invoked = false;
            var command = new AsyncRelayCommand(
                async () => await Task.Yield(),
                () =>
                    {
                        invoked = true;
                        return true;
                    });

            var canExecute = command.CanExecute();

            Assert.True(invoked);
            Assert.True(canExecute);
        }

        [Test]
        public async Task NonGenericAsyncRelayCommandExecuteShouldInvokeExecuteAction()
        {
            var executed = false;
            var command = new AsyncRelayCommand(
                              async () =>
                                  {
                                      await Task.Delay(50);
                                      executed = true;
                                  }) as ICommand;
            command.Execute(new object());

            Assert.False(executed);
            await Task.Delay(100);
            Assert.True(executed);
        }

        [Test]
        public void NonGenericAsyncRelayCommandExecuteShouldInvokeExecuteActionErrorHandler()
        {
            var executed = false;
            var hasError = false;
            using  var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

            var command = new AsyncRelayCommand(
                              async () =>
                                  {
                                      await Task.Yield();
                                      executed = true;
                                      waitHandle.Set();
                                  },
                              ex => hasError = true) as ICommand;
            command.Execute(new object());

            waitHandle.WaitOne();
            Task.Delay(50);
            Assert.False(hasError);
            Assert.True(executed);
        }

        [Test]
        public void NonGenericAsyncRelayCommandExecuteShouldInvokeExecuteActionException()
        {
            var executed = false;
            var command = new AsyncRelayCommand(
                              async () =>
                                  {
                                      await Task.Yield();
                                      throw new Exception("Test Exception");
                                  }) as ICommand;
            command.Execute(new object());

            Assert.False(executed);
        }

        [Test]
        public async Task NonGenericAsyncRelayCommandExecuteShouldInvokeExecuteActionException2()
        {
            Exception exception = null;
            var command = new AsyncRelayCommand(
                              async () =>
                                  {
                                      await Task.Delay(50);
                                      throw new Exception("Test Exception");
                                  },
                              ex => exception = ex) as ICommand;
            command.Execute(new object());

            Assert.Null(exception);
            await Task.Delay(100);
            Assert.NotNull(exception);
            Assert.AreEqual("Test Exception", exception.Message);
        }

        [Test]
        public void NonGenericAsyncRelayCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();

            var command = new AsyncRelayCommand(async () => await Task.Yield());

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void NonGenericAsyncRelayCommandThrowsIfExecuteDelegateIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new AsyncRelayCommand(null);
                    });
        }

        [Test]
        public void RaiseCanExecuteChangedNoCanExecuteNoRaiseCanExecuteChanged()
        {
            var handlers = new AsyncDelegateObjectHandlers();
            var command = new WinUI.Commands.AsyncRelayCommand<object>(async o => await handlers.Execute(o));
            var canExecuteChangedRaised = false;
            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            command.RaiseCanExecuteChanged();
            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var handlers = new AsyncDelegateObjectHandlers();
            var command = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(async o => await handlers.Execute(o), o => false);
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
            ICommand command = new global::Anori.WinUI.Commands.AsyncRelayCommand<MyClass>(
                async o => await Task.Yield(),
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
        public void ShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(null);
                    });
        }

        [Test]
        public void WhenConstructedWithGenericTypeAndCanExecuteIsNonNullableValueType_Throws()
        {
            ICommand command = new global::Anori.WinUI.Commands.AsyncRelayCommand<int>(async o => await Task.Yield(), i => true, ex => { });
            Assert.Throws<InvalidCastException>(() => { command.Execute(new object()); });
        }

        [Test]
        public void WhenConstructedWithGenericTypeIsNonNullableValueType_Throws()
        {
            ICommand command = new global::Anori.WinUI.Commands.AsyncRelayCommand<int>(async o => await Task.Yield(), exception => { });
            Assert.Throws<InvalidCastException>(() => { command.Execute(new object()); });
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfNullable_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new global::Anori.WinUI.Commands.AsyncRelayCommand<int?>(async o => await Task.Yield());

            // verify
            Assert.NotNull(actual);
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfObject_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new global::Anori.WinUI.Commands.AsyncRelayCommand<object>(async o => await Task.Yield());

            // verify
            Assert.NotNull(actual);
        }
    }
}