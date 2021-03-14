// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencySyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.Common;
    using Anori.WinUI.Common;

    /// <summary>
    ///     Activatable Concurrency Sync Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="IConcurrencySyncCommand{T}" />
    /// <seealso cref="IActivatable" />
    public interface IActivatableConcurrencySyncCommand<in T> : IConcurrencySyncCommand<T>,
                                                                IActivatable<IActivatableConcurrencySyncCommand<T>>
    {
    }
}