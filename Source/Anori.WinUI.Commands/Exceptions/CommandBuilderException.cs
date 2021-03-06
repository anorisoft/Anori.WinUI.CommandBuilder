// -----------------------------------------------------------------------
// <copyright file="CommandBuilderException.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Exceptions
{
    using System;

    /// <summary>
    /// Command Builder Exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class CommandBuilderException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBuilderException" /> class.
        /// </summary>
        /// <param name="messgae">The messgae.</param>
        public CommandBuilderException(string messgae)
            : base(messgae)
        {
        }
    }
}