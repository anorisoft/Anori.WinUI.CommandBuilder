using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface IConcurrencyAsyncCommand<in T> : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        Task ExecuteAsync([CanBeNull] T parameter, CancellationToken token);

        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute([CanBeNull] T parameter);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is executing.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is executing; otherwise, <c>false</c>.
        /// </value>
        bool IsExecuting { get; }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        /// <value>
        /// The cancel command.
        /// </value>
        ISyncCommand CancelCommand { get;  }
    }
}