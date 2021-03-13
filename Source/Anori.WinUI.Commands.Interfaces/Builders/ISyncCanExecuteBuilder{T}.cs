// -----------------------------------------------------------------------
// <copyright file="ISyncCanExecuteBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The I Synchronize Can Execute Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ISyncCanExecuteBuilder<in T>
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Sync Command.</returns>
        [NotNull]
        ISyncCommand<T> Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Sync Command.</returns>
        [NotNull]
        ISyncCommand<T> Build([NotNull] Action<ISyncCommand<T>> setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Sync Can Execute Command Builder.</returns>
        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>Sync Can Execute Command Builder.</returns>
        [NotNull]
        ISyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>Sync Can Execute Command Builder.</returns>
        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> Activatable();
    }
}