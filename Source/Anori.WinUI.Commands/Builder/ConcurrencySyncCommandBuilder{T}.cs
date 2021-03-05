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
    public sealed class ConcurrencySyncCommandBuilder<T> :
        IConcurrencySyncCommandBuilder<T>,
        IConcurrencySyncCanExecuteBuilder<T>,
        IActivatableConcurrencySyncCommandBuilder<T>,
        IActivatableConcurrencySyncCanExecuteBuilder<T>
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Action<T, CancellationToken> execute;

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
        private Predicate<T> canExecuteFunction;

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
        /// Initializes a new instance of the <see cref="SyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ConcurrencySyncCommandBuilder([NotNull] Action<T, CancellationToken> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.Build(
            Action<IActivatableConcurrencySyncCommand<T>> setCommand) => BuildActivatable(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.
            ObservesProperty<TType>(Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.
            ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.
            AutoActivate() => AutoActivate();

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.OnError(
            Action<Exception> error) =>
            OnError(error);

        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.OnCompleted(
            Action completed)
        {
            return OnCompleted(completed);
        }

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.OnCancel(
            Action cancel) =>
            OnCancel(cancel);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand<T> IActivatableConcurrencySyncCanExecuteBuilder<T>.Build() =>
            BuildActivatable();

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.OnError(
            Action<Exception> error) =>
            OnError(error);

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.OnCompleted(
            Action completed) =>
            OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.OnCancel(
            Action cancel) =>
            OnCancel(cancel);

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand<T> IActivatableConcurrencySyncCommandBuilder<T>.Build(
            Action<IActivatableConcurrencySyncCommand<T>> setCommand) => BuildActivatable(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.
            CanExecute(Predicate<T> canExecute) => CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.
            ObservesCanExecute(Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.AutoActivate() =>
            AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCommand<T> IActivatableConcurrencySyncCommandBuilder<T>.Build() =>
            BuildActivatable();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IActivatableConcurrencySyncCommandBuilder<T>.
            CanExecute(ICanExecuteSubject canExecute) => ActivatableCanExecute(canExecute);

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.OnCompleted(Action completed) =>
            OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.OnCancel(Action cancel) =>
            OnCancel(cancel);

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.OnError(Action<Exception> error) =>
            OnError(error);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IConcurrencySyncCommand<T> IConcurrencySyncCanExecuteBuilder<T>.Build(
            Action<IConcurrencySyncCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.Activatable() =>
            Activatable();

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencySyncCommand<T> IConcurrencySyncCanExecuteBuilder<T>.Build() => Build();

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencySyncCommandBuilder<T> IConcurrencySyncCommandBuilder<T>.OnCompleted(Action completed) =>
            OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencySyncCommandBuilder<T> IConcurrencySyncCommandBuilder<T>.OnCancel(Action cancel) =>
            OnCancel(cancel);

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencySyncCommandBuilder<T> IConcurrencySyncCommandBuilder<T>.OnError(Action<Exception> error) =>
            OnError(error);

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IConcurrencySyncCommand<T> IConcurrencySyncCommandBuilder<T>.Build(
            Action<IConcurrencySyncCommand<T>> setCommand) => Build(setCommand);

        /// <summary>
        /// Activateables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencySyncCommandBuilder<T> IConcurrencySyncCommandBuilder<T>.Activatable() =>
            Activatable();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencySyncCommand<T> IConcurrencySyncCommandBuilder<T>.Build() => Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencySyncCanExecuteBuilder<T> IConcurrencySyncCommandBuilder<T>.
            CanExecute(ICanExecuteSubject canExecute) => CanExecute(canExecute);

        private IActivatableConcurrencySyncCanExecuteBuilder<T> ActivatableCanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        private IConcurrencySyncCanExecuteBuilder<T> CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        private ConcurrencySyncCommandBuilder<T> OnError(Action<Exception> error)
        {
            errorAction = error;
            return this;
        }

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        private ConcurrencySyncCommandBuilder<T> OnCompleted(Action completed)
        {
            completedAction = completed;
            return this;
        }

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        private ConcurrencySyncCommandBuilder<T> OnCancel(Action cancel)
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
        private ConcurrencySyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
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
        private ActivatableConcurrencyCanExecuteObserverCommand<T> BuildActivatable()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                        canExecuteFunction, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                    canExecuteFunction, completedAction, errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ActivatableConcurrencyCanExecuteObserverCommand<T>(execute, isAutoActivate, completedAction,
                errorAction, cancelAction);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ConcurrencyCanExecuteObserverCommand<T> Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                        canExecuteFunction, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                    canExecuteFunction, completedAction, errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ConcurrencyCanExecuteObserverCommand<T>(execute,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ConcurrencyCanExecuteObserverCommand<T>(execute, completedAction, errorAction, cancelAction);
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        private ConcurrencySyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        private ConcurrencySyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
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
        private ConcurrencySyncCommandBuilder<T> ObservesCommandManager()
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
        private ConcurrencySyncCommandBuilder<T> AutoActivate()
        {
            isAutoActivate = true;
            return this;
        }

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder<T> Activatable()
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
        private ActivatableConcurrencyCanExecuteObserverCommand<T> BuildActivatable(
            [NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand<T>> setCommand)
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
        private ConcurrencyCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ConcurrencyCanExecuteObserverCommand<T>> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = Build();
            setCommand(command);
            return command;
        }
    }
}