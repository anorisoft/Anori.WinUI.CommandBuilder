using Anori.WinUI.Common;

namespace Anori.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableConcurrencyAsyncCommand<in T> : IConcurrencyAsyncCommand<T>, IActivatable<IActivatableConcurrencyAsyncCommand<T>>
    {
    }
}