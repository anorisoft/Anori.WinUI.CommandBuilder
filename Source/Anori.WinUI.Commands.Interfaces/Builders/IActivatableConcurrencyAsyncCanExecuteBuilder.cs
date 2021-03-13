// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencyAsyncCanExecuteBuilder.cs" company="AnoriSoft">
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
    ///     The Activatable Concurrency Asynchronous Can Execute Builder interface.
    /// </summary>
    public interface IActivatableConcurrencyAsyncCanExecuteBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Async Command.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Concurrency Async Command.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCommand Build([NotNull] Action<IActivatableConcurrencyAsyncCommand> setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder AutoActivate();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnError([NotNull] Func<Exception, Task> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnCompleted([NotNull] Func<Task> completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Activatable Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnCancel([NotNull] Func<Task> cancel);
    }
}