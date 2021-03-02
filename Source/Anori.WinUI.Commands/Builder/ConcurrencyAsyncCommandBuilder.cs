using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Anori.WinUI.Commands.CanExecuteObservers;
using Anori.WinUI.Commands.Commands;
using Anori.WinUI.Commands.Exceptions;
using Anori.WinUI.Commands.Interfaces;
using Anori.WinUI.Commands.Interfaces.Builders;
using Anori.WinUI.Commands.Interfaces.Commands;
using JetBrains.Annotations;

namespace Anori.WinUI.Commands.Builder
{
    public sealed class ConcurrencyAsyncCommandBuilder :
        IConcurrencyAsyncCommandBuilder,
        IConcurrencyAsyncCanExecuteBuilder,
        IActivatableConcurrencyAsyncCommandBuilder,
        IActivatableConcurrencyAsyncCanExecuteBuilder
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Func<CancellationToken, Task> execute;

        /// <summary>
        /// The observes
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        /// The cancel action
        /// </summary>
        private Func<Task> cancelAction;

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
        private Func<Task> completedAction;

        /// <summary>
        /// The error action
        /// </summary>
        private Func<Exception, Task> errorAction;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActiate;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ConcurrencyAsyncCommandBuilder([NotNull] Func<CancellationToken, Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand IActivatableConcurrencyAsyncCanExecuteBuilder.Build(
            Action<IActivatableConcurrencyAsyncCommand> setCommand) => BuildActivatable(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCanExecuteBuilder.
            ObservesProperty<TType>(Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCanExecuteBuilder.Observes(
            ICanExecuteChangedSubject observer)
        {
            observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCanExecuteBuilder.
            ObservesCommandManager() => ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCanExecuteBuilder.AutoActivate() =>
            AutoActivate();

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCanExecuteBuilder.OnError(
            Func<Exception, Task> error) => OnError(error);

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCanExecuteBuilder.
            OnCompleted(Func<Task> completed) => OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCanExecuteBuilder.
            OnCancel(Func<Task> cancel) => OnCancel(cancel);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand IActivatableConcurrencyAsyncCanExecuteBuilder.Build() =>
            BuildActivatable();

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder IActivatableConcurrencyAsyncCommandBuilder.OnError(
            Func<Exception, Task> error) => OnError(error);

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder IActivatableConcurrencyAsyncCommandBuilder.
            OnCompleted(Func<Task> completed) => OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder IActivatableConcurrencyAsyncCommandBuilder.
            OnCancel(Func<Task> cancel) => OnCancel(cancel);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand IActivatableConcurrencyAsyncCommandBuilder.Build() =>
            BuildActivatable();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand IActivatableConcurrencyAsyncCommandBuilder.Build(
            Action<IActivatableConcurrencyAsyncCommand> setCommand) => BuildActivatable(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCommandBuilder.
            CanExecute(Func<bool> canExecute) => CanExecute(canExecute);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCommandBuilder.CanExecute(
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
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCommandBuilder.
            ObservesCanExecute(Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IActivatableConcurrencyAsyncCommandBuilder.AutoActivate() =>
            AutoActivate();

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCanExecuteBuilder.Observes(
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
        IConcurrencyAsyncCommand IConcurrencyAsyncCanExecuteBuilder.
            Build(Action<IConcurrencyAsyncCommand> setCommand) => Build(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression) => ObservesProperty(expression);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCanExecuteBuilder.ObservesCommandManager() =>
            ObservesCommandManager();

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCanExecuteBuilder.Activatable() => Activatable();

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCanExecuteBuilder.OnError(Func<Exception, Task> error) =>
            OnError(error);

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCanExecuteBuilder.OnCompleted(Func<Task> completed) =>
            OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCanExecuteBuilder.OnCancel(Func<Task> cancel) =>
            OnCancel(cancel);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCommand IConcurrencyAsyncCanExecuteBuilder.Build() => Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommandBuilder IConcurrencyAsyncCommandBuilder.OnError(Func<Exception, Task> error) =>
            OnError(error);

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommandBuilder IConcurrencyAsyncCommandBuilder.OnCompleted(Func<Task> completed) =>
            OnCompleted(completed);

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommandBuilder IConcurrencyAsyncCommandBuilder.OnCancel(Func<Task> cancel) => OnCancel(cancel);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCommand IConcurrencyAsyncCommandBuilder.Build() => Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommand IConcurrencyAsyncCommandBuilder.Build(
            Action<IConcurrencyAsyncCommand> setCommand) =>
            Build(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCommandBuilder.CanExecute(Func<bool> canExecute) =>
            CanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute) => ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder IConcurrencyAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute, bool fallback) => ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder IConcurrencyAsyncCommandBuilder.Activatable() => Activatable();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        public ConcurrencyAsyncCommandBuilder CanExecute([NotNull] Func<bool> canExecute)
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
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private ActivatableConcurrencyAsyncCanExecuteObserverCommand BuildActivatable(
            [NotNull] Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand> setCommand)
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
        [NotNull]
        private ConcurrencyAsyncCanExecuteObserverCommand Build(
            [NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand> setCommand)
        {
            if (setCommand == null) throw new ArgumentNullException(nameof(setCommand));
            var command = Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ActivatableConcurrencyAsyncCanExecuteObserverCommand BuildActivatable()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate,
                        canExecuteFunction, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate,
                    canExecuteFunction, completedAction, errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ActivatableConcurrencyAsyncCanExecuteObserverCommand(execute, isAutoActiate, completedAction,
                errorAction, cancelAction);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCanExecuteObserverCommand Build()
        {
            if (observes.Any())
            {
                if (canExecuteFunction != null)
                {
                    return new ConcurrencyAsyncCanExecuteObserverCommand(execute,
                        canExecuteFunction, completedAction, errorAction, cancelAction, observes.ToArray());
                }

                if (canExecuteSubject != null)
                {
                    return new ConcurrencyAsyncCanExecuteObserverCommand(execute,
                        canExecuteSubject, completedAction, errorAction, cancelAction,
                        observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (canExecuteFunction != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand(execute,
                    canExecuteFunction, completedAction, errorAction, cancelAction);
            }

            if (canExecuteSubject != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand(execute,
                    canExecuteSubject, completedAction, errorAction, cancelAction);
            }

            return new ConcurrencyAsyncCanExecuteObserverCommand(execute, completedAction, errorAction, cancelAction);
        }

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        private ConcurrencyAsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        private ConcurrencyAsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute,
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
        private ConcurrencyAsyncCommandBuilder ObservesCommandManager()
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
        private ConcurrencyAsyncCommandBuilder Activatable() => this;

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder AutoActivate()
        {
            isAutoActiate = true;
            return this;
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        private ConcurrencyAsyncCommandBuilder OnError(Func<Exception, Task> error)
        {
            errorAction = error;
            return this;
        }

        /// <summary>
        /// Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        private ConcurrencyAsyncCommandBuilder OnCompleted(Func<Task> completed)
        {
            completedAction = completed;
            return this;
        }

        /// <summary>
        /// Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        private ConcurrencyAsyncCommandBuilder OnCancel(Func<Task> cancel)
        {
            cancelAction = cancel;
            return this;
        }
    }
}