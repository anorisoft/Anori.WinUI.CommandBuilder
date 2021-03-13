// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencySyncCanExecuteBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The Activatable Concurrency Synchronize Can Execute Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IActivatableConcurrencySyncCanExecuteBuilder<in T>
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Sync Command.</returns>
        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Concurrency Sync Command.</returns>
        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build([NotNull] Action<IActivatableConcurrencySyncCommand<T>> setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> AutoActivate();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnError([NotNull] Action<Exception> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnCompleted([NotNull] Action completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnCancel([NotNull] Action cancel);
    }
}