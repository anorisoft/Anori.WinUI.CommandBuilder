using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IAsyncCommandBuilder
    {
        [NotNull]
        IAsyncCommand Build();

        [NotNull]
        IAsyncCommand Build([NotNull] Action<IAsyncCommand> setCommand);

        [NotNull]
        IAsyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IAsyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IAsyncCommandBuilder ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCommandBuilder Activatable();
    }
}