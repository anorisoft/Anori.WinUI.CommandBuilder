// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencyAsyncCommandBuilder.cs" company="AnoriSoft">
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
    public interface IActivatableConcurrencyAsyncCommandBuilder
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand Build([NotNull] Action<IActivatableConcurrencyAsyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder AutoActivate();

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder OnCancel([NotNull] Func<Task> cancel);
    }
}