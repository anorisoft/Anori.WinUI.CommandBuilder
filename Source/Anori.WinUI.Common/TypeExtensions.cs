// -----------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;

namespace Anori.WinUI.Common
{
    using JetBrains.Annotations;

    public static class TypeExtensions
    {
        public static bool IsNullable([NotNull] this TypeInfo genericTypeInfo)
        {
            if (genericTypeInfo == null)
            {
                throw new ArgumentNullException(nameof(genericTypeInfo));
            }

            return Nullable.GetUnderlyingType(genericTypeInfo) != null;
        }

        public static bool IsNullable([NotNull] this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Nullable.GetUnderlyingType(type) != null;
        }

        public static bool IsNullable<T>()
        {
            return IsNullable(typeof(T));
        }
    }
}