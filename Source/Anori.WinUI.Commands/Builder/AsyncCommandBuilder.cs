// -----------------------------------------------------------------------
// <copyright file="AsyncCommandBuilder.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Builder
{
    using Anori.WinUI.Commands.CanExecuteObservers;
    using Anori.WinUI.Commands.Commands;
    using Anori.WinUI.Commands.Exceptions;
    using Anori.WinUI.Commands.Interfaces;
    using Anori.WinUI.Commands.Interfaces.Builders;
    using Anori.WinUI.Commands.Resources;
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    ///     Async Command Builder.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IAsyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IAsyncCanExecuteBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableAsyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableAsyncCanExecuteBuilder" />
    internal sealed class AsyncCommandBuilder : IAsyncCommandBuilder,
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
        public AsyncCommandBuilder([NotNull] Func<Task> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        ///     Result of ObservesCanExecute as AsyncCommandBuilder.
        /// </returns>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        /// <exception cref="CommandBuilderException">CommandBuilder Exception.</exception>
        [NotNull]
        public AsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
        {
            if (canExecute == null)
            {
                throw new ArgumentNullException(nameof(canExecute));
            }

            if (this.canExecuteSubject != null)
            {
                throw new CanExecuteFunctionAlreadyDefinedException(ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CanExecuteFunctionAlreadyDefinedException(ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteSubject = CanExecuteObserver.Create(canExecute, fallback);
            return this;
        }

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.Activatable()
        {
            return this.Activatable();
        }

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     The Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCommandBuilder IAsyncCommandBuilder.Activatable()
        {
            return this.Activatable();
        }

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.AutoActivate()
        {
            return this.AutoActivate();
        }

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.AutoActivate()
        {
            return this.AutoActivate();
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     Activatable Async Command.
        /// </returns>
        IActivatableAsyncCommand IActivatableAsyncCanExecuteBuilder.Build(Action<IActivatableAsyncCommand> setCommand)
        {
            return this.BuildActivatable(setCommand);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>The Activatable Async CanExecute Command Builder.</returns>
        IActivatableAsyncCommand IActivatableAsyncCanExecuteBuilder.Build()
        {
            return this.BuildActivatable();
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     Activatable Async Command.
        /// </returns>
        IActivatableAsyncCommand IActivatableAsyncCommandBuilder.Build(Action<IActivatableAsyncCommand> setCommand)
        {
            return this.BuildActivatable(setCommand);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Async Command.
        /// </returns>
        IActivatableAsyncCommand IActivatableAsyncCommandBuilder.Build()
        {
            return this.BuildActivatable();
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     The Async Command.
        /// </returns>
        IAsyncCommand IAsyncCanExecuteBuilder.Build(Action<IAsyncCommand> setCommand)
        {
            return this.Build(setCommand);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     The Async Command.
        /// </returns>
        IAsyncCommand IAsyncCanExecuteBuilder.Build()
        {
            return this.Build();
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     The Async Command.
        /// </returns>
        IAsyncCommand IAsyncCommandBuilder.Build(Action<IAsyncCommand> setCommand)
        {
            return this.Build(setCommand);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     The Async Command.
        /// </returns>
        IAsyncCommand IAsyncCommandBuilder.Build()
        {
            return this.Build();
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.CanExecute(Func<bool> canExecute)
        {
            return this.CanExecute(canExecute);
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     The Async CanExecute Command Builder.
        /// </returns>
        IAsyncCanExecuteBuilder IAsyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     The Async CanExecute Command Builder.
        /// </returns>
        IAsyncCanExecuteBuilder IAsyncCommandBuilder.CanExecute(Func<bool> canExecute)
        {
            return this.CanExecute(canExecute);
        }

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
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
        /// <returns>
        ///     The Async CanExecute Command Builder.
        /// </returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute)
        {
            return this.ObservesCanExecute(canExecute);
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback)
        {
            return this.ObservesCanExecute(canExecute, fallback);
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     The Async CanExecute Command Builder.
        /// </returns>
        IAsyncCanExecuteBuilder IAsyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute)
        {
            return this.ObservesCanExecute(canExecute);
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.ObservesCommandManager()
        {
            return this.ObservesCommandManager();
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Async CanExecute Command Builder.
        /// </returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.ObservesCommandManager()
        {
            return this.ObservesCommandManager();
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     The Async CanExecute Command Builder.
        /// </returns>
        IAsyncCommandBuilder IAsyncCommandBuilder.ObservesCommandManager()
        {
            return this.ObservesCommandManager();
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     The Activatable Async CanExecute Command Builder.
        /// </returns>
        IActivatableAsyncCanExecuteBuilder IActivatableAsyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression)
        {
            return this.ObservesProperty(expression);
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Async CanExecute Command Builder.
        /// </returns>
        IAsyncCanExecuteBuilder IAsyncCanExecuteBuilder.ObservesProperty<TType>(Expression<Func<TType>> canExecute)
        {
            return this.ObservesProperty(canExecute);
        }

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>The Async Command Builder.</returns>
        private AsyncCommandBuilder Activatable()
        {
            this.isAutoActivate = false;
            return this;
        }

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>
        ///     The Async Command Builder.
        /// </returns>
        [NotNull]
        private AsyncCommandBuilder AutoActivate()
        {
            this.isAutoActivate = true;
            return this;
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     The Async CanExecuteObserver Command Builder.
        /// </returns>
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
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
        ///     Builds this instance.
        /// </summary>
        /// <returns>The Async CanExecute Observer Command.</returns>
        /// <exception cref="NoCanExecuteException">No CanExecute Exception.</exception>
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
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     The Activatable Async CanExecuteObserver Command.
        /// </returns>
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
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
        /// <returns>The Activatable Async CanExecute Observer Command.</returns>
        /// <exception cref="NoCanExecuteException">No Can Execute Exception.</exception>
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
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>The Async Command Builder.</returns>
        /// <exception cref="CommandBuilderException">CommandBuilder Exception.</exception>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        [NotNull]
        private AsyncCommandBuilder CanExecute([NotNull] Func<bool> canExecute)
        {
            if (this.canExecuteFunction != null)
            {
                throw new CanExecuteFunctionAlreadyDefinedException(ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            if (this.canExecuteSubject != null)
            {
                throw new CanExecuteFunctionAlreadyDefinedException(ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            this.canExecuteFunction = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Thes AsyncCommandBuilder.
        /// </returns>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        /// <exception cref="CommandBuilderException">Command Builder Exception. </exception>
        [NotNull]
        private AsyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
        {
            if (canExecute == null)
            {
                throw new ArgumentNullException(nameof(canExecute));
            }

            if (this.canExecuteSubject != null)
            {
                throw new CanExecuteFunctionAlreadyDefinedException(ExceptionStrings.CanExecuteExpressionAlreadyDefined);
            }

            if (this.canExecuteFunction != null)
            {
                throw new CanExecuteFunctionAlreadyDefinedException(ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.canExecuteSubject = CanExecuteObserver.Create(canExecute);
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>The Async Command Builder.</returns>
        /// <exception cref="CommandBuilderException">CommandBuilder Exception.</exception>
        [NotNull]
        private AsyncCommandBuilder ObservesCommandManager()
        {
            if (this.observes.Contains(CommandManagerObserver.Observer))
            {
                throw new CanExecuteFunctionAlreadyDefinedException(ExceptionStrings.CanExecuteFunctionAlreadyDefined);
            }

            this.observes.Add(CommandManagerObserver.Observer);
            return this;
        }

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The Async Command Builder.</returns>
        /// <exception cref="ArgumentNullException">expression is null.</exception>
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
    }
}