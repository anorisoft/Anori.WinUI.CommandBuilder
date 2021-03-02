namespace Anori.WinUI.Commands.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public interface ISyncCommand : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Determines whether this instance can execute.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance can execute; otherwise, <c>false</c>.
        /// </returns>
        bool CanExecute();

        /// <summary>
        /// Executes this instance.
        /// </summary>
        void Execute();
    }
}