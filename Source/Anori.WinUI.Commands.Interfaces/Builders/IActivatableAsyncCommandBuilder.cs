// -----------------------------------------------------------------------
// <copyright file="IActivatableAsyncCommandBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     Activatable Async Command Builder.
    /// </summary>
    public interface IActivatableAsyncCommandBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Async Command.</returns>
        [NotNull]
        IActivatableAsyncCommand Build([NotNull] Action<IActivatableAsyncCommand> setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder AutoActivate();
    }
}