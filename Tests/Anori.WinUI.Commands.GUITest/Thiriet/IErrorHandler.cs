// -----------------------------------------------------------------------
// <copyright file="IErrorHandler.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
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