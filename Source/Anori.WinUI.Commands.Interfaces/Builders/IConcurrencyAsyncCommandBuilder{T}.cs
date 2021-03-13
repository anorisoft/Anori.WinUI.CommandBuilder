// -----------------------------------------------------------------------
// <copyright file="IConcurrencyAsyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    /// <summary>
    ///     The I Concurrency Asynchronous Command Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IConcurrencyAsyncCommandBuilder<T>
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Concurrency Async Command.</returns>
        [NotNull]
        IConcurrencyAsyncCommand<T> Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Concurrency Async Command.</returns>
        [NotNull]
        IConcurrencyAsyncCommand<T> Build([NotNull] Action<IConcurrencyAsyncCommand<T>> setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Command Can Execute Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Command vBuilder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Async Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> Activatable();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCommandBuilder<T> OnError([NotNull] Func<Exception, Task> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCommandBuilder<T> OnCompleted([NotNull] Func<Task> completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCommandBuilder<T> OnCancel([NotNull] Func<Task> cancel);
    }
}