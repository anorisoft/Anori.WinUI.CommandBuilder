// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCanExecuteBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using JetBrains.Annotations;
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Activatable Async CanExecute Command Builder Interface.
    /// </summary>
    public interface IActivatableAsyncCanExecuteBuilder
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>The Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand Build([NotNull] Action<IActivatableAsyncCommand> setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCommandManager();

        /// <summary>
        /// Automatic activate.
        /// </summary>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder AutoActivate();
    }
}