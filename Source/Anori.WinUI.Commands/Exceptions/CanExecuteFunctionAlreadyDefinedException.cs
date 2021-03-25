﻿// -----------------------------------------------------------------------
// <copyright file="CanExecuteFunctionAlreadyDefinedException.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using JetBrains.Annotations;

    /// <summary>
    ///     The Can Execute Function Already Defined Exception class.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Exceptions.CommandBuilderException" />
    [Serializable]
    public sealed class CanExecuteFunctionAlreadyDefinedException : CommandBuilderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanExecuteFunctionAlreadyDefinedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        [StringFormatMethod("message")]
        public CanExecuteFunctionAlreadyDefinedException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CanExecuteFunctionAlreadyDefinedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        private CanExecuteFunctionAlreadyDefinedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}