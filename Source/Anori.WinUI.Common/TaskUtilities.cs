// -----------------------------------------------------------------------
// <copyright file="TaskUtilities.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Anori.WinUI.Common
{
    public static partial class TaskExtensions
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
#pragma warning disable S3168 // "async" methods should not return "void"

        public static async void FireAndForgetSafeAsync(this Task task, Action<Exception> error = null)
#pragma warning restore S3168 // "async" methods should not return "void"
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                error.Raise(ex);
            }
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
#pragma warning disable S3168 // "async" methods should not return "void"

        public static async void FireAndForgetSafeAsync(
            [NotNull] this Task task,
            [CanBeNull] Action completed = null,
            [CanBeNull] Action<Exception> error = null,
            [CanBeNull] Action final = null,
            [CanBeNull] Action cancel = null,
            bool configureAwait = false)
#pragma warning restore S3168 // "async" methods should not return "void"
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                await task.ConfigureAwait(configureAwait);
                completed?.Invoke();
            }
            catch (TaskCanceledException)
            {
                cancel?.Invoke();
            }
            catch (Exception ex)
            {
                error?.Invoke(ex);
            }
            finally
            {
                final?.Invoke();
            }
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
#pragma warning disable S3168 // "async" methods should not return "void"

        public static async void FireAndForgetSafeAsync<T>(
            [NotNull] this Task<T> task,
            [CanBeNull] Action<T> completed = null,
            [CanBeNull] Action<Exception> error = null,
            [CanBeNull] Action final = null,
            [CanBeNull] Action cancel = null,
            bool configureAwait = false)
#pragma warning restore S3168 // "async" methods should not return "void"
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                var result = await task.ConfigureAwait(configureAwait);
                completed?.Invoke(result);
            }
            catch (TaskCanceledException)
            {
                cancel?.Invoke();
            }
            catch (Exception ex)
            {
                error?.Invoke(ex);
            }
            finally
            {
                final?.Invoke();
            }
        }

#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
#pragma warning disable S3168 // "async" methods should not return "void"

        public static async void FireAndForgetSafeAsync<T>(
            [NotNull] this Task<T> task,
            [CanBeNull] Func<T, Task> completed = null,
            [CanBeNull] Func<Exception, Task> error = null,
            [CanBeNull] Func<Task> final = null,
            [CanBeNull] Func<Task> cancel = null,
            bool configureAwait = false)
#pragma warning restore S3168 // "async" methods should not return "void"
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            try
            {
                var result = await task.ConfigureAwait(configureAwait);
                if (completed != null)
                {
                    await completed(result);
                }
            }
            catch (TaskCanceledException)
            {
                if (cancel != null)
                {
                    await cancel.Invoke();
                }
            }
            catch (Exception ex)
            {
                if (error != null)
                {
                    await error.Invoke(ex);
                }
            }
            finally
            {
                if (final != null)
                {
                    await final.Invoke();
                }
            }
        }
    }
}