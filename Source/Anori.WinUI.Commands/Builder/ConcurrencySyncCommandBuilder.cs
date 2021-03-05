using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Anori.WinUI.Commands.CanExecuteObservers;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Commands.Exceptions;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Commands.Interfaces.Builders;

using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Builder
{
    public sealed class ConcurrencySyncCommandBuilder :
        IConcurrencySyncCommandBuilder,
        IConcurrencySyncCanExecuteBuilder,
        IActivatableConcurrencySyncCommandBuilder,
        IActivatableConcurrencySyncCanExecuteBuilder
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Action<CancellationToken> execute;

        /// <summary>
        /// The observes
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        /// The cancel action
        /// </summary>
        private Action cancelAction;

        /// <summary>
        /// The can execute function
        /// </summary>
        private Func<bool> canExecuteFunction;

        /// <summary>
        /// The can execute expression
        /// </summary>
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        /// The completed action
        /// </summary>
        private Action completedAction;

        /// <summary>
        /// The error action
        /// </summary>
        private Action<Exception> errorAction;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActivate = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ConcurrencySyncCommandBuilder([NotNull] Action<CancellationToken> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCanExecuteBuilder.Build(
            Action<IActivatableConcurrencySyncCommand> setCommand) => BuildActivatable(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.
            ObservesProperty<TType>(Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.
            ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.
            OnCompleted(Action completed) => OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.
            OnCancel(Action cancel) => OnCancel(cancel);

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.OnError(
            Action<Exception> error) => OnError(error);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.AutoActivate() =>
            AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCanExecuteBuilder.Build() =>
            BuildActivatable();

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder IActivatableConcurrencySyncCommandBuilder.
            OnCompleted(Action completed) => OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder IActivatableConcurrencySyncCommandBuilder.OnCancel(Action cancel) =>
            OnCancel(cancel);

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder IActivatableConcurrencySyncCommandBuilder.OnError(
            Action<Exception> error) => OnError(error);

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCommandBuilder.Build(
            Action<IActivatableConcurrencySyncCommand> setCommand) => BuildActivatable(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.
            CanExecute(Func<bool> canExecute) => CanExecute(canExecute);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.CanExecute(
            ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.
            ObservesCanExecute(Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.AutoActivate() =>
            AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCommandBuilder.Build() =>
            BuildActivatable();

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.OnError(Action<Exception> error) =>
            OnError(error);

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.OnCompleted(Action completed) =>
            OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.OnCancel(Action cancel) => OnCancel(cancel);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IConcurrencySyncCommand IConcurrencySyncCanExecuteBuilder.Build(
            Action<IConcurrencySyncCommand> setCommand) =>
            Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.Activatable() => Activatable();

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencySyncCommand IConcurrencySyncCanExecuteBuilder.Build() => Build();

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.OnCompleted(Action completed) =>
            OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.OnCancel(Action cancel) => OnCancel(cancel);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IConcurrencySyncCommand IConcurrencySyncCommandBuilder.Build(
            Action<IConcurrencySyncCommand> setCommand) =>
            Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.Activatable() => Activatable();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.CanExecute(Func<bool> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencySyncCommand IConcurrencySyncCommandBuilder.Build() => Build();

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.OnError(Action<Exception> error) =>
            OnError(error);

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        private ConcurrencySyncCommandBuilder OnError(Action<Exception> error)
        {
            errorAction = error;
            return this;
        }

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        private ConcurrencySyncCommandBuilder OnCompleted(Action completed)
        {
            completedAction = completed;
            return this;
        }

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        private ConcurrencySyncCommandBuilder OnCancel(Action cancel)
        {
            cancelAction = cancel;
            return this;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private ConcurrencySyncCommandBuilder CanExecute([NotNull] Func<bool> canExecute)
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
        [NotNull]
        private ActivatableConcurrencyCanExecuteObserverCommand BuildActivatable()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand(execute, isAutoActivate,
                        canExecuteFunction, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand(execute, isAutoActivate,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableConcurrencyCanExecuteObserverCommand(execute, isAutoActivate, canExecuteFunction,
                    completedAction, errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ActivatableConcurrencyCanExecuteObserverCommand(execute, isAutoActivate,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ActivatableConcurrencyCanExecuteObserverCommand(execute, isAutoActivate, completedAction,
                errorAction, cancelAction);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyCanExecuteObserverCommand Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand(execute,
                        canExecuteFunction, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand(execute,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ConcurrencyCanExecuteObserverCommand(execute, canExecuteFunction, completedAction,
                    errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ConcurrencyCanExecuteObserverCommand(execute,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ConcurrencyCanExecuteObserverCommand(execute, completedAction, errorAction, cancelAction);
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder Activatable()
        {
            isAutoActivate = false;
            return this;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        private ActivatableConcurrencyCanExecuteObserverCommand BuildActivatable(
            [NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand> setCommand)
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
        private ConcurrencyCanExecuteObserverCommand Build(
            [NotNull] Action<ConcurrencyCanExecuteObserverCommand> setCommand)
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
        private ConcurrencySyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private ConcurrencySyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private ConcurrencySyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
            bool fallback)
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
        [NotNull]
        private ConcurrencySyncCommandBuilder ObservesCommandManager()
        {
            if (observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder AutoActivate()
        {
            isAutoActivate = true;
            return this;
        }
    }
}