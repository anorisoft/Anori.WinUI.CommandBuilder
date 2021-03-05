using System;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableSyncCommandBuilder<T>
    {
        [NotNull]
        IActivatableSyncCommand<T> Build();

        [NotNull]
        IActivatableSyncCommand<T> Build([NotNull] Action<IActivatableSyncCommand<T>> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);


        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableSyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);


        [NotNull]
        IActivatableSyncCommandBuilder<T> AutoActivate();
    }
}