// -----------------------------------------------------------------------
// <copyright file="IErrorHandler.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anorisoft.WinUI.Common
{
    using System;

    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}