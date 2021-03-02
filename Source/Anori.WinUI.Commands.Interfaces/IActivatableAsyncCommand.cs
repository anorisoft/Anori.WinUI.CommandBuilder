using Anori.WinUI.Common;

namespace Anori.WinUI.Commands.Interfaces
{
    public interface IActivatableAsyncCommand : IAsyncCommand, IActivatable<IActivatableAsyncCommand>
    {
    }
}