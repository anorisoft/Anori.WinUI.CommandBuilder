// -----------------------------------------------------------------------
// <copyright file="IErrorHandler.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest.Thiriet
{
    using System;

    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}