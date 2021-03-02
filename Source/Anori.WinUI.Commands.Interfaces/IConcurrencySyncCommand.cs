using System.Threading;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface IConcurrencySyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        void Execute(CancellationToken token);

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();
        void Execute();
    }
}