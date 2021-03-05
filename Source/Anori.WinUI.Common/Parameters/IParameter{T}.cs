// -----------------------------------------------------------------------
// <copyright file="IParameter{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Anori.WinUI.Common.Parameters.IReadOnlyParameter{T}" />
    /// <seealso cref="Anori.WinUI.Common.Parameters.IParameter" />
    public interface IParameter<T> : IReadOnlyParameter<T>, IParameter
    {
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        new T Value { get; set; }
    }
}