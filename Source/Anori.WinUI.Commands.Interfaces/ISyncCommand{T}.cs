// -----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface ISyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Determines whether this instance can execute the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        ///   <c>true</c> if this instance can execute the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute([CanBeNull] T parameter);

        /// <summary>
        /// Executes the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        void Execute([CanBeNull] T parameter);
    }
}