// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    /// Activatable Async Command Builder Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    public interface IActivatableAsyncCommandBuilder<T>
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand<T> Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand<T> Build([NotNull] Action<IActivatableAsyncCommand<T>> setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Async CanExecute Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Async CanExecute Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Result of ObservesCanExecute as IActivatableAsyncCanExecuteBuilder&lt;T&gt;.
        /// </returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        /// Result of ObservesCanExecute as IActivatableAsyncCanExecuteBuilder&lt;T&gt;.
        /// </returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns>
        /// Result of AutoActivate as IActivatableAsyncCanExecuteBuilder&lt;T&gt;.
        /// </returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder<T> AutoActivate();
    }
}