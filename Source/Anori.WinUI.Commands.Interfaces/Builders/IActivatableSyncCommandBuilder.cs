using System;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Interfaces.Builders
{
    /// <summary>
    /// The Activatable Synchronize Command Builder interface.
    /// </summary>
    public interface IActivatableSyncCommandBuilder
    {
        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>Activatable Sync Command.</returns>
        [NotNull]
        IActivatableSyncCommand Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Sync Command.</returns>
        [NotNull]
        IActivatableSyncCommand Build([NotNull] Action<IActivatableSyncCommand> setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder CanExecute([NotNull] Func<bool> canExecute);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder CanExecute([NotNull] ICanExecuteSubject canExecute);


        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns>Activatable Sync Can Execute Command Builder.</returns>
        [NotNull]
        IActivatableSyncCanExecuteBuilder AutoActivate();
    }
}