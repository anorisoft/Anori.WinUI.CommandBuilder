// -----------------------------------------------------------------------
// <copyright file="CommandManagerObserver.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    using JetBrains.Annotations;

    public class CommandManagerObserver : IPropertyObserver
    {
        /// <summary>
        ///     The dictionary
        /// </summary>
        [NotNull]
        private readonly IDictionary<IPropertyObservable, EventHandler> dictionary =
            new Dictionary<IPropertyObservable, EventHandler>();

        /// <summary>
        ///     Adds the specified observable.
        /// </summary>
        /// <param name="observable">The observable.</param>
        public void Add([NotNull] IPropertyObservable observable)
        {
            if (observable == null)
            {
                throw new ArgumentNullException(nameof(observable));
            }

            if (this.dictionary.TryGetValue(observable, out _))
            {
                return;
            }

            void Handler(object sender, EventArgs args) => observable.RaisePropertyChanged();
            this.dictionary.Add(observable, Handler);
            CommandManager.RequerySuggested += Handler;
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Removes the specified observable.
        /// </summary>
        /// <param name="observable">The observable.</param>
        public void Remove([NotNull] IPropertyObservable observable)
        {
            if (observable == null)
            {
                throw new ArgumentNullException(nameof(observable));
            }

            if (!this.dictionary.TryGetValue(observable, out var handler))
            {
                return;
            }

            this.dictionary.Remove(observable);
            CommandManager.RequerySuggested -= handler;
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        ///     unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            foreach (var handler in this.dictionary.Values)
            {
                CommandManager.RequerySuggested -= handler;
            }

            this.dictionary.Clear();
        }
    }
}