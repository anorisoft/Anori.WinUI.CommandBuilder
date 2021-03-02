using System.Threading;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        void Execute([CanBeNull] T parameter, CancellationToken token);

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute([CanBeNull] T parameter);
    }
}