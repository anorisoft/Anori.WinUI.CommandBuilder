using Anori.WinUI.Common;

namespace Anori.WinUI.Commands.Interfaces.Commands
{
    public interface IActivatableAsyncCommand<in T> : IAsyncCommand<T>, IActivatable<IActivatableAsyncCommand<T>>
    {
    }
}