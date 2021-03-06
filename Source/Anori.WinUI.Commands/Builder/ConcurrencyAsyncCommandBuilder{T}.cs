// -----------------------------------------------------------------------
// <copyright file="ConcurrencyAsyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Builder
{
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

    using JetBrains.Annotations;

    public sealed class ConcurrencyAsyncCommandBuilder<T> : IConcurrencyAsyncCommandBuilder<T>,
                                                            IConcurrencyAsyncCanExecuteBuilder<T>,
                                                            IActivatableConcurrencyAsyncCommandBuilder<T>,
                                                            IActivatableConcurrencyAsyncCanExecuteBuilder<T>
    {
        /// <summary>
        ///     The execute
        /// </summary>
        private readonly Func<T, CancellationToken, Task> execute;

        /// <summary>
        ///     The observes
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        ///     The cancel action
        /// </summary>
        private Func<Task> cancelAction;

        /// <summary>
        ///     The can execute function
        /// </summary>
        private Predicate<T> canExecuteFunction;

        /// <summary>
        ///     The can execute expression
        /// </summary>
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        ///     The completed action
        /// </summary>
        private Func<Task> completedAction;

        /// <summary>
        ///     The error action
        /// </summary>
        private Func<Exception, Task> errorAction;

        /// <summary>
        ///     The is automatic actiate
        /// </summary>
        private bool isAutoActiate;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public ConcurrencyAsyncCommandBuilder([NotNull] Func<T, CancellationToken, Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.Build(
            Action<IActivatableConcurrencyAsyncCommand<T>> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.
            ObservesProperty<TType>(Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.
            ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.
            AutoActivate() =>
            this.AutoActivate();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.OnError(
            Func<Exception, Task> error) =>
            this.OnError(error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.OnCompleted(
            Func<Task> completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.OnCancel(
            Func<Task> cancel) =>
            this.OnCancel(cancel);

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.OnError(
            Func<Exception, Task> error) =>
            this.OnError(error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.OnCompleted(
            Func<Task> completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.OnCancel(
            Func<Task> cancel) =>
            this.OnCancel(cancel);

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.
            OnError(Func<Exception, Task> error) =>
            this.OnError(error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.OnCompleted(Func<Task> completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.OnCancel(Func<Task> cancel) =>
            this.OnCancel(cancel);

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand<T> IActivatableConcurrencyAsyncCanExecuteBuilder<T>.Build() =>
            this.BuildActivatable();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand<T> IActivatableConcurrencyAsyncCommandBuilder<T>.Build() =>
            this.BuildActivatable();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommand<T> IActivatableConcurrencyAsyncCommandBuilder<T>.Build(
            Action<IActivatableConcurrencyAsyncCommand<T>> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.CanExecute(
            Predicate<T> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.CanExecute(
            ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.CanExecute(
            ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.
            ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.
            ObservesCanExecute(Expression<Func<bool>> canExecute, bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCanExecuteBuilder<T> IActivatableConcurrencyAsyncCommandBuilder<T>.AutoActivate() =>
            this.AutoActivate();

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCommand<T> IConcurrencyAsyncCanExecuteBuilder<T>.Build() => this.Build();

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCanExecuteBuilder<T>.AutoActivate() =>
            this.AutoActivate();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommand<T> IConcurrencyAsyncCanExecuteBuilder<T>.Build(
            Action<IConcurrencyAsyncCommand<T>> setCommand) =>
            this.Build(setCommand);

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommand<T> IConcurrencyAsyncCommandBuilder<T>.Build(
            Action<IConcurrencyAsyncCommand<T>> setCommand) =>
            this.Build(setCommand);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableConcurrencyAsyncCommandBuilder<T> IConcurrencyAsyncCommandBuilder<T>.Activatable() =>
            this.Activatable();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommandBuilder<T> IConcurrencyAsyncCommandBuilder<T>.OnError(Func<Exception, Task> error) =>
            this.OnError(error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommandBuilder<T> IConcurrencyAsyncCommandBuilder<T>.OnCompleted(Func<Task> completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        IConcurrencyAsyncCommandBuilder<T> IConcurrencyAsyncCommandBuilder<T>.OnCancel(Func<Task> cancel) =>
            this.OnCancel(cancel);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IConcurrencyAsyncCanExecuteBuilder<T> IConcurrencyAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IConcurrencyAsyncCommand<T> IConcurrencyAsyncCommandBuilder<T>.Build() => this.Build();

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        public ConcurrencyAsyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
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
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> BuildActivatable(
            [NotNull] Action<ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand)
        {
            if (setCommand == null)
            {
                throw new ArgumentNullException(nameof(setCommand));
            }

            var command = this.BuildActivatable();
            setCommand(command);
            return command;
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private ConcurrencyAsyncCanExecuteObserverCommand<T> Build(
            [NotNull] Action<ConcurrencyAsyncCanExecuteObserverCommand<T>> setCommand)
        {
            if (setCommand == null)
            {
                throw new ArgumentNullException(nameof(setCommand));
            }

            var command = this.Build();
            setCommand(command);
            return command;
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ActivatableConcurrencyAsyncCanExecuteObserverCommand<T> BuildActivatable()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.isAutoActiate,
                        this.canExecuteFunction,
                        this.completedAction,
                        this.errorAction,
                        this.cancelAction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.isAutoActiate,
                        this.canExecuteSubject,
                        this.completedAction,
                        this.errorAction,
                        this.cancelAction,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(
                    this.execute,
                    this.isAutoActiate,
                    this.canExecuteFunction,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(
                    this.execute,
                    this.isAutoActiate,
                    this.canExecuteSubject,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            return new ActivatableConcurrencyAsyncCanExecuteObserverCommand<T>(
                this.execute,
                this.isAutoActiate,
                this.completedAction,
                this.errorAction,
                this.cancelAction);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ConcurrencyAsyncCanExecuteObserverCommand<T> Build()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ConcurrencyAsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.canExecuteFunction,
                        this.completedAction,
                        this.errorAction,
                        this.cancelAction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ConcurrencyAsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.canExecuteSubject,
                        this.completedAction,
                        this.errorAction,
                        this.cancelAction,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand<T>(
                    this.execute,
                    this.canExecuteFunction,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ConcurrencyAsyncCanExecuteObserverCommand<T>(
                    this.execute,
                    this.canExecuteSubject,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            return new ConcurrencyAsyncCanExecuteObserverCommand<T>(
                this.execute,
                this.completedAction,
                this.errorAction,
                this.cancelAction);
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            this.observes.Add(new PropertyObserverFactory().ObservesProperty(expression));
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
        {
            if (canExecute == null)
            {
                throw new ArgumentNullException(nameof(canExecute));
            }

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
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> ObservesCanExecute(
            [NotNull] Expression<Func<bool>> canExecute,
            bool fallback)
        {
            if (canExecute == null)
            {
                throw new ArgumentNullException(nameof(canExecute));
            }

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
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> ObservesCommandManager()
        {
            if (this.observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> Activatable() => this;

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ConcurrencyAsyncCommandBuilder<T> AutoActivate()
        {
            this.isAutoActiate = true;
            return this;
        }

        private ConcurrencyAsyncCommandBuilder<T> OnError(Func<Exception, Task> error)
        {
            this.errorAction = error;
            return this;
        }

        private ConcurrencyAsyncCommandBuilder<T> OnCompleted(Func<Task> completed)
        {
            this.completedAction = completed;
            return this;
        }

        private ConcurrencyAsyncCommandBuilder<T> OnCancel(Func<Task> cancel)
        {
            this.cancelAction = cancel;
            return this;
        }
    }
}