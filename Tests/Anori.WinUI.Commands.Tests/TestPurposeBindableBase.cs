// -----------------------------------------------------------------------
// <copyright file="TestPurposeBindableBase.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    ///     Provides minimum functionality TestViewModel based class in order to expose
    ///     GetPropertyChangedSubscribedLength to test if PropertyObserve's
    ///     unsubscribing to PropertyChanged is working properly.
    /// </summary>
    public abstract class TestPurposeBindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int GetPropertyChangedSubscribedLength()
        {
            return this.PropertyChanged?.GetInvocationList()?.Length ?? 0;
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, args);
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.RaisePropertyChanged(propertyName);

            return true;
        }
    }
}