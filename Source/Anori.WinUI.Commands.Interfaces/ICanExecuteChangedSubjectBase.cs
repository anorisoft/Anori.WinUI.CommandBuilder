// -----------------------------------------------------------------------
// <copyright file="ICanExecuteChangedSubjectBase.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using System;

    /// <summary>
    ///     Interface Can Execute Updater.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface ICanExecuteChangedSubjectBase : IDisposable
    {
        /// <summary>
        ///     Adds the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void Add(ICanExecuteChangedObserver observer);

        /// <summary>
        ///     Removes the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void Remove(ICanExecuteChangedObserver observer);
    }
}