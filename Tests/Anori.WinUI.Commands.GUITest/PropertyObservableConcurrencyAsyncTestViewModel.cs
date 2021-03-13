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

    using JetBrains.Annotations;

    public class PropertyObservableConcurrencyAsyncTestViewModel : PropertyObservableTestViewModelBase
    {
       
        public PropertyObservableConcurrencyAsyncTestViewModel()
        {
            static async Task ConcurrencyAsyncExecution(CancellationToken c) => await Task.Yield();

            this.TestAndCommand = CommandBuilder.Builder.Command(ConcurrencyAsyncExecution)
                .ObservesCanExecute(() => this.Condition1 && this.Condition2)
                .Build();

            this.TestOrCommand = CommandBuilder.Builder.Command(ConcurrencyAsyncExecution)
                .ObservesCanExecute(() => this.Condition1 || this.Condition2)
                .Build();
        }
    }
}