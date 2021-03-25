// -----------------------------------------------------------------------
// <copyright file="NoCanExecuteException.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     No CanExecute Exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public sealed class NoCanExecuteException : CommandBuilderException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NoCanExecuteException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoCanExecuteException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoCanExecuteException" /> class.
        /// </summary>
        public NoCanExecuteException()
            : base("No Can Execute")
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NoCanExecuteException" /> class.
        /// </summary>
        /// <param name="info">
        ///     The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object
        ///     data about the exception being thrown.
        /// </param>
        /// <param name="context">
        ///     The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual
        ///     information about the source or destination.
        /// </param>
        private NoCanExecuteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}