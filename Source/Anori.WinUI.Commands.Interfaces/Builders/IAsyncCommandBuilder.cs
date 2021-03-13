// -----------------------------------------------------------------------
// <copyright file="IAsyncCommandBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     Async Command Builder.
    /// </summary>
    public interface IAsyncCommandBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>The Async Command.</returns>
        [NotNull]
        IAsyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>The Async Command.</returns>
        [NotNull]
        IAsyncCommand Build([NotNull] Action<IAsyncCommand> setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>The Async CanExecute Command Builder.</returns>
        [NotNull]
        IAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>The Async CanExecute Command Builder.</returns>
        [NotNull]
        IAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>The Async CanExecute Command Builder.</returns>
        [NotNull]
        IAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>The Async CanExecute Command Builder.</returns>
        [NotNull]
        IAsyncCommandBuilder ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>The Async CanExecute Command Builder.</returns>
        [NotNull]
        IActivatableAsyncCommandBuilder Activatable();
    }
}