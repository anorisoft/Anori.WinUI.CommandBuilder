// -----------------------------------------------------------------------
// <copyright file="IParameter.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    /// <summary>
    /// </summary>
    /// <seealso cref="Anori.WinUI.Common.Parameters.IReadOnlyParameter{T}" />
    /// <seealso cref="Anori.WinUI.Common.Parameters.IParameter" />
    public interface IParameter : IReadOnlyParameter
    {
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        new object Value { get; set; }
    }
}