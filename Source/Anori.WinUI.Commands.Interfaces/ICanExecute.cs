using System;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface ICanExecute
    {
        /// <summary>
        /// Gets the can execute.
        /// </summary>
        /// <value>
        /// The can execute.
        /// </value>
        Func<bool> CanExecute { get; }
    }
}