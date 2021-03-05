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
            this.DirectCommand = new DirectCommand(() =>new PropertyObservableTest().ShowDialog());
            this.DirectCommand.RaiseCanExecuteChanged();
            PropertyObservableNullReferenceCommand = new DirectCommand(() => new PropertyObservableNullReferenceTest().ShowDialog());
            ConcurrencyTestCommand = new DirectCommand(() => new General.MainWindow().ShowDialog());
        }

        public DirectCommand ConcurrencyTestCommand { get; }


        public DirectCommand DirectCommand { get; }

        public DirectCommand PropertyObservableNullReferenceCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}