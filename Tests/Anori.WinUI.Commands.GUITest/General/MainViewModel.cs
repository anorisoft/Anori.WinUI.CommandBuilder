// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WinUI.Commands.Builder;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Commands.GUITest.Thiriet;
using Anori.WinUI.Commands.Interfaces;

using JetBrains.Annotations;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Anori.WinUI.Commands.GUITest.General
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly MainWindow window;

        private string text;

        private ThirietViewModel thirietViewModel;

        private bool throwException;

        public MainViewModel(MainWindow window)
        {
            this.ThirietViewModel = new ThirietViewModel();
            this.window = window;
            this.DirectCommand = new DirectCommand(this.Execute, this.CanExecute);
            this.DirectCommand.RaiseCanExecuteChanged();

            this.AsyncCommand = new AsyncCommand(
                async () => await this.ExecuteAsync().ConfigureAwait(false),
                this.CanExecute);

            this.ConcurrencyCommand = CommandBuilder.Builder.Command(this.ExecuteWithToken, this.CanExecute).Build();
            this.ConcurrencyCommand.CanExecuteChanged += this.ConcurrencyCommandOnCanExecuteChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ThirietViewModel ThirietViewModel
        {
            get => this.thirietViewModel;
            set
            {
                if (Equals(value, this.thirietViewModel))
                {
                    return;
                }

                this.thirietViewModel = value;
                this.OnPropertyChanged();
            }
        }

        public AsyncCommand AsyncCommand { get; }

        public IConcurrencySyncCommand ConcurrencyCommand { get; }

        public DirectCommand DirectCommand { get; }

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

        public bool Toggle { get; set; }

        public bool ThrowException
        {
            get => this.throwException;
            set
            {
                if (value == this.throwException)
                {
                    return;
                }

                this.throwException = value;
                this.OnPropertyChanged();
            }
        }

        private void ConcurrencyCommandOnCanExecuteChanged(object sender, EventArgs e)
        {
        }

        private void ExecuteWithToken(CancellationToken token)
        {
            if (this.ThrowException)
            {
                throw new Exception("Test Exception");
            }

            var cancelled = token.WaitHandle.WaitOne(TimeSpan.FromSeconds(5));
            if (cancelled)
            {
                token.ThrowIfCancellationRequested();
            }

            //throw new Exception("Test Ex");
            //Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        private async Task ExecuteAsync()
        {
            await Task.Yield();
            this.Execute();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecute()
        {
            //this.Toggle = !this.Toggle;
            Debug.WriteLine("CanExecute");
            //return this.Toggle;
            return true;
        }

        private void Execute()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}