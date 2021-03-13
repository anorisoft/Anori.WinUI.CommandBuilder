// -----------------------------------------------------------------------
// <copyright file="SyncCommandBuilder{T}.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Builder
{
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

    /// <summary>
    /// The Synchronize Command Builder class.
    /// </summary>
    /// <typeparam name="T">Parameter type.</typeparam>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.ISyncCommandBuilder{T}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.ISyncCanExecuteBuilder{T}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableSyncCommandBuilder{T}" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableSyncCanExecuteBuilder{T}" />
    internal sealed class SyncCommandBuilder<T> : ISyncCommandBuilder<T>,
                                                  ISyncCanExecuteBuilder<T>,
                                                  IActivatableSyncCommandBuilder<T>,
                                                  IActivatableSyncCanExecuteBuilder<T>
    {
        /// <summary>
        ///     The execute.
        /// </summary>
        [NotNull]
        private readonly Action<T> execute;

        /// <summary>
        ///     The observes.
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        ///     The can execute function.
        /// </summary>
        [CanBeNull]
        private Predicate<T> canExecuteFunction;

        /// <summary>
        ///     The can execute expression.
        /// </summary>
        [CanBeNull]
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        ///     The is automatic actiate.
        /// </summary>
        private bool isAutoActiate;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncCommandBuilder{T}"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute</exception>
        /// <exception cref="SyncCommandBuilder">execute</exception>
        public SyncCommandBuilder([NotNull] Action<T> execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        /// Activatable Sync Command.
        /// </returns>
        IActivatableSyncCommand<T> IActivatableSyncCanExecuteBuilder<T>.Build(
            Action<IActivatableSyncCommand<T>> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCanExecuteBuilder<T>.ObservesProperty<TType>(
            Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCanExecuteBuilder<T>.Observes(
            ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCanExecuteBuilder<T>.ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCanExecuteBuilder<T>.AutoActivate() => this.AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// Activatable Sync Command.
        /// </returns>
        IActivatableSyncCommand<T> IActivatableSyncCanExecuteBuilder<T>.Build() => this.BuildActivatable();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        /// Activatable Sync Command.
        /// </returns>
        IActivatableSyncCommand<T> IActivatableSyncCommandBuilder<T>.Build(
            Action<IActivatableSyncCommand<T>> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCommandBuilder<T>.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> IActivatableSyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Automatics the activate.
        /// </summary>
        /// <returns>
        /// Activatable Sync Command Builder.
        /// </returns>
        IActivatableSyncCommandBuilder<T> IActivatableSyncCommandBuilder<T>.AutoActivate() => this.AutoActivate();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// Activatable Sync Command.
        /// </returns>
        IActivatableSyncCommand<T> IActivatableSyncCommandBuilder<T>.Build() => this.BuildActivatable();

        /// <summary>
        /// Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        /// Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder<T> ISyncCanExecuteBuilder<T>.Observes(ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        /// Sync Command.
        /// </returns>
        ISyncCommand<T> ISyncCanExecuteBuilder<T>.Build(Action<ISyncCommand<T>> setCommand) => this.Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns>
        /// Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder<T> ISyncCanExecuteBuilder<T>.Activatable() => this.Activatable();

        /// <summary>
        /// Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder<T> ISyncCanExecuteBuilder<T>.
            ObservesProperty<TType>(Expression<Func<TType>> canExecute) =>
            this.ObservesProperty(canExecute);

        /// <summary>
        /// Observeses the command manager.
        /// </summary>
        /// <returns>
        /// Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder<T> ISyncCanExecuteBuilder<T>.ObservesCommandManager() => this.ObservesCommandManager();

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// Sync Command.
        /// </returns>
        ISyncCommand<T> ISyncCanExecuteBuilder<T>.Build() => this.Build();

        /// <summary>
        /// Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        /// Sync Command.
        /// </returns>
        ISyncCommand<T> ISyncCommandBuilder<T>.Build(Action<ISyncCommand<T>> setCommand) => this.Build(setCommand);

        /// <summary>
        /// Activatables this instance.
        /// </summary>
        /// <returns>
        /// Activatable Sync Command Builder.
        /// </returns>
        IActivatableSyncCommandBuilder<T> ISyncCommandBuilder<T>.Activatable() => this.Activatable();

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        /// Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        /// Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        /// Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder<T> ISyncCommandBuilder<T>.CanExecute(Predicate<T> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// Sync Command.
        /// </returns>
        ISyncCommand<T> ISyncCommandBuilder<T>.Build() => this.Build();

        /// <summary>
        /// Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns></returns>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        [NotNull]
        private SyncCommandBuilder<T> CanExecute([NotNull] Predicate<T> canExecute)
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
        /// Activatables this instance.
        /// </summary>
        /// <returns></returns>
        private SyncCommandBuilder<T> Activatable() => this;

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException">No Can Execute Exception.</exception>
        [NotNull]
        private ActivatableCanExecuteObserverCommand<T> BuildActivatable()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ActivatableCanExecuteObserverCommand<T>(
                        this.execute,
                        this.isAutoActiate,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ActivatableCanExecuteObserverCommand<T>(
                        this.execute,
                        this.isAutoActiate,
                        this.canExecuteSubject,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new ActivatableCanExecuteObserverCommand<T>(
                    this.execute,
                    this.isAutoActiate,
                    this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ActivatableCanExecuteObserverCommand<T>(
                    this.execute,
                    this.isAutoActiate,
                    this.canExecuteSubject);
            }

            return new ActivatableCanExecuteObserverCommand<T>(this.execute, this.isAutoActiate);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoCanExecuteException">No Can Execute Exception.</exception>
        [NotNull]
        private CanExecuteObserverCommand<T> Build()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new CanExecuteObserverCommand<T>(
                        this.execute,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new CanExecuteObserverCommand<T>(
                        this.execute,
                        this.canExecuteSubject,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new CanExecuteObserverCommand<T>(this.execute, this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new CanExecuteObserverCommand<T>(this.execute, this.canExecuteSubject);
            }

            return new CanExecuteObserverCommand<T>(this.execute);
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
        [NotNull]
        private ActivatableCanExecuteObserverCommand<T> BuildActivatable(
            [NotNull] Action<ActivatableCanExecuteObserverCommand<T>> setCommand)
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
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
        [NotNull]
        private CanExecuteObserverCommand<T> Build([NotNull] Action<CanExecuteObserverCommand<T>> setCommand)
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
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">expression is null.</exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        /// <exception cref="CommandBuilderException">
        /// </exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        /// <exception cref="CommandBuilderException">Command Builder Exception.</exception>
        [NotNull]
        private SyncCommandBuilder<T> ObservesCommandManager()
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
        private SyncCommandBuilder<T> AutoActivate()
        {
            this.isAutoActiate = true;
            return this;
        }
    }
}