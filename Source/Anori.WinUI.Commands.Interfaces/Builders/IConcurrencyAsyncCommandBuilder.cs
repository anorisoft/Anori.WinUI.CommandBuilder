// -----------------------------------------------------------------------
// <copyright file="IConcurrencyAsyncCommandBuilder.cs" company="AnoriSoft">
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
    ///     The Concurrency Asynchronous Command Builder interface.
    /// </summary>
    public interface IConcurrencyAsyncCommandBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Concurrency Async Command.</returns>
        [NotNull]
        IConcurrencyAsyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Concurrency Async Command.</returns>
        [NotNull]
        IConcurrencyAsyncCommand Build([NotNull] Action<IConcurrencyAsyncCommand> setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder Activatable();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCommandBuilder OnError([NotNull] Func<Exception, Task> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCommandBuilder OnCompleted([NotNull] Func<Task> completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCommandBuilder OnCancel([NotNull] Func<Task> cancel);
    }
}