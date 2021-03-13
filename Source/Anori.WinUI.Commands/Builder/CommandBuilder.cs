// -----------------------------------------------------------------------
// <copyright file="CommandBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Builder
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Commands.Interfaces.Builders;

    /// <summary>
    /// Command Builder.
    /// Class to create Command based on ICommand.
    /// </summary>
    /// <seealso cref="ICommandBuilder" />
    public sealed class CommandBuilder : ICommandBuilder
    {
        /// <summary>
        ///     Gets the builder.
        /// </summary>
        /// <value>
        ///     The builder.
        /// </value>
        public static ICommandBuilder Builder { get; } = new CommandBuilder();

        /// <summary>
        /// Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public ISyncCommandBuilder Command(Action execute) => new SyncCommandBuilder(execute);

        /// <summary>
        /// Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public ISyncCanExecuteBuilder Command(Action execute, Func<bool> canExecute) =>
            ((ISyncCommandBuilder)new SyncCommandBuilder(execute)).CanExecute(canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public ISyncCommandBuilder<T> Command<T>(Action<T> execute) => new SyncCommandBuilder<T>(execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Predicate<T> canExecute) =>
            ((ISyncCommandBuilder<T>)new SyncCommandBuilder<T>(execute)).CanExecute(canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public IAsyncCommandBuilder Command(Func<Task> execute) => new AsyncCommandBuilder(execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute) =>
            ((IAsyncCommandBuilder)new AsyncCommandBuilder(execute)).CanExecute(canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public IAsyncCommandBuilder<T> Command<T>(Func<T, Task> execute) => new AsyncCommandBuilder<T>(execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Predicate<T> canExecute) =>
            ((IAsyncCommandBuilder<T>)new AsyncCommandBuilder<T>(execute)).CanExecute(canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public IConcurrencySyncCommandBuilder Command(Action<CancellationToken> execute) =>
            new ConcurrencySyncCommandBuilder(execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public IConcurrencySyncCommandBuilder<T> Command<T>(Action<T, CancellationToken> execute) =>
            new ConcurrencySyncCommandBuilder<T>(execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public IConcurrencyAsyncCommandBuilder Command(Func<CancellationToken, Task> execute) =>
            new ConcurrencyAsyncCommandBuilder(execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <returns></returns>
        public IConcurrencyAsyncCommandBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute) =>
            new ConcurrencyAsyncCommandBuilder<T>(execute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public IConcurrencySyncCanExecuteBuilder Command(Action<CancellationToken> execute, Func<bool> canExecute) =>
            ((IConcurrencySyncCommandBuilder)new ConcurrencySyncCommandBuilder(execute)).CanExecute(canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public IConcurrencySyncCanExecuteBuilder<T> Command<T>(
            Action<T, CancellationToken> execute,
            Predicate<T> canExecute) =>
            ((IConcurrencySyncCommandBuilder<T>)new ConcurrencySyncCommandBuilder<T>(execute)).CanExecute(canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public IConcurrencyAsyncCanExecuteBuilder
            Command(Func<CancellationToken, Task> execute, Func<bool> canExecute) =>
            new ConcurrencyAsyncCommandBuilder(execute).CanExecute(canExecute);

        /// <summary>
        ///     Commands the specified execute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        public IConcurrencyAsyncCanExecuteBuilder<T> Command<T>(
            Func<T, CancellationToken, Task> execute,
            Predicate<T> canExecute) =>
            new ConcurrencyAsyncCommandBuilder<T>(execute).CanExecute(canExecute);
    }
}