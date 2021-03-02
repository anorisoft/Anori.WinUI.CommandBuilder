// -----------------------------------------------------------------------
// <copyright file="RelayCommandOfTFixture.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using NUnit.Framework;

    using System;
    using System.Windows.Input;
    using System.Windows.Threading;

    using ICommand = System.Windows.Input.ICommand;

    /// <summary>
    ///     Summary description for RelayCommandFixture
    /// </summary>
    [TestFixture]
    public class RelayCommandOfTFixture
    {
        [Test]
        public void CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new RelayCommand<object>(handlers.Execute, handlers.CanExecute);
            var parameter = new object();

            handlers.CanExecuteReturnValue = true;
            var retVal = command.CanExecute(parameter);

            Assert.AreSame(parameter, handlers.CanExecuteParameter);
            Assert.AreEqual(handlers.CanExecuteReturnValue, retVal);
        }

        [Test]
        public void CanExecuteReturnsTrueWithouthCanExecuteDelegate()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new RelayCommand<object>(handlers.Execute);

            var retVal = command.CanExecute(null);

            Assert.True(retVal);
        }

        [Test]
        public void CanRemoveCanExecuteChangedHandler()
        {
            var command = new RelayCommand<object>(o => { });
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
            var command = new RelayCommand<object>(handlers.Execute) as ICommand;
            var parameter = new object();

            command.Execute(parameter);

            Assert.AreSame(parameter, handlers.ExecuteParameter);
        }

        [Test]
        public void GenericRelayCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new RelayCommand<object>(o => { });

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedNoRaiseCanExecuteChanged()
        {
            var handlers = new DelegateObjectHandlers();
            var command = new RelayCommand<object>(handlers.Execute, o => true);
            var canExecuteChangedRaised = false;
            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var canExecuteChangedRaised = false;

            var handlers = new DelegateObjectHandlers();
            var command = new RelayCommand<object>(handlers.Execute, o => false);

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
            ICommand command = new RelayCommand<MyClass>(
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
        public void ShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new RelayCommand<object>(null);
                    });
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfNullable_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new RelayCommand<int?>(param => { });

            // verify
            Assert.NotNull(actual);
        }

        [Test]
        public void WhenConstructedWithGenericTypeOfObject_InitializesValues()
        {
            // Prepare

            // Act
            var actual = new RelayCommand<object>(param => { });

            // verify
            Assert.NotNull(actual);
        }
    }
}