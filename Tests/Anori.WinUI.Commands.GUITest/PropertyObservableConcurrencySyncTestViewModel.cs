// -----------------------------------------------------------------------
// <copyright file="PropertyObservableConcurrencySyncTestViewModel.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest
{
    using System.Threading;

    using Anori.WinUI.Commands.Builder;

    public class PropertyObservableConcurrencySyncTestViewModel : PropertyObservableTestViewModelBase
    {

        public PropertyObservableConcurrencySyncTestViewModel()
        {
            static void ConcurrencySyncExecution(CancellationToken c) { }

            this.TestAndCommand = CommandBuilder.Builder.Command(ConcurrencySyncExecution)
                .ObservesCanExecute(() => this.Condition1 && this.Condition2)
                .Build();

            this.TestOrCommand = CommandBuilder.Builder.Command(ConcurrencySyncExecution)
                .ObservesCanExecute(() => this.Condition1 || this.Condition2)
                .Build();
        }

    }
}