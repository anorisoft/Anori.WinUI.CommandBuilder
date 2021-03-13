// -----------------------------------------------------------------------
// <copyright file="ICommandBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    ///     The Command Builder interface.
    /// </summary>
    public interface ICommandBuilder
    {
        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns>Sync Command Builder.</returns>
        ISyncCommandBuilder Command(Action execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns>Sync Command Builder.</returns>
        ISyncCommandBuilder<T> Command<T>(Action<T> execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns>Async Command Builder.</returns>
        IAsyncCommandBuilder Command(Func<Task> execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns>Async Command Builder.</returns>
        IAsyncCommandBuilder<T> Command<T>(Func<T, Task> execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Sync Can Excecute Command Builder.</returns>
        ISyncCanExecuteBuilder Command(Action execute, Func<bool> canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Sync Can Excecute Command Builder.</returns>
        ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Predicate<T> canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Async Can Excecute Command Builder.</returns>
        IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Async Can Excecute Command Builder.</returns>
        IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Predicate<T> canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        IConcurrencySyncCommandBuilder Command(Action<CancellationToken> execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        IConcurrencySyncCommandBuilder<T> Command<T>(Action<T, CancellationToken> execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        IConcurrencyAsyncCommandBuilder Command(Func<CancellationToken, Task> execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns>Concurrency Async Command Builder.</returns>
        IConcurrencyAsyncCommandBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync Can Execute Command Builder.</returns>
        IConcurrencySyncCanExecuteBuilder Command(Action<CancellationToken> execute, Func<bool> canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync Can Execute Command Builder.</returns>
        IConcurrencySyncCanExecuteBuilder<T> Command<T>(Action<T, CancellationToken> execute, Predicate<T> canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        IConcurrencyAsyncCanExecuteBuilder Command(Func<CancellationToken, Task> execute, Func<bool> canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        IConcurrencyAsyncCanExecuteBuilder<T> Command<T>(
            Func<T, CancellationToken, Task> execute,
            Predicate<T> canExecute);
    }
}