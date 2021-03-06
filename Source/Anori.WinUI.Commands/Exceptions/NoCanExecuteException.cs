// -----------------------------------------------------------------------
// <copyright file="NoCanExecuteException.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Exceptions
{
    using System;

    /// <summary>
    ///     No CanExecute Exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class NoCanExecuteException : Exception
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
        {
        }
    }
}