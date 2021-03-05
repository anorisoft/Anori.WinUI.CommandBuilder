using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencyAsyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand Build(
            [NotNull] Action<IActivatableConcurrencyAsyncCommand> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder AutoActivate();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder OnCancel([NotNull] Func<Task> cancel);
    }
}