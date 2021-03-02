using Anori.WinUI.Common;

namespace Anori.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableConcurrencySyncCommand : IConcurrencySyncCommand, IActivatable<IActivatableConcurrencySyncCommand>
    {
        void Execute();
    }
}