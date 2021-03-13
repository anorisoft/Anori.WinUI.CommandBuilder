// -----------------------------------------------------------------------
// <copyright file="PropertyObservableTestViewModel.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest
{
    using Anori.WinUI.Commands.Builder;
    using System.Threading;
    using System.Threading.Tasks;

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