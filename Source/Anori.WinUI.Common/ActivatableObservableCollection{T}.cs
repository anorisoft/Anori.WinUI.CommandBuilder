// -----------------------------------------------------------------------
// <copyright file="ActivatableObservableCollection{T}.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace CanExecuteChangedTests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    using Anori.WinUI.Common;

    public class ActivatableObservableCollection<T> : ObservableCollection<T>
        where T : IActivatable
    {
        /// <summary>
        ///     The is activated
        /// </summary>
        private bool isActivated;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableObservableCollection{T}" /> class.
        /// </summary>
        public ActivatableObservableCollection()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ActivatableObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="collection">The collection from which the tree are copied.</param>
        public ActivatableObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is activated.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is activated; otherwise, <c>false</c>.
        /// </value>
        public bool IsActivated
        {
            get => this.isActivated;
            private set
            {
                if (this.isActivated == value)
                {
                    return;
                }

                this.isActivated = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.IsActivated)));
            }
        }

        /// <summary>
        ///     Activates this instance.
        /// </summary>
        public void Activate()
        {
            if (this.IsActivated)
            {
                return;
            }

            foreach (var item in this.Items)
            {
                item.Activate();
            }

            this.IsActivated = true;
        }

        /// <summary>
        ///     Deactivates this instance.
        /// </summary>
        public void Deactivate()
        {
            if (!this.IsActivated)
            {
                return;
            }

            foreach (var item in this.Items)
            {
                item.Deactivate();
            }

            this.IsActivated = false;
        }

        /// <summary>
        ///     Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            this.Deactivate();
            base.ClearItems();
        }

        /// <summary>
        ///     Inserts an item into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert.</param>
        protected override void InsertItem(int index, T item)
        {
            if (item != null && this.IsActivated)
            {
                item.Activate();
            }

            base.InsertItem(index, item);
        }

        /// <summary>
        ///     Removes the item at the specified index of the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        protected override void RemoveItem(int index)
        {
            var item = this.Items[index];

            if (!this.IsActivated && item != null)
            {
                item.Deactivate();
            }

            base.RemoveItem(index);
        }

        /// <summary>
        ///     Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to replace.</param>
        /// <param name="item">The new value for the element at the specified index.</param>
        protected override void SetItem(int index, T item)
        {
            if (this.IsActivated && item != null)
            {
                item.Activate();
            }

            base.SetItem(index, item);
        }
    }
}