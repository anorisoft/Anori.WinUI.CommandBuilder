// -----------------------------------------------------------------------
// <copyright file="PropertyObservableAsyncTestViewModel.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest
{
    using System.Threading.Tasks;

    using Anori.WinUI.Commands.Builder;

    public class PropertyObservableAsyncTestViewModel : PropertyObservableTestViewModelBase
    {

        public PropertyObservableAsyncTestViewModel()
        {
            static async Task AsyncExecution() => await Task.Yield();

            this.TestAndCommand = CommandBuilder.Builder.Command(AsyncExecution)
                .ObservesCanExecute(() => this.Condition1 && this.Condition2)
                .Build();

            this.TestOrCommand = CommandBuilder.Builder.Command(AsyncExecution)
                .ObservesCanExecute(() => this.Condition1 || this.Condition2)
                .Build();
        }
    }
}