using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Interfaces.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableConcurrencySyncCommand Build();

        [NotNull]
        IActivatableConcurrencySyncCommand Build(
            [NotNull] Action<IActivatableConcurrencySyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder AutoActivate();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder OnError([NotNull] Action<Exception> error);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder OnCompleted([NotNull] Action completed);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder OnCancel([NotNull] Action cancel);
    }
}