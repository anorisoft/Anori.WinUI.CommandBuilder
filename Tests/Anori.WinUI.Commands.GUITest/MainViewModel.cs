// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="AnoriSoft">
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

    using Anori.WinUI.Commands.Commands;
    using Anori.WinUI.Commands.GUITest.Thiriet;

    using JetBrains.Annotations;

    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly MainWindow window;
        
        public MainViewModel(MainWindow window)
        {
 
            this.window = window;
            this.PropertyObservableConcurrencyAsyncTestCommand = new DirectCommand(() => new PropertyObservableConcurrencyAsyncTestView().ShowDialog());
            this.PropertyObservableConcurrencyAsyncTestCommand.RaiseCanExecuteChanged();

            this.PropertyObservableAsyncTestCommand = new DirectCommand(() => new PropertyObservableAsyncTestView().ShowDialog());
            this.PropertyObservableAsyncTestCommand.RaiseCanExecuteChanged();

            this.PropertyObservableConcurrencySyncTestCommand = new DirectCommand(() => new PropertyObservableConcurrencySyncTestView().ShowDialog());
            this.PropertyObservableConcurrencySyncTestCommand.RaiseCanExecuteChanged();

            this.PropertyObservableSyncTestCommand = new DirectCommand(() => new PropertyObservableSyncTestView().ShowDialog());
            this.PropertyObservableSyncTestCommand.RaiseCanExecuteChanged();

            PropertyObservableNullReferenceCommand = new DirectCommand(() => new PropertyObservableNullReferenceTest().ShowDialog());
            ConcurrencyTestCommand = new DirectCommand(() => new General.MainWindow().ShowDialog());
        }

        public DirectCommand ConcurrencyTestCommand { get; }


        public DirectCommand PropertyObservableConcurrencyAsyncTestCommand { get; }
        public DirectCommand PropertyObservableAsyncTestCommand { get; }
        public DirectCommand PropertyObservableConcurrencySyncTestCommand { get; }
        public DirectCommand PropertyObservableSyncTestCommand { get; }

        public DirectCommand PropertyObservableNullReferenceCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}