using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencyAsyncCanExecuteBuilder<T>
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build(
            [NotNull] Action<IActivatableConcurrencyAsyncCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesProperty<TType>(
            [NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCommandManager();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> OnCancel([NotNull] Func<Task> cancel);
    }
}