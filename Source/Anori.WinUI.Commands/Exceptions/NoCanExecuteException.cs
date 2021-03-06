// -----------------------------------------------------------------------
// <copyright file="NoCanExecuteException.cs" company="Anorisoft">
// Copyright (c) bfa solutions ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Anori.WinUI.Commands.Exceptions
{
    [Serializable]
    public class NoCanExecuteException : Exception
    {
        public NoCanExecuteException(string message)
            : base(message)
        {
        }

        public NoCanExecuteException()
        {
        }
    }
}