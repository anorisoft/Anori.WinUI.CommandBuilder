// -----------------------------------------------------------------------
// <copyright file="DelegateHandlers.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Tests
{
    using System;

    public class DelegateHandlers : INotifyPropertyChanged
    {
        public bool CanExecuteReturnValue
        {
            get => canExecuteReturnValue;
            set
            {
                if (value == canExecuteReturnValue) return;
                canExecuteReturnValue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanExecuteReturnValue));

            }
        }

        public bool CanExecuteValueAndCount
        {
            get
            {
                CanExecuteCount++;
                return canExecuteReturnValue;
            }
            set
            {
                if (value == canExecuteReturnValue) return;
                canExecuteReturnValue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanExecuteReturnValue));
            }
        }

        private bool observableBoolean;
        private int obserableInteger;
        private string observableString;
        private bool canExecuteReturnValue = true;

        public bool CanExecute()
        {
            CanExecuteCount++;
            return this.CanExecuteReturnValue;
        }

        public int CanExecuteCount { get; private set; }

        public void Execute()
        {
            ExecuteCount++;
        }
        public async Task ExecuteAsync()
        {
            await Task.Yield();
            ExecuteCount++;
        }
        public int ExecuteCount { get; private set; }
        private event PropertyChangedEventHandler PropertyChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this.PropertyChanged += value;
            }
            remove => this.PropertyChanged -= value;
        }

        public bool ObservableBoolean
        {
            get => observableBoolean;
            set
            {
                if (value == observableBoolean) return;
                observableBoolean = value;
                OnPropertyChanged();
            }
        }

        public int ObserableInteger
        {
            get => obserableInteger;
            set
            {
                if (value == obserableInteger) return;
                obserableInteger = value;
                OnPropertyChanged();
            }
        }


        public string ObservableString
        {
            get => observableString;
            set
            {
                if (value == observableString) return;
                observableString = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}