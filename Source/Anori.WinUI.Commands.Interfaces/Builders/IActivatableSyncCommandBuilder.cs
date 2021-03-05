using System;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableSyncCommandBuilder
    {
        [NotNull]
        IActivatableSyncCommand Build();

        [NotNull]
        IActivatableSyncCommand Build([NotNull] Action<IActivatableSyncCommand> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableSyncCanExecuteBuilder AutoActivate();
    }
}