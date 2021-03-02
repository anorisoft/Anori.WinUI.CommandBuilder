using Anori.WinUI.Common;

namespace Anori.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableConcurrencyAsyncCommand : IConcurrencyAsyncCommand, IActivatable<IActivatableConcurrencyAsyncCommand>
    {
        void Execute();
    }
}