// -----------------------------------------------------------------------
// <copyright file="IsNullableTest.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using Anori.WinUI.Common;

    using NUnit.Framework;

    [TestFixture]
    public class IsNullableTest
    {
        [Test]
        public void IsNullable_ListOfT_Test()
        {
            var typeInfo = typeof(List<int>).GetTypeInfo();

            if (!typeInfo.IsValueType)
            {
                return;
            }

            if (!typeInfo.IsGenericType)
            {
                throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
            }

            if (!typeof(Nullable<>).GetTypeInfo().IsAssignableFrom(typeInfo.GetGenericTypeDefinition().GetTypeInfo()))
            {
                throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
            }
        }

        [Test]
        public void IsNullable_MyGenericStructInt_Test()
        {
            Assert.Throws<InvalidCastException>(
                () =>
                    {
                        var typeInfo = typeof(MyGenericStruct<int>).GetTypeInfo();

                        if (!typeInfo.IsValueType)
                        {
                            return;
                        }

                        if (!typeInfo.IsGenericType)
                        {
                            throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
                        }

                        if (!typeof(Nullable<>).GetTypeInfo()
                                .IsAssignableFrom(typeInfo.GetGenericTypeDefinition().GetTypeInfo()))
                        {
                            throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
                        }
                    });
        }

        [Test]
        public void IsNullable2_ListOfT_Test()
        {
            var typeInfo = typeof(List<int>).GetTypeInfo();
            if (typeInfo.IsValueType)
            {
                if (!typeInfo.IsNullable())
                {
                    throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
                }
            }
        }

        [Test]
        public void IsNullable2_MyGenericStructInt_Test()
        {
            Assert.Throws<InvalidCastException>(
                () =>
                    {
                        var typeInfo = typeof(MyGenericStruct<int>).GetTypeInfo();
                        if (!typeInfo.IsNullable())
                        {
                            throw new InvalidCastException("DelegateCommandInvalidGenericPayloadType");
                        }
                    });
        }

        [Test]
        public void Type_IsNullable_ListOfT_Test()
        {
            var value = typeof(List<object>).IsNullable();
            Assert.IsFalse(value);
        }

        [Test]
        public void Type_IsNullable_MyType_Test()
        {
            var value = typeof(MyType).IsNullable();
            Assert.IsFalse(value);
        }

        [Test]
        public void Type_IsNullable_NullableInt_Test()
        {
            var value = typeof(int?).IsNullable();
            Assert.IsTrue(value);
        }

        [Test]
        public void Type_IsNullable_Object_Test()
        {
            var value = typeof(object).IsNullable();
            Assert.IsFalse(value);
        }

        [Test]
        public void TypeInfo_IsNullable_ListOfT_Test()
        {
            var value = typeof(List<object>).GetTypeInfo().IsNullable();
            Assert.IsFalse(value);
        }

        [Test]
        public void TypeInfo_IsNullable_MyType_Test()
        {
            var value = typeof(MyType).GetTypeInfo().IsNullable();
            Assert.IsFalse(value);
        }

        [Test]
        public void TypeInfo_IsNullable_Object_Test()
        {
            var value = typeof(object).GetTypeInfo().IsNullable();
            Assert.IsFalse(value);
        }

        [Test]
        public void TypeInfo_IsNullable1_NullableInt_Test()
        {
            var value = typeof(int?).GetTypeInfo().IsNullable();
            Assert.IsTrue(value);
        }

        [Test]
        public void TypeInfo_IsNullable2_NullableInt_Test()
        {
            var value = typeof(int?).GetTypeInfo().IsNullable();
            Assert.IsTrue(value);
        }

        [Test]
        public void TypeOf_IsNullable_ListOfT_Test()
        {
            var value = TypeExtensions.IsNullable<List<object>>();
            Assert.IsFalse(value);
        }

        [Test]
        public void TypeOf_IsNullable_MyType_Test()
        {
            var value = TypeExtensions.IsNullable<MyType>();
            Assert.IsFalse(value);
        }

        [Test]
        public void TypeOf_IsNullable_Object_Test()
        {
            var value = TypeExtensions.IsNullable<object>();
            Assert.IsFalse(value);
        }

        [Test]
        public void TypeOf_IsNullable1_NullableInt_Test()
        {
            var value = TypeExtensions.IsNullable<int?>();
            Assert.IsTrue(value);
        }

        [Test]
        public void TypeOf_IsNullable2_NullableInt_Test()
        {
            var value = TypeExtensions.IsNullable<int?>();
            Assert.IsTrue(value);
        }
    }

    public class MyType
    {
    }
}