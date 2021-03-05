using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Anori.WinUI.Commands.CanExecuteObservers;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Commands.Exceptions;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Commands.Interfaces.Builders;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Builder
{
    public sealed class SyncCommandBuilder :
        ISyncCommandBuilder,
        ISyncCanExecuteBuilder,
        IActivatableSyncCommandBuilder,
        IActivatableSyncCanExecuteBuilder
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Action execute;

        /// <summary>
        /// The observes
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        /// The can execute function
        /// </summary>
        private Func<bool> canExecuteFunction;

        /// <summary>
        /// The can execute expression
        /// </summary>
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActivate = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public SyncCommandBuilder([NotNull] Action execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableSyncCommand IActivatableSyncCanExecuteBuilder.Build(
            Action<IActivatableSyncCommand> setCommand)
            => BuildActivatable(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression)
            => ObservesProperty(expression);

        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.ObservesCommandManager()
            => ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.AutoActivate()
            => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCommand IActivatableSyncCanExecuteBuilder.Build()
            => BuildActivatable();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableSyncCommand IActivatableSyncCommandBuilder.Build(
            Action<IActivatableSyncCommand> setCommand)
            => BuildActivatable(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.CanExecute(Func<bool> canExecute)
            => CanExecute(canExecute);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute)
            => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback)
            => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.AutoActivate() => AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCommand IActivatableSyncCommandBuilder.Build()
            => BuildActivatable();

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        public ISyncCanExecuteBuilder Observes(ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        ISyncCanExecuteBuilder ISyncCanExecuteBuilder.ObservesCommandManager()
            => ObservesCommandManager();

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder ISyncCanExecuteBuilder.ObservesProperty<TType>(Expression<Func<TType>> expression)
            => ObservesProperty(expression);

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ISyncCommand ISyncCanExecuteBuilder.Build(Action<ISyncCommand> setCommand)
            => Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCanExecuteBuilder ISyncCanExecuteBuilder.Activatable()
            => Activatable();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ISyncCommand ISyncCanExecuteBuilder.Build()
            => Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.CanExecute(Func<bool> canExecute)
            => CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute)
            => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute, bool fallback)
            => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        ISyncCommand ISyncCommandBuilder.Build(Action<ISyncCommand> setCommand)
            => Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableSyncCommandBuilder ISyncCommandBuilder.Activatable()
            => Activatable();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        ISyncCommand ISyncCommandBuilder.Build()
            => Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        [NotNull]
        private SyncCommandBuilder CanExecute([NotNull] Func<bool> canExecute)
        {
            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            if (this.canExecuteSubject != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            this.canExecuteFunction = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ActivatableCanExecuteObserverCommand BuildActivatable()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteSubject,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteFunction);
            }

            if (canExecuteSubject != null)
            {
                return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate, canExecuteSubject);
            }

            return new ActivatableCanExecuteObserverCommand(execute, isAutoActivate);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private CanExecuteObserverCommand Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new CanExecuteObserverCommand(execute, canExecuteFunction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new CanExecuteObserverCommand(execute, canExecuteSubject,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new CanExecuteObserverCommand(execute, canExecuteFunction);
            }

            if (canExecuteSubject != null)
            {
                return new CanExecuteObserverCommand(execute, canExecuteSubject);
            }

            return new CanExecuteObserverCommand(execute);
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        private ActivatableCanExecuteObserverCommand BuildActivatable(
            [NotNull] Action<ActivatableCanExecuteObserverCommand> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = BuildActivatable();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        private CanExecuteObserverCommand Build([NotNull] Action<CanExecuteObserverCommand> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">expression</exception>
        [NotNull]
        private SyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            this.observes.Add(new PropertyObserverFactory().ObservesProperty(expression));
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            if (this.canExecuteSubject != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteSubject = CanExecuteObserver.Create(canExecute);
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
        {
            if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));
            if (this.canExecuteSubject != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteSubject = CanExecuteObserver.Create(canExecute, fallback);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException"></exception>
        [NotNull]
        private SyncCommandBuilder ObservesCommandManager()
        {
            if (observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private SyncCommandBuilder Activatable()
        {
            isAutoActivate = false;
            return this;
        }

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private SyncCommandBuilder AutoActivate()
        {
            isAutoActivate = true;
            return this;
        }
    }
}