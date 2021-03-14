// -----------------------------------------------------------------------
// <copyright file="SyncCommandBuilder.cs" company="AnoriSoft">
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
    ///     The Synchronize Command Builder class.
    /// </summary>
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.ISyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.ISyncCanExecuteBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableSyncCommandBuilder" />
    /// <seealso cref="Anori.WinUI.Commands.Interfaces.Builders.IActivatableSyncCanExecuteBuilder" />
    internal sealed class SyncCommandBuilder : ISyncCommandBuilder,
                                               ISyncCanExecuteBuilder,
                                               IActivatableSyncCommandBuilder,
                                               IActivatableSyncCanExecuteBuilder
    {
        /// <summary>
        ///     The execute.
        /// </summary>
        private readonly Action execute;

        /// <summary>
        ///     The observes.
        /// </summary>
        private readonly List<ICanExecuteChangedSubject> observes = new List<ICanExecuteChangedSubject>();

        /// <summary>
        ///     The can execute function.
        /// </summary>
        private Func<bool> canExecuteFunction;

        /// <summary>
        ///     The can execute expression.
        /// </summary>
        private ICanExecuteSubject canExecuteSubject;

        /// <summary>
        ///     The is automatic actiate.
        /// </summary>
        private bool isAutoActivate = true;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SyncCommandBuilder" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="ArgumentNullException">execute is null.</exception>
        public SyncCommandBuilder([NotNull] Action execute) =>
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Sync Command.</returns>
        IActivatableSyncCommand IActivatableSyncCanExecuteBuilder.Build(Action<IActivatableSyncCommand> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.ObservesProperty<TType>(
            Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.Observes(ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.ObservesCommandManager() =>
            this.ObservesCommandManager();

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCanExecuteBuilder.AutoActivate() => this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Command.
        /// </returns>
        IActivatableSyncCommand IActivatableSyncCanExecuteBuilder.Build() => this.BuildActivatable();

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     Activatable Sync Command.
        /// </returns>
        IActivatableSyncCommand IActivatableSyncCommandBuilder.Build(Action<IActivatableSyncCommand> setCommand) =>
            this.BuildActivatable(setCommand);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.CanExecute(Func<bool> canExecute) =>
            this.CanExecute(canExecute);

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.ObservesCanExecute(
            Expression<Func<bool>> canExecute,
            bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder IActivatableSyncCommandBuilder.AutoActivate() => this.AutoActivate();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Command.
        /// </returns>
        IActivatableSyncCommand IActivatableSyncCommandBuilder.Build() => this.BuildActivatable();

        /// <summary>
        ///     Observeses the specified observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        public ISyncCanExecuteBuilder Observes(ICanExecuteChangedSubject observer)
        {
            this.observes.Add(observer);
            return this;
        }

        /// <summary>
        ///     Observeses the command manager.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder ISyncCanExecuteBuilder.ObservesCommandManager() => this.ObservesCommandManager();

        /// <summary>
        ///     Observeses the property.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder ISyncCanExecuteBuilder.ObservesProperty<TType>(Expression<Func<TType>> expression) =>
            this.ObservesProperty(expression);

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        ISyncCommand ISyncCanExecuteBuilder.Build(Action<ISyncCommand> setCommand) => this.Build(setCommand);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCanExecuteBuilder ISyncCanExecuteBuilder.Activatable() => this.Activatable();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Sync Command.
        /// </returns>
        ISyncCommand ISyncCanExecuteBuilder.Build() => this.Build();

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.CanExecute(ICanExecuteSubject canExecute)
        {
            this.canExecuteSubject = canExecute;
            return this;
        }

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.CanExecute(Func<bool> canExecute) => this.CanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.ObservesCanExecute(Expression<Func<bool>> canExecute) =>
            this.ObservesCanExecute(canExecute);

        /// <summary>
        ///     Observeses the can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <param name="fallback">if set to <c>true</c> [fallback].</param>
        /// <returns>
        ///     Sync Can Execute Command Builder.
        /// </returns>
        ISyncCanExecuteBuilder ISyncCommandBuilder.
            ObservesCanExecute(Expression<Func<bool>> canExecute, bool fallback) =>
            this.ObservesCanExecute(canExecute, fallback);

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>
        ///     Sync Command.
        /// </returns>
        ISyncCommand ISyncCommandBuilder.Build(Action<ISyncCommand> setCommand) => this.Build(setCommand);

        /// <summary>
        ///     Activatables this instance.
        /// </summary>
        /// <returns>
        ///     Activatable Sync Can Execute Command Builder.
        /// </returns>
        IActivatableSyncCommandBuilder ISyncCommandBuilder.Activatable() => this.Activatable();

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Sync Command.
        /// </returns>
        ISyncCommand ISyncCommandBuilder.Build() => this.Build();

        /// <summary>
        ///     Determines whether this instance can execute the specified can execute.
        /// </summary>
        /// <param name="canExecute">The can execute.</param>
        /// <returns>Sync Command Builder.</returns>
        /// <exception cref="CommandBuilderException">
        ///     Command Builder Exception.
        /// </exception>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
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
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Activatable Can Execute Observer Command.</returns>
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
        private ActivatableCanExecuteObserverCommand BuildActivatable(
            [NotNull] Action<ActivatableCanExecuteObserverCommand> setCommand)
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
        ///     Activatable Can Execute Observer Command.
        /// </returns>
        /// <exception cref="NoCanExecuteException">No Can Execute Exception.</exception>
        [NotNull]
        private ActivatableCanExecuteObserverCommand BuildActivatable()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new ActivatableCanExecuteObserverCommand(
                        this.execute,
                        this.isAutoActivate,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new ActivatableCanExecuteObserverCommand(
                        this.execute,
                        this.isAutoActivate,
                        this.canExecuteSubject,
                        this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new ActivatableCanExecuteObserverCommand(
                    this.execute,
                    this.isAutoActivate,
                    this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new ActivatableCanExecuteObserverCommand(
                    this.execute,
                    this.isAutoActivate,
                    this.canExecuteSubject);
            }

            return new ActivatableCanExecuteObserverCommand(this.execute, this.isAutoActivate);
        }

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        /// <returns>
        ///     Can Execute Observer Command.
        /// </returns>
        /// <exception cref="NoCanExecuteException">No Can Execute Exception.</exception>
        [NotNull]
        private CanExecuteObserverCommand Build()
        {
            if (this.observes.Any())
            {
                if (this.canExecuteFunction != null)
                {
                    return new CanExecuteObserverCommand(
                        this.execute,
                        this.canExecuteFunction,
                        this.observes.ToArray());
                }

                if (this.canExecuteSubject != null)
                {
                    return new CanExecuteObserverCommand(this.execute, this.canExecuteSubject, this.observes.ToArray());
                }

                throw new NoCanExecuteException();
            }

            if (this.canExecuteFunction != null)
            {
                return new CanExecuteObserverCommand(this.execute, this.canExecuteFunction);
            }

            if (this.canExecuteSubject != null)
            {
                return new CanExecuteObserverCommand(this.execute, this.canExecuteSubject);
            }

            return new CanExecuteObserverCommand(this.execute);
        }

        /// <summary>
        ///     Builds the specified set command.
        /// </summary>
        /// <param name="setCommand">The set command.</param>
        /// <returns>Can Execute Observer Command.</returns>
        /// <exception cref="ArgumentNullException">setCommand is null.</exception>
        private CanExecuteObserverCommand Build([NotNull] Action<CanExecuteObserverCommand> setCommand)
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
        /// <returns>Sync Command Builder.</returns>
        /// <exception cref="ArgumentNullException">expression is null.</exception>
        [NotNull]
        private SyncCommandBuilder ObservesProperty<TType>([NotNull] Expression<Func<TType>> expression)
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
        /// <returns>Sync Command Builder.</returns>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        /// <exception cref="CommandBuilderException">Command Builder Exception. </exception>
        [NotNull]
        private SyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute)
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
        /// <returns>Sync Command Builder.</returns>
        /// <exception cref="ArgumentNullException">canExecute is null.</exception>
        /// <exception cref="CommandBuilderException">Command Builder Exception. </exception>
        [NotNull]
        private SyncCommandBuilder ObservesCanExecute([NotNull] Expression<Func<bool>> canExecute, bool fallback)
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
        /// <returns>Sync Command Builder.</returns>
        /// <exception cref="CommandBuilderException">Command Builder Exception.</exception>
        [NotNull]
        private SyncCommandBuilder ObservesCommandManager()
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
        /// <returns>Sync Command Builder.</returns>
        [NotNull]
        private SyncCommandBuilder Activatable()
        {
            this.isAutoActivate = false;
            return this;
        }

        /// <summary>
        ///     Automatics the activate.
        /// </summary>
        /// <returns>Sync Command Builder.</returns>
        [NotNull]
        private SyncCommandBuilder AutoActivate()
        {
            this.isAutoActivate = true;
            return this;
        }
    }
}