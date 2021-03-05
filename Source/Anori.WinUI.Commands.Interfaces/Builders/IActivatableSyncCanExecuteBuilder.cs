using System;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    public interface IActivatableSyncCanExecuteBuilder
    {
        [NotNull]
        IActivatableSyncCommand Build();

        [NotNull]
        IActivatableSyncCommand Build([NotNull] Action<IActivatableSyncCommand> setCommand);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression);

        [NotNull]
        IActivatableSyncCanExecuteBuilder Observes([NotNull] ICanExecuteChangedSubject observer);

        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCommandManager();

        [NotNull]
        IActivatableSyncCanExecuteBuilder AutoActivate();
    }
}