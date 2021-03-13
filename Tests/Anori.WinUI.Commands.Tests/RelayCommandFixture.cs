// -----------------------------------------------------------------------
// <copyright file="RelayCommandFixture.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using NUnit.Framework;

    using System;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Anori.WinUI.Commands.Commands;

    using ICommand = System.Windows.Input.ICommand;

    /// <summary>
    ///     Summary description for RelayCommandFixture
    /// </summary>
    [TestFixture]
    public class RelayCommandFixture
    {
        [Test]
        public void CanExecuteCallsPassedInCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command = new CanExecuteObserverCommand(handlers.Execute, handlers.CanExecute);

            handlers.CanExecuteReturnValue = true;
            var actual = command.CanExecute();

            Assert.AreEqual(handlers.CanExecuteReturnValue, actual);
        }

        [Test]
        public void CanExecuteReturnsTrueWithoutCanExecuteDelegate()
        {
            var handlers = new DelegateHandlers();
            var command = new CanExecuteObserverCommand(handlers.Execute);

            var condition = command.CanExecute();

            Assert.True(condition);
        }

        [Test]
        public void CanExecuteWithExceptionWithoutCanExecuteDelegate()
        {
            var command = new CanExecuteObserverCommand(() => { }, () => throw new Exception("Test Exception"));

            Assert.Throws<Exception>(() => command.CanExecute());
        }

        [Test]
        public void CanRemoveCanExecuteChangedHandler()
        {
            var command = new CanExecuteObserverCommand(() => { });
            var canExecuteChangedRaised = false;

            void Handler(object s, EventArgs e) => canExecuteChangedRaised = true;

            command.CanExecuteChanged += Handler;
            command.CanExecuteChanged -= Handler;

            CommandManager.InvalidateRequerySuggested();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedNoRaiseCanExecuteChanged()
        {
            var handlers = new DelegateHandlers();
            var command = new CanExecuteObserverCommand(handlers.Execute, () => true);
            var canExecuteChangedRaised = false;

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
        }

        /// <summary>
        /// Raises the can execute with exception changed no raise can execute changed.
        /// </summary>
        [Test]
        public void RaiseCanExecuteWithExceptionChangedNoRaiseCanExecuteChanged()
        {
            var handlers = new DelegateHandlers();
            var command = new CanExecuteObserverCommand(handlers.Execute, () => true);
            var canExecuteChangedRaised = false;

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RaiseCanExecuteChangedRaisesCanExecuteChanged()
        {
            var canExecuteChangedRaised = false;

            var handlers = new DelegateHandlers();
            var command = new CanExecuteObserverCommand(handlers.Execute, () => false);

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            CommandManager.InvalidateRequerySuggested();
            Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action(() => { }));

            Assert.True(canExecuteChangedRaised);
        }

        [Test]
        public void RelayCommandCanExecuteShouldInvokeCanExecuteFunc()
        {
            var invoked = false;
            var command = new CanExecuteObserverCommand(
                () => { },
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
        public void RelayCommandExecuteShouldInvokeExecuteAction()
        {
            var executed = false;
            var command = new CanExecuteObserverCommand(() => executed = true) as ICommand;
            command.Execute(new object());

            Assert.True(executed);
        }

        [Test]
        public void RelayCommandExecuteWithExceptionShouldInvokeExecuteAction()
        {
            var command = new CanExecuteObserverCommand(() => throw new Exception("Test Exception")) as ICommand;

            Assert.Throws<Exception>(() => command.Execute(new object()));
        }

        [Test]
        public void RelayCommandNotObservingPropertiesShouldNotRaiseOnEmptyPropertyName()
        {
            var canExecuteChangedRaised = false;
            var commandTestObject = new CommandTestObject();
            var command = new CanExecuteObserverCommand(() => { });

            command.CanExecuteChanged += delegate { canExecuteChangedRaised = true; };

            commandTestObject.RaisePropertyChanged(null);

            Assert.False(canExecuteChangedRaised);
        }

        [Test]
        public void RelayCommandShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new CanExecuteObserverCommand(null);
                    });
        }

        [Test]
        public void RelayCommandThrowsIfExecuteDelegateIsNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new CanExecuteObserverCommand(null);
                    });
        }

        [Test]
        public void ShouldThrowIfExecuteMethodDelegateNull()
        {
            Assert.Throws<ArgumentNullException>(
                () =>
                    {
                        var command = new CanExecuteObserverCommand(null);
                    });
        }
    }
}