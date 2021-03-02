using System;
using System.Linq.Expressions;
using Anori.WinUI.Commands.Interfaces.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencySyncCanExecuteBuilder<T>
    {
        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build();

        [NotNull]
        IActivatableConcurrencySyncCommand<T> Build(
            [NotNull] Action<IActivatableConcurrencySyncCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> AutoActivate();

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnError([NotNull] Action<Exception> error);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnCompleted([NotNull] Action completed);

        [NotNull]
        IActivatableConcurrencySyncCanExecuteBuilder<T> OnCancel([NotNull] Action cancel);
    }
}