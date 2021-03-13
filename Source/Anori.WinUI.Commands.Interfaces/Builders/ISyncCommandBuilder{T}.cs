// -----------------------------------------------------------------------
// <copyright file="ISyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    using System;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    /// <summary>
    ///     The I Synchronize Command Builder interface.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    public interface ISyncCommandBuilder<T>
    {
        [NotNull]
        ISyncCommand<T> Build();

        [NotNull]
        ISyncCommand<T> Build([NotNull] Action<ISyncCommand<T>> setCommand);

        [NotNull]
        ISyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        ISyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableSyncCommandBuilder<T> Activatable();
    }
}