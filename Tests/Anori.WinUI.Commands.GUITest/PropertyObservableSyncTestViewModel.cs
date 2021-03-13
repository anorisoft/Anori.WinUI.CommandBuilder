// -----------------------------------------------------------------------
// <copyright file="PropertyObservableSyncTestViewModel.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest
{
    using Anori.WinUI.Commands.Builder;

    public class PropertyObservableSyncTestViewModel : PropertyObservableTestViewModelBase
    {

        public PropertyObservableSyncTestViewModel()
        {
            static void SyncExecution() { }

            this.TestAndCommand = CommandBuilder.Builder.Command(SyncExecution)
                .ObservesCanExecute(() => this.Condition1 && this.Condition2)
                .Build();

            this.TestOrCommand = CommandBuilder.Builder.Command(SyncExecution)
                .ObservesCanExecute(() => this.Condition1 || this.Condition2)
                .Build();
        }

    }
}