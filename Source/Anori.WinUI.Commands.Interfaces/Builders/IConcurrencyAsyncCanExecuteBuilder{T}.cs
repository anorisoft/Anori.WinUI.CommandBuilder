// -----------------------------------------------------------------------
// <copyright file="IConcurrencyAsyncCanExecuteBuilder{T}.cs" company="AnoriSoft">
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
    ///     The I Concurrency Asynchronous Can Execute Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IConcurrencyAsyncCanExecuteBuilder<in T>
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
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Concurrency Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> Activatable();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> OnError([NotNull] Func<Exception, Task> error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> OnCompleted([NotNull] Func<Task> completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Concurrency Async Can Execute Command Builder.</returns>
        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> OnCancel([NotNull] Func<Task> cancel);
    }
}