// -----------------------------------------------------------------------
// <copyright file="CommandBuilderException.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     Command Builder Exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public abstract class CommandBuilderException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandBuilderException" /> class.
        /// </summary>
        /// <param name="messgae">The messgae.</param>
        protected CommandBuilderException(string messgae)
            : base(messgae)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBuilderException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected CommandBuilderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}