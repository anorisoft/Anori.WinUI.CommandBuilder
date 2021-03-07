// -----------------------------------------------------------------------
// <copyright file="PropertyObservableTestViewModel.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Anori.WinUI.Commands.Builder;
    using Anori.WinUI.Commands.CanExecuteObservers;
    using Anori.WinUI.Commands.Commands;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Common;

    using JetBrains.Annotations;

    public class PropertyObservableTestViewModel : INotifyPropertyChanged
    {
        private bool condition1;

        private bool condition2;

        public PropertyObservableTestViewModel()
        {
            var commandFactory = CommandBuilder.Builder;
            //var canExecuteObserverAnd =
            //    new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 && this.Condition2);
            //TestAndCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverAnd);

            Action syncExecution = () => { };
            Action<CancellationToken> concurrencySyncExecution = c => { };
            Func<CancellationToken, Task> concurrencyAsyncExecution = async c => await Task.Yield();

            IConcurrencyAsyncCommand testAndCommand;
            this.TestAndCommand = testAndCommand = commandFactory.Command(concurrencyAsyncExecution)
                                      .ObservesCanExecute(() => this.Condition1 && this.Condition2)
                                      .Build();

            ((IActivatable)testAndCommand).Activate();

            var canExecuteObserverOr =
                new PropertyObserverFactory().ObservesCanExecute(() => this.Condition1 || this.Condition2);
            this.TestOrCommand = new ActivatableCanExecuteObserverCommand(() => { }, canExecuteObserverOr);
            this.TestOrCommand.Activate();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TestAndCommand { get; }

        public bool Condition1
        {
            get => this.condition1;
            set
            {
                if (value == this.condition1)
                {
                    return;
                }

                this.condition1 = value;
                this.OnPropertyChanged();
            }
        }

        public bool Condition2
        {
            get => this.condition2;
            set
            {
                if (value == this.condition2)
                {
                    return;
                }

                this.condition2 = value;
                this.OnPropertyChanged();
            }
        }

        internal ActivatableCanExecuteObserverCommand TestOrCommand { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}