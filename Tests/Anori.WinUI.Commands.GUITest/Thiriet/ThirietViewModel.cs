// -----------------------------------------------------------------------
// <copyright file="ThirietViewModel.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest.Thiriet
{
    using System.Threading.Tasks;

    public class ThirietViewModel : ViewModelBase
    {
        private bool isBusy;

        public ThirietViewModel()
        {
            this.Submit = new AsyncCommand(this.ExecuteSubmitAsync, this.CanExecuteSubmit);
        }

        public bool IsBusy
        {
            get => this.isBusy;
            private set => this.SetField(ref this.isBusy, value);
        }

        public IAsyncCommand Submit { get; }

        private async Task ExecuteSubmitAsync()
        {
            try
            {
                this.IsBusy = true;
                var coffeeService = new CoffeeService();
                await coffeeService.PrepareCoffeeAsync();
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        private bool CanExecuteSubmit()
        {
            return !this.IsBusy;
        }
    }
}