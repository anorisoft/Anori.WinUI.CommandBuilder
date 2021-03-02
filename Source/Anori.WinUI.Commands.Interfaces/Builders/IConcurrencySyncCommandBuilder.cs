using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCommandBuilder
    {
        [NotNull]
        IConcurrencySyncCommand Build();

        [NotNull]
        IConcurrencySyncCommand Build([NotNull] Action<IConcurrencySyncCommand> setCommand);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder
            ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        [NotNull]
        IActivatableConcurrencySyncCommandBuilder Activatable();

        [NotNull]
        IConcurrencySyncCommandBuilder OnError([NotNull] Action<Exception> error);

        [NotNull]
        IConcurrencySyncCommandBuilder OnCompleted([NotNull] Action completed);

        [NotNull]
        IConcurrencySyncCommandBuilder OnCancel([NotNull] Action cancel);
    }
}