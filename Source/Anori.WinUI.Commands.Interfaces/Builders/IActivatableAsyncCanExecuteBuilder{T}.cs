// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCanExecuteBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    /// Activatable Async CanExecute Command Builder Interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IActivatableAsyncCanExecuteBuilder<in T>
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand<T> Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>The Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand<T> Build([NotNull] Action<IActivatableAsyncCommand<T>> setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> AutoActivate();
    }
}