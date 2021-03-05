// -----------------------------------------------------------------------
// <copyright file="ICanExecuteChangedSubjectBase.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    ///     Interface Can Execute Updater
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