// -----------------------------------------------------------------------
// <copyright file="IActivatableConcurrencyAsyncCommand{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Interfaces
{
    using Anori.Common;

    /// <summary>
    ///     Activatable Concurrency Async Command Interface.
    /// </summary>
    /// <typeparam name="T">The parameter type.</typeparam>
    /// <seealso cref="IConcurrencyAsyncCommand{T}" />
    /// <seealso cref="IActivatable" />
    public interface IActivatableConcurrencyAsyncCommand<in T> : IConcurrencyAsyncCommand<T>,
                                                                 IActivatable<IActivatableConcurrencyAsyncCommand<T>>
    {
    }
}