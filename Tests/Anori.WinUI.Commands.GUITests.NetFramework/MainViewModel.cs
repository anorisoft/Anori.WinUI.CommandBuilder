// -----------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="bfa solutions ltd">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Anori.WinUI.Commands.Builder;
using Anori.WinUI.Commands.Commands;
using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Interfaces;

namespace Anori.WinUI.Commands.GuitTests.NetFramework
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly MainWindow window;

        private bool throwException;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="window">The window.</param>
        public MainViewModel(MainWindow window)
        {
            this.window = window;
            ConcurrencyCommand = CommandBuilder.Builder.Command(async (CancellationToken token) => await ExecuteWithToken(token), CanExecute).Build();
            ConcurrencyCommand.CanExecuteChanged += ConcurrencyCommandOnCanExecuteChanged;
        }

        /// <summary>
        /// Gets the concurrency command.
        /// </summary>
        /// <value>
        /// The concurrency command.
        /// </value>
        public IConcurrencyAsyncCommand ConcurrencyCommand { get; }

        public bool ThrowException
        {
            get => throwException;
            set
            {
                if (value == throwException)
                {
                    return;
                }
                throwException = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Concurrencies the command on can execute changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ConcurrencyCommandOnCanExecuteChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Executes the with token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <exception cref="Exception">Test Exception</exception>
        private async Task ExecuteWithToken(CancellationToken token)
        {
            Debug.WriteLine("Executing");
            try
            {
                if (ThrowException)
                {
                    throw new Exception("Test Exception");
                }

                var cancelled = token.WaitHandle.WaitOne(TimeSpan.FromSeconds(5));
                if (cancelled)
                {
                    Debug.WriteLine("Cancelled");
                    token.ThrowIfCancellationRequested();
                }
            }
            finally
            {
                Debug.WriteLine("Executed");
            }
        }

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecute()
        {
            Debug.WriteLine("CanExecute");
            return true;
        }
    }
}