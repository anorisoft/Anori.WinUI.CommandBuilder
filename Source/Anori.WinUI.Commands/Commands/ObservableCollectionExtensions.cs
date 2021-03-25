// -----------------------------------------------------------------------
// <copyright file="ObservableCollectionExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Commands
{
    using System;
    using System.Collections.Generic;

    using Anori.WinUI.Commands.Exceptions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Commands.Resources;

    /// <summary>
    ///     Observable Collection Extensions.
    /// </summary>
    internal static class ObservableCollectionExtensions
    {
        /// <summary>
        ///     Adds if not contains.
        /// </summary>
        /// <param name="observers">The observers.</param>
        /// <param name="newItems">The new items.</param>
        /// <exception cref="ArgumentNullException">observers - Observable item.</exception>
        /// <exception cref="ArgumentException">propertyObserver is null.</exception>
        public static void AddIfNotContains(
            this IList<ICanExecuteChangedSubjectBase> observers,
            IEnumerable<ICanExecuteChangedSubject> newItems)
        {
            foreach (var propertyObserver in newItems)
            {
                if (propertyObserver == null)
                {
                    throw new ArgumentNullException(nameof(observers), "Observable item.");
                }

                if (observers.Contains(propertyObserver))
                {
                    throw new ObserverIsAlreadyBeingObservedException(
                        ExceptionStrings.ObserverIsAlreadyBeingObserved,
                        propertyObserver);
                }

                observers.Add(propertyObserver);
            }
        }
    }
}