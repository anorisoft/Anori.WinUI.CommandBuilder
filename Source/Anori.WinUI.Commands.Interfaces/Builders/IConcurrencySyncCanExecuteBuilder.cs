using JetBrains.Annotations;
using System;
using System.Linq.Expressions;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        IConcurrencySyncCommand Build();

        [NotNull]
        IConcurrencySyncCommand Build([NotNull] Action<IConcurrencySyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Activatable();

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IConcurrencySyncCanExecuteBuilder ObservesCommandManager();

        [NotNull] IConcurrencySyncCanExecuteBuilder OnError([NotNull] Action<Exception> error);

        [NotNull] IConcurrencySyncCanExecuteBuilder OnCompleted([NotNull] Action completed);

        [NotNull] IConcurrencySyncCanExecuteBuilder OnCancel([NotNull] Action cancel);
    }
}