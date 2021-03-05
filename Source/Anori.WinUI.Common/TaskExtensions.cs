// -----------------------------------------------------------------------
// <copyright file="TaskExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Common
{
    #region

    using System;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    #endregion

    /// <summary>
    ///     Extensions for Tasks
    /// </summary>
    public static partial class TaskExtensions
    {
        /// <summary>
        ///     Determines whether this instance is finished.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>
        ///     <c>true</c> if the specified task is finished; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">task</exception>
        public static bool IsFinished([NotNull] this Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (task.IsCompleted)
            {
                return true;
            }

            if (task.IsCanceled)
            {
                return true;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (task.IsFaulted)
            {
                return true;
            }

            return false;
        }
    }
}