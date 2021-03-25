// -----------------------------------------------------------------------
// <copyright file="ObserverIsAlreadyBeingObservedException.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using JetBrains.Annotations;

    /// <summary>
    /// The Observer Is Already Being Observed Exception class.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Exceptions.CommandBuilderException" />
    [Serializable]
    public sealed class ObserverIsAlreadyBeingObservedException : CommandBuilderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObserverIsAlreadyBeingObservedException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="args">The arguments.</param>
        [StringFormatMethod("message")]
        public ObserverIsAlreadyBeingObservedException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObserverIsAlreadyBeingObservedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        private ObserverIsAlreadyBeingObservedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}