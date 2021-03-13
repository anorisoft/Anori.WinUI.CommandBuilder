// -----------------------------------------------------------------------
// <copyright file="IAsyncCanExecuteBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The Asynchronous Can Execute Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IAsyncCanExecuteBuilder<T>
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Async Command.</returns>
        [NotNull]
        IAsyncCommand<T> Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Async Command.</returns>
        [NotNull]
        IAsyncCommand<T> Build([NotNull] Action<IAsyncCommand<T>> setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        ///     Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Activatable Async Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> Activatable();
    }
}