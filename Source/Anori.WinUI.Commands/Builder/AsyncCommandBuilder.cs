// -----------------------------------------------------------------------
// <copyright file="AsyncCommandBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Anori.WinUI.Commands.CanExecuteObservers;
    using Anori.WinUI.Commands.Commands;
    using Anori.WinUI.Commands.Exceptions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Commands.Interfaces.Builders;

    using JetBrains.Annotations;

    /// <summary>
    /// Async Command Builder.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IAsyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IAsyncCanExecuteBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableAsyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableAsyncCanExecuteBuilder" />
    public sealed class AsyncCommandBuilder : IAsyncCommandBuilder,
                                              IAsyncCanExecuteBuilder,
                                              IActivatableAsyncCommandBuilder,
                                              IActivatableAsyncCanExecuteBuilder
    {
        /// <summary>
        ///     The execute.
        /// </summary>
        [NotNull]
        private readonly Func<Task> execute;

        /// <summary>
        ///     The observes.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        ///     The can execute function.
        /// </summary>
        [CanBeNull]
        private Func<bool> canExecuteFunction;

        /// <summary>
        ///     The can execute expression.
        /// </summary>
        [CanBeNull]
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        ///     The is automatic actiate.
        /// </summary>
        private bool isAutoActivate = true;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AsyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute in null.</exception>
        public AsyncCommandBuilder([NotNull] Func<Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Async Command.</returns>
        IActivatableAsyncCommand IActivatableAsyncCanExecuteBuilder.
            Build(Action<IActivatableAsyncCommand> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.Observes(
            ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.AutoActivate() => this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommand IActivatableAsyncCanExecuteBuilder.Build() => this.BuildActivatable();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableAsyncCommand IActivatableAsyncCommandBuilder.Build(Action<IActivatableAsyncCommand> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.CanExecute(Func<bool> canExecute) =>
            this.CanExecute(canExecute);

        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        IAsyncCanExecuteBuilder IAsyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.AutoActivate() => this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommand IActivatableAsyncCommandBuilder.Build() => this.BuildActivatable();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IAsyncCommand IAsyncCanExecuteBuilder.Build(Action<IAsyncCommand> setCommand) => this.Build(setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.ObservesProperty<TType>(Expression<Func<TType>> canExecute) =>
            this.ObservesProperty(canExecute);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.Activatable() => this.Activatable();

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.ObservesCommandManager() => this.ObservesCommandManager();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IAsyncCommand IAsyncCanExecuteBuilder.Build() => this.Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IAsyncCommand IAsyncCommandBuilder.Build(Action<IAsyncCommand> setCommand) => this.Build(setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCommandBuilder.CanExecute(Func<bool> canExecute) => this.CanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder IAsyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCommandBuilder IAsyncCommandBuilder.ObservesCommandManager() => this.ObservesCommandManager();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IAsyncCommand IAsyncCommandBuilder.Build() => this.Build();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommandBuilder IAsyncCommandBuilder.Activatable() => this.Activatable();

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        ///     CommandBuilder Exception.
        /// </exception>
        [NotNull]
        public AsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        ///     CommandBuilder Exception.
        /// </exception>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        [NotNull]
        private AsyncCommandBuilder CanExecute([NotNull] Func<bool> canExecute)
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
        private AsyncCanExecuteObserverCommand Build([NotNull] Action<AsyncCanExecuteObserverCommand> setCommand)
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
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private ActivatableAsyncCanExecuteObserverCommand BuildActivatable(
            [NotNull] Action<ActivatableAsyncCanExecuteObserverCommand> setCommand)
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
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private ActivatableAsyncCanExecuteObserverCommand BuildActivatable()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand(
                        this.execute,
                        this.isAutoActivate,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand(
                        this.execute,
                        this.isAutoActivate,
                        this.canExecuteSubject,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand(
                    this.execute,
                    this.isAutoActivate,
                    this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand(
                    this.execute,
                    this.isAutoActivate,
                    this.canExecuteSubject);
            }

            return new ActivatableAsyncCanExecuteObserverCommand(this.execute, this.isAutoActivate);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException"></exception>
        [NotNull]
        private AsyncCanExecuteObserverCommand Build()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new AsyncCanExecuteObserverCommand(
                        this.execute,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new AsyncCanExecuteObserverCommand(
                        this.execute,
                        this.canExecuteSubject,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new AsyncCanExecuteObserverCommand(this.execute, this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new AsyncCanExecuteObserverCommand(this.execute, this.canExecuteSubject);
            }

            return new AsyncCanExecuteObserverCommand(this.execute);
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">expression</exception>
        [NotNull]
        private AsyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            this.observes.Add(new PropertyObserverFactory().ObservesProperty(expression));
            return this;
        }

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        private AsyncCommandBuilder Activatable()
        {
            this.isAutoActivate = false;
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">canExecute</exception>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private AsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder AutoActivate()
        {
            this.isAutoActivate = true;
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">CommandBuilder Exception.</exception>
        [NotNull]
        private AsyncCommandBuilder ObservesCommandManager()
        {
            if (this.observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CommandBuilderException(Resources.ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }
    }
}