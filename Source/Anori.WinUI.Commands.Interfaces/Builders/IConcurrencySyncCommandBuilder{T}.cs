// -----------------------------------------------------------------------
// <copyright file="IConcurrencySyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The I Concurrency Synchronize Command Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IConcurrencySyncCommandBuilder<T>
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Concurrency Sync Command.</returns>
        [NotNull]
        IConcurrencySyncCommand<T> Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Concurrency Sync Command.</returns>
        [NotNull]
        IConcurrencySyncCommand<T> Build([NotNull] Action<IConcurrencySyncCommand<T>> setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>Concurrency Sync Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencySyncCanExecuteBuilder<T> ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencySyncCommandBuilder<T> Activatable();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        [NotNull]
        IConcurrencySyncCommandBuilder<T> OnError([NotNull] Action<Exception> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        [NotNull]
        IConcurrencySyncCommandBuilder<T> OnCompleted([NotNull] Action completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        [NotNull]
        IConcurrencySyncCommandBuilder<T> OnCancel([NotNull] Action cancel);
    }
}