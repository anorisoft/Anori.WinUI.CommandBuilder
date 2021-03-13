// -----------------------------------------------------------------------
// <copyright file="ISyncCanExecuteBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The Synchronize Can Execute Builder interface.
    /// </summary>
    public interface ISyncCanExecuteBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Sync Command Builder.
        /// </returns>
        [NotNull]
        ISyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     Sync Command Builder.
        /// </returns>
        [NotNull]
        ISyncCommand Build([NotNull] Action<ISyncCommand> setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        ISyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        ISyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        ISyncCanExecuteBuilder ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder Activatable();
    }
}