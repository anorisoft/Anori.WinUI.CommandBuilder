// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencyAsyncCommandBuilder{T}.cs" company="AnoriSoft">
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
    ///     The I Activatable Concurrency Asynchronous Command Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IActivatableConcurrencyAsyncCommandBuilder<T>
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Async Command.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Concurrency Async Command.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build(
            [NotNull] Action<IActivatableConcurrencyAsyncCommand<T>> setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        ///     Activatable Concurrency Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Activatable Concurrency Async Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> OnError([NotNull] Func<Exception, Task> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Activatable Concurrency Async Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> OnCompleted([NotNull] Func<Task> completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Activatable Concurrency Async Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> OnCancel([NotNull] Func<Task> cancel);
    }
}