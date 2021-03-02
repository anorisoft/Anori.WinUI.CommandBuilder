// -----------------------------------------------------------------------
// <copyright file="ICommandFactory.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Anori.WinUI.Commands.Interfaces.Builders;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface ICommandBuilder
    {
        ISyncCommandBuilder Command(Action execute);

        ISyncCommandBuilder<T> Command<T>(Action<T> execute);

        IAsyncCommandBuilder Command(Func<Task> execute);

        IAsyncCommandBuilder<T> Command<T>(Func<T, Task> execute);

        ISyncCanExecuteBuilder Command(Action execute, Func<bool> canExecute);

        ISyncCanExecuteBuilder<T> Command<T>(Action<T> execute, Predicate<T> canExecute);

        IAsyncCanExecuteBuilder Command(Func<Task> execute, Func<bool> canExecute);

        IAsyncCanExecuteBuilder<T> Command<T>(Func<T, Task> execute, Predicate<T> canExecute);

        IConcurrencySyncCommandBuilder Command(Action<CancellationToken> execute);

        IConcurrencySyncCommandBuilder<T> Command<T>(Action<T, CancellationToken> execute);

        IConcurrencyAsyncCommandBuilder Command(Func<CancellationToken, Task> execute);

        IConcurrencyAsyncCommandBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute);

        IConcurrencySyncCanExecuteBuilder Command(Action<CancellationToken> execute, Func<bool> canExecute);

        IConcurrencySyncCanExecuteBuilder<T> Command<T>(Action<T, CancellationToken> execute, Predicate<T> canExecute);

        IConcurrencyAsyncCanExecuteBuilder Command(Func<CancellationToken, Task> execute, Func<bool> canExecute);

        IConcurrencyAsyncCanExecuteBuilder<T> Command<T>(Func<T, CancellationToken, Task> execute, Predicate<T> canExecute);
    }
}