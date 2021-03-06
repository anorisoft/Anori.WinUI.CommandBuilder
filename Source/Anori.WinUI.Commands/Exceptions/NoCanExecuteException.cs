// -----------------------------------------------------------------------
// <copyright file="NoCanExecuteException.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Anori.WinUI.Commands.Exceptions
{
    /// <summary>
    /// No CanExecute Exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class NoCanExecuteException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoCanExecuteException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NoCanExecuteException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoCanExecuteException"/> class.
        /// </summary>
        public NoCanExecuteException()
        {
        }
    }
}