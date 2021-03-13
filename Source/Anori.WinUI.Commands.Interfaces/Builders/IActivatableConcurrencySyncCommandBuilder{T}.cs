// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencySyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The Activatable Concurrency Synchronize Command Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IActivatableConcurrencySyncCommandBuilder<T>
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
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>Activatable Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

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
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> OnError([NotNull] Action<Exception> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> OnCompleted([NotNull] Action completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> OnCancel([NotNull] Action cancel);
    }
}