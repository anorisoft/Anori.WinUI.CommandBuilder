// -----------------------------------------------------------------------
// <copyright file="AsyncCommandBuilder{T}.cs" company="AnoriSoft">
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

namespace Anori.WinUI.Commands.Builder
{
    public sealed class AsyncCommandBuilder<T> :
        IAsyncCommandBuilder<T>,
        IAsyncCanExecuteBuilder<T>,
        IActivatableAsyncCommandBuilder<T>,
        IActivatableAsyncCanExecuteBuilder<T>
    {
        /// <summary>
        /// The execute
        /// </summary>
        private readonly Func<T, Task> execute;

        /// <summary>
        /// The observes
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        /// The can execute expression
        /// </summary>
        private Predicate<T> canExecuteFunction;

        /// <summary>
        /// The can execute function
        /// </summary>
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        /// The is automatic actiate
        /// </summary>
        private bool isAutoActiate;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        public AsyncCommandBuilder([NotNull] Func<T, Task> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableAsyncCommand<T> IActivatableAsyncCanExecuteBuilder<T>.Build(
            Action<IActivatableAsyncCommand<T>> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.Observes(
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
        IAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.Observes(ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCanExecuteBuilder<T>.AutoActivate() =>
            this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommand<T> IActivatableAsyncCanExecuteBuilder<T>.Build() => this.BuildActicatable();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IActivatableAsyncCommand<T> IActivatableAsyncCommandBuilder<T>.Build(
            Action<IActivatableAsyncCommand<T>> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.CanExecute(
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
        IAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IActivatableAsyncCommandBuilder<T>.AutoActivate() => this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommand<T> IActivatableAsyncCommandBuilder<T>.Build() => this.BuildActicatable();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCanExecuteBuilder<T>.Build(Action<IAsyncCommand<T>> setCommand) =>
            this.Build(setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> canExecute) =>
            this.ObservesProperty(canExecute);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.ObservesCommandManager() => this.ObservesCommandManager();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCanExecuteBuilder<T> IAsyncCanExecuteBuilder<T>.Activatable() => this.Activatable();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCanExecuteBuilder<T>.Build() => this.Build();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCommandBuilder<T>.Build(Action<IAsyncCommand<T>> setCommand) => this.Build(setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        IAsyncCanExecuteBuilder<T> IAsyncCommandBuilder<T>.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns></returns>
        IAsyncCommandBuilder<T> IAsyncCommandBuilder<T>.ObservesCommandManager() => this.ObservesCommandManager();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        IAsyncCommand<T> IAsyncCommandBuilder<T>.Build() => this.Build();

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        IActivatableAsyncCommandBuilder<T> IAsyncCommandBuilder<T>.Activatable() => this.Activatable();

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private AsyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
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
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private ActivatableAsyncCanExecuteObserverCommand<T> BuildActicatable()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.isAutoActiate,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ActivatableAsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.isAutoActiate,
                        this.canExecuteSubject,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand<T>(
                    this.execute,
                    this.isAutoActiate,
                    this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ActivatableAsyncCanExecuteObserverCommand<T>(
                    this.execute,
                    this.isAutoActiate,
                    this.canExecuteSubject);
            }

            return new ActivatableAsyncCanExecuteObserverCommand<T>(this.execute, this.isAutoActiate);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        [NotNull]
        private AsyncCanExecuteObserverCommand<T> Build()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new AsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new AsyncCanExecuteObserverCommand<T>(
                        this.execute,
                        this.canExecuteSubject,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new AsyncCanExecuteObserverCommand<T>(this.execute, this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new AsyncCanExecuteObserverCommand<T>(this.execute, this.canExecuteSubject);
            }

            return new AsyncCanExecuteObserverCommand<T>(this.execute);
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand</exception>
        [NotNull]
        private AsyncCanExecuteObserverCommand<T> Build([NotNull] Action<AsyncCanExecuteObserverCommand<T>> setCommand)
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
        private ActivatableAsyncCanExecuteObserverCommand<T> BuildActivatable(
            [NotNull] Action<ActivatableAsyncCanExecuteObserverCommand<T>> setCommand)
        {
            if (setCommand == null)
            {
                throw new ArgumentNullException(nameof(setCommand));
            }

            var command = this.BuildActicatable();
            setCommand(command);
            return command;
        }

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns></returns>
        private AsyncCommandBuilder<T> Activatable() => this;

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        private AsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        private AsyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        private AsyncCommandBuilder<T> ObservesCommandManager()
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
        /// <returns></returns>
        [NotNull]
        private AsyncCommandBuilder<T> AutoActivate()
        {
            this.isAutoActiate = true;
            return this;
        }
    }
}