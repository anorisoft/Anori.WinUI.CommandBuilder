// -----------------------------------------------------------------------
// <copyright file="IAsyncCanExecuteBuilder.cs" company="AnoriSoft">
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
    public interface IAsyncCanExecuteBuilder
    {
        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     The Async Command.
        /// </returns>
        [NotNull]
        IAsyncCommand Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     The Async Command.
        /// </returns>
        [NotNull]
        IAsyncCommand Build([NotNull] Action<IAsyncCommand> setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Async CanExecute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        ///     Async CanExecute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Async CanExecute Command Builder.
        /// </returns>
        [NotNull]
        IAsyncCanExecuteBuilder ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Async CanExecute Command Builder.
        /// </returns>
        [NotNull]
        IActivatableAsyncCanExecuteBuilder Activatable();
    }
}