// -----------------------------------------------------------------------
// <copyright file="DelegateObjectHandlers.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using System.Threading.Tasks;

    public class DelegateObjectHandlers
    {
        public object CanExecuteParameter;

        public bool CanExecuteReturnValue = true;

        public object ExecuteParameter;

        public bool CanExecute(object parameter)
        {
            this.CanExecuteParameter = parameter;
            return this.CanExecuteReturnValue;
        }

        public void Execute(object parameter)
        {
            this.ExecuteParameter = parameter;
        }
    }

    public class AsyncDelegateObjectHandlers
    {
        public object CanExecuteParameter;

        public bool CanExecuteReturnValue = true;

        public object ExecuteParameter;

        public bool CanExecute(object parameter)
        {
            this.CanExecuteParameter = parameter;
            return this.CanExecuteReturnValue;
        }

        public async Task Execute(object parameter)
        {
            await Task.Yield();
            this.ExecuteParameter = parameter;
        }
    }
}