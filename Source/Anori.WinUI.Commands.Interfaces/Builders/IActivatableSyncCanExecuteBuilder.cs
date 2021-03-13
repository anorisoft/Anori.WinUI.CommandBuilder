// -----------------------------------------------------------------------
// <copyright file="IActivatableSyncCanExecuteBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The Activatable Synchronize Can Execute Builder interface.
    /// </summary>
    public interface IActivatableSyncCanExecuteBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Activatable Sync Command.</returns>
        [NotNull]
        IActivatableSyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Sync Command.</returns>
        [NotNull]
        IActivatableSyncCommand Build([NotNull] Action<IActivatableSyncCommand> setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder AutoActivate();
    }
}