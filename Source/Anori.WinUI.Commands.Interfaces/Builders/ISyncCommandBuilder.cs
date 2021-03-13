// -----------------------------------------------------------------------
// <copyright file="ISyncCommandBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The Synchronize Command Builder interface.
    /// </summary>
    public interface ISyncCommandBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Sync Command.
        /// </returns>
        [NotNull]
        ISyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     Sync Command.
        /// </returns>
        [NotNull]
        ISyncCommand Build([NotNull] Action<ISyncCommand> setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        ISyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        ISyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        ISyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IActivatableSyncCommandBuilder Activatable();
    }
}