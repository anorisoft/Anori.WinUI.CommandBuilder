// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanExecuteChanged.Tests
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Threading;

    using Anori.WinUI.Commands;

    using CanExecuteChangedTests;

    using JetBrains.Annotations;

    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer timer;

        private readonly MainWindow window;

        private string text;

        public MainViewModel(MainWindow window)
        {
            this.window = window;
            var command = new DirectCommand(this.Execute, this.CanExecute);
            command.RaiseCanExecuteChanged();
            this.TestCommand = command;
            this.timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            this.timer.Tick += (e, a) =>
                {
                    this.Text = DateTime.Now.ToLongTimeString();
                    command.RaiseCanExecuteChanged();
                    // window.InvalidateMeasure();
                    // window.InvalidateVisual();
                    window.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                };
            //this.timer.Start();
        }

        public ICommand TestCommand { get; }

        public bool Toggle { get; set; }

        public string Text
        {
            get => this.text;
            set
            {
                if (value == this.text)
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool CanExecute()
        {
            this.Toggle = !this.Toggle;
            Debug.WriteLine("CanExecute");
            return this.Toggle;
        }

        private void Execute()
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}