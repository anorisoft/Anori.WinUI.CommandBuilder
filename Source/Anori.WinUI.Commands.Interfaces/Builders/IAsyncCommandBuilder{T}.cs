// -----------------------------------------------------------------------
// <copyright file="IAsyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    /// The I Asynchronous Command Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface IAsyncCommandBuilder<T>
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
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCommandBuilder<T> ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Async Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IActivatableAsyncCommandBuilder<T> Activatable();
    }
}