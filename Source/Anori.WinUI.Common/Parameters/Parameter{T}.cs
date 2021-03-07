// -----------------------------------------------------------------------
// <copyright file="Parameter.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common.Parameters
{
    using System;

    public class Parameter<T> : IParameter<T>
    {
        /// <summary>
        ///     The value
        /// </summary>
        private T value;

        /// <summary>
        ///     Occurs when [value changed].
        /// </summary>
        event EventHandler<EventArgs<object>> IReadOnlyParameter.ValueChanged
        {
            add => this.InternalValueChanged += value;
            remove => this.InternalValueChanged -= value;
        }

        /// <summary>
        ///     Occurs when [value changed].
        /// </summary>
        public event EventHandler<EventArgs<T>> ValueChanged;

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        object IReadOnlyParameter.Value => this.Value;

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        object IParameter.Value
        {
            get => this.Value;
            set => this.Value = (T)value;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public T Value
        {
            get => this.value;
            set
            {
                if (this.value == null)
                {
                    if (value == null)
                    {
                        return;
                    }
                }
                else if (this.value.Equals(value))
                {
                    return;
                }

                this.value = value;
                this.OnValueChanged(value);
            }
        }

        /// <summary>
        ///     Occurs when [internal value changed].
        /// </summary>
        private event EventHandler<EventArgs<object>> InternalValueChanged;

        /// <summary>
        ///     Called when [value changed].
        /// </summary>
        /// <param name="value">The value.</param>
        protected virtual void OnValueChanged(T value)
        {
            this.InternalValueChanged?.Invoke(this, new EventArgs<object>(value));
            this.ValueChanged?.Invoke(this, new EventArgs<T>(value));
        }
    }
}