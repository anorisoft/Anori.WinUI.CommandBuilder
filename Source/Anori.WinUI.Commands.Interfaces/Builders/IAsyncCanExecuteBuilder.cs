using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IAsyncCanExecuteBuilder
    {
        [NotNull]
        IAsyncCommand Build();

        [NotNull]
        IAsyncCommand Build([NotNull] Action<IAsyncCommand> setCommand);
        
        [NotNull]
        IAsyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> canExecute);

        [NotNull]
        IAsyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);


        [NotNull]
        IAsyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableAsyncCanExecuteBuilder Activatable();
    }
}