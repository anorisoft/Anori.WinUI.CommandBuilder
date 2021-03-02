// -----------------------------------------------------------------------
// <copyright file="ErrorHandler.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Commands.Tests
{
    using System;

    public class ErrorHandler : IErrorHandler
    {
        public Exception Exception { get; private set; }

        public bool HasException => this.Exception != null;

        public void HandleError(Exception ex)
        {
            this.Exception = ex;
        }
    }
}