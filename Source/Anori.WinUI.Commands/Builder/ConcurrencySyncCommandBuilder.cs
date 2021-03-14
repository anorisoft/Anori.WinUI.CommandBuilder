// -----------------------------------------------------------------------
// <copyright file="ConcurrencySyncCommandBuilder.cs" company="AnoriSoft">
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

    using Anori.WinUI.Commands.CanExecuteObservers;
    using Anori.WinUI.Commands.Commands;
    using Anori.WinUI.Commands.Exceptions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Commands.Interfaces.Builders;

    using JetBrains.Annotations;

    /// <summary>
    ///     Concurrency Sync Command Builder.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IConcurrencySyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IConcurrencySyncCanExecuteBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableConcurrencySyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableConcurrencySyncCanExecuteBuilder" />
    internal sealed class ConcurrencySyncCommandBuilder : IConcurrencySyncCommandBuilder,
                                                          IConcurrencySyncCanExecuteBuilder,
                                                          IActivatableConcurrencySyncCommandBuilder,
                                                          IActivatableConcurrencySyncCanExecuteBuilder
    {
        /// <summary>
        ///     The execute.
        /// </summary>
        private readonly Action<CancellationToken> execute;

        /// <summary>
        ///     The observes.
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        ///     The cancel action.
        /// </summary>
        private Action cancelAction;

        /// <summary>
        ///     The can execute function.
        /// </summary>
        private Func<bool> canExecuteFunction;

        /// <summary>
        ///     The can execute subject.
        /// </summary>
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        ///     The completed action.
        /// </summary>
        private Action completedAction;

        /// <summary>
        ///     The error action.
        /// </summary>
        private Action<Exception> errorAction;

        /// <summary>
        ///     The is automatic actiate.
        /// </summary>
        private bool isAutoActivate = true;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConcurrencySyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="System.ArgumentNullException">execute is null.</exception>
        public ConcurrencySyncCommandBuilder([NotNull] Action<CancellationToken> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Concurrency Sync Command.</returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCanExecuteBuilder.Build(
            Action<IActivatableConcurrencySyncCommand> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.
            ObservesProperty<TType>(Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.Observes(
            ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Activatable Concurrency Sync CanExecute Builder.
        /// </returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.
            ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>
        ///     Activatable Concurrency Sync CanExecute Builder.
        /// </returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.OnCompleted(
            Action completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>
        ///     Activatable Concurrency Sync CanExecute Builder.
        /// </returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.OnCancel(
            Action cancel) =>
            this.OnCancel(cancel);

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.OnError(
            Action<Exception> error) =>
            this.OnError(error);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>
        ///     Activatable Concurrency Sync CanExecute Builder.
        /// </returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCanExecuteBuilder.AutoActivate() =>
            this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Concurrency Sync Command Builder.
        /// </returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCanExecuteBuilder.Build() =>
            this.BuildActivatable();

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        IActivatableConcurrencySyncCommandBuilder IActivatableConcurrencySyncCommandBuilder.OnCompleted(
            Action completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        IActivatableConcurrencySyncCommandBuilder IActivatableConcurrencySyncCommandBuilder.OnCancel(Action cancel) =>
            this.OnCancel(cancel);

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        IActivatableConcurrencySyncCommandBuilder IActivatableConcurrencySyncCommandBuilder.OnError(
            Action<Exception> error) =>
            this.OnError(error);

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Concurrency Sync Command.</returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCommandBuilder.Build(
            Action<IActivatableConcurrencySyncCommand> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.CanExecute(
            Func<bool> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.CanExecute(
            ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IActivatableConcurrencySyncCommandBuilder.AutoActivate() =>
            this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Sync Command.</returns>
        IActivatableConcurrencySyncCommand IActivatableConcurrencySyncCommandBuilder.Build() => this.BuildActivatable();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.OnError(Action<Exception> error) =>
            this.OnError(error);

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.OnCompleted(Action completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.OnCancel(Action cancel) =>
            this.OnCancel(cancel);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Concurrency Sync Command.</returns>
        IConcurrencySyncCommand IConcurrencySyncCanExecuteBuilder.Build(Action<IConcurrencySyncCommand> setCommand) =>
            this.Build(setCommand);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Sync CanExecute Builder.</returns>
        IActivatableConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.Activatable() =>
            this.Activatable();

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCanExecuteBuilder.ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Concurrency Sync Command.</returns>
        IConcurrencySyncCommand IConcurrencySyncCanExecuteBuilder.Build() => this.Build();

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        IConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.OnCompleted(Action completed) =>
            this.OnCompleted(completed);

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        IConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.OnCancel(Action cancel) => this.OnCancel(cancel);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        IConcurrencySyncCommand IConcurrencySyncCommandBuilder.Build(Action<IConcurrencySyncCommand> setCommand) =>
            this.Build(setCommand);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Activatable Concurrency Sync Command Builder.</returns>
        IActivatableConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.Activatable() => this.Activatable();

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.CanExecute(Func<bool> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCanExecuteBuilder IConcurrencySyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>Concurrency Sync Command.</returns>
        IConcurrencySyncCommand IConcurrencySyncCommandBuilder.Build() => this.Build();

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Concurrency Sync CanExecute Builder.</returns>
        IConcurrencySyncCommandBuilder IConcurrencySyncCommandBuilder.OnError(Action<Exception> error) =>
            this.OnError(error);

        /// <summary>
        ///     Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        private ConcurrencySyncCommandBuilder OnError(Action<Exception> error)
        {
            this.errorAction = error;
            return this;
        }

        /// <summary>
        ///     Called when [completed].
        /// </summary>
        /// <param name="completed">The completed.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        private ConcurrencySyncCommandBuilder OnCompleted(Action completed)
        {
            this.completedAction = completed;
            return this;
        }

        /// <summary>
        ///     Called when [cancel].
        /// </summary>
        /// <param name="cancel">The cancel.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        private ConcurrencySyncCommandBuilder OnCancel(Action cancel)
        {
            this.cancelAction = cancel;
            return this;
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        /// <exception cref="CommandBuilderException">
        ///     CommandBuilder Exception.
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
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Concurrency Sync CanExecute Command.</returns>
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
        private ActivatableConcurrencyCanExecuteObserverCommand BuildActivatable(
            [NotNull] Action<ActivatableConcurrencyCanExecuteObserverCommand> setCommand)
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
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Concurrency Sync CanExecute Builder.
        /// </returns>
        /// <exception cref="Anori.WinUI.Commands.Exceptions.NoCanExecuteException">No CanExecute Exception.</exception>
        [NotNull]
        private ActivatableConcurrencyCanExecuteObserverCommand BuildActivatable()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand(
                        this.execute,
                        this.isAutoActivate,
                        this.canExecuteFunction,
                        this.completedAction,
                        this.errorAction,
                        this.cancelAction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ActivatableConcurrencyCanExecuteObserverCommand(
                        this.execute,
                        this.isAutoActivate,
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
                return new ActivatableConcurrencyCanExecuteObserverCommand(
                    this.execute,
                    this.isAutoActivate,
                    this.canExecuteFunction,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ActivatableConcurrencyCanExecuteObserverCommand(
                    this.execute,
                    this.isAutoActivate,
                    this.canExecuteSubject,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            return new ActivatableConcurrencyCanExecuteObserverCommand(
                this.execute,
                this.isAutoActivate,
                this.completedAction,
                this.errorAction,
                this.cancelAction);
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Concurrency Sync CanExecute Command.</returns>
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
        private ConcurrencyCanExecuteObserverCommand Build(
            [NotNull] Action<ConcurrencyCanExecuteObserverCommand> setCommand)
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
        /// <returns>
        ///     Concurrency Sync Command.
        /// </returns>
        /// <exception cref="Anori.WinUI.Commands.Exceptions.NoCanExecuteException">No CanExecute Exception.</exception>
        [NotNull]
        private ConcurrencyCanExecuteObserverCommand Build()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand(
                        this.execute,
                        this.canExecuteFunction,
                        this.completedAction,
                        this.errorAction,
                        this.cancelAction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ConcurrencyCanExecuteObserverCommand(
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
                return new ConcurrencyCanExecuteObserverCommand(
                    this.execute,
                    this.canExecuteFunction,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ConcurrencyCanExecuteObserverCommand(
                    this.execute,
                    this.canExecuteSubject,
                    this.completedAction,
                    this.errorAction,
                    this.cancelAction);
            }

            return new ConcurrencyCanExecuteObserverCommand(
                this.execute,
                this.completedAction,
                this.errorAction,
                this.cancelAction);
        }

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>Concurrency Sync Command Builder.</returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder Activatable()
        {
            this.isAutoActivate = false;
            return this;
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>Concurrency Sync Command Builder.</returns>
        /// <exception cref="ArgumentNullException">expression is null.</exception>
        [NotNull]
        private ConcurrencySyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        /// <returns>Concurrency Sync Command Builder.</returns>
        /// <exception cref="CommandBuilderException">CommandBuilder Exception.</exception>
        [NotNull]
        private ConcurrencySyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        /// <returns>Concurrency Sync Command Builder.</returns>
        /// <exception cref="CommandBuilderException">CommandBuilder Exception.</exception>
        [NotNull]
        private ConcurrencySyncCommandBuilder ObservesCanExecute(
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
        /// <returns>
        ///     Result of ObservesCommandManager as ConcurrencySyncCommandBuilder.
        /// </returns>
        /// <exception cref="Anori.WinUI.Commands.Exceptions.CommandBuilderException">CommandBuilder Exception.</exception>
        [NotNull]
        private ConcurrencySyncCommandBuilder ObservesCommandManager()
        {
            if (this.observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>
        ///     Result of AutoActivate as ConcurrencySyncCommandBuilder.
        /// </returns>
        [NotNull]
        private ConcurrencySyncCommandBuilder AutoActivate()
        {
            this.isAutoActivate = true;
            return this;
        }
    }
}