using Anori.WinUI.Common;

namespace Anori.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableConcurrencySyncCommand<in T> : IConcurrencySyncCommand<T>, IActivatable<IActivatableConcurrencySyncCommand<T>>
    {
    }
}