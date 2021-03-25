// -----------------------------------------------------------------------
// <copyright file="AsyncCommand.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest.Thiriet
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        private readonly Func<T, bool> _canExecute;

        private readonly IErrorHandler _errorHandler;

        private readonly Func<T, Task> _execute;

        private bool _isExecuting;

        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
            this._errorHandler = errorHandler;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(T parameter)
        {
            return !this._isExecuting && (this._canExecute?.Invoke(parameter) ?? true);
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (this.CanExecute(parameter))
            {
                try
                {
                    this._isExecuting = true;
                    await this._execute(parameter);
                }
                finally
                {
                    this._isExecuting = false;
                }
            }

            this.RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.ExecuteAsync((T)parameter).FireAndForgetSafeAsync(this._errorHandler);
        }

        #endregion Explicit implementations
    }

    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<bool> _canExecute;

        private readonly IErrorHandler _errorHandler;

        private readonly Func<Task> _execute;

        private bool _isExecuting;

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null, IErrorHandler errorHandler = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
            this._errorHandler = errorHandler;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute()
        {
            return !this._isExecuting && (this._canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (this.CanExecute())
            {
                try
                {
                    this._isExecuting = true;
                    await this._execute();
                }
                finally
                {
                    this._isExecuting = false;
                }
            }

            this.RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations

        bool ICommand.CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            this.ExecuteAsync().FireAndForgetSafeAsync(this._errorHandler);
        }

        #endregion Explicit implementations
    }
}