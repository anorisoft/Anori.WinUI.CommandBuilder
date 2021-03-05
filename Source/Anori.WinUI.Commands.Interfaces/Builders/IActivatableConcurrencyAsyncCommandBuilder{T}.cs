using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableConcurrencyAsyncCommandBuilder<T>
    {
        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build();

        [NotNull]
        IActivatableConcurrencyAsyncCommand<T> Build(
            [NotNull] Action<IActivatableConcurrencyAsyncCommand<T>> setCommand);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T>
            ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> AutoActivate();

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> OnCancel([NotNull] Func<Task> cancel);
    }
}