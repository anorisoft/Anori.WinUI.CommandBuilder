using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IConcurrencyAsyncCommandBuilder<T>
    {
        [NotNull]
        IConcurrencyAsyncCommand<T> Build();

        [NotNull]
        IConcurrencyAsyncCommand<T> Build(
            [NotNull] Action<IConcurrencyAsyncCommand<T>> setCommand);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] Predicate<T> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> CanExecute([NotNull] ICanExecuteSubject canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        [NotNull]
        IConcurrencyAsyncCanExecuteBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback);

        [NotNull]
        IActivatableConcurrencyAsyncCommandBuilder<T> Activatable();

        [NotNull]
        IConcurrencyAsyncCommandBuilder<T> OnError([NotNull] Func<Exception, Task> error);

        [NotNull]
        IConcurrencyAsyncCommandBuilder<T> OnCompleted([NotNull] Func<Task> completed);

        [NotNull]
        IConcurrencyAsyncCommandBuilder<T> OnCancel([NotNull] Func<Task> cancel);
    }
}