// -----------------------------------------------------------------------
// <copyright file="IConcurrencyAsyncCanExecuteBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    public interface IConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        IConcurrencyAsyncCommand Build();

        [NotNull]
        IConcurrencyAsyncCommand Build([NotNull] Action<IConcurrencyAsyncCommand> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Activatable();

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder OnCancel([NotNull] Func<Task> cancel);
    }
}