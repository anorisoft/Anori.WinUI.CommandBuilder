// -----------------------------------------------------------------------
// <copyright file="CommandTestObject.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using System;
    using System.Linq.Expressions;

    public class CommandTestObject : TestViewModel
    {
        private bool boolProperty;

        private ComplexType complexProperty;

        private int intProperty;

        public bool BoolProperty
        {
            get => this.boolProperty;
            set => this.SetProperty(ref this.boolProperty, value);
        }

        public Expression<Func<bool>> BoolPropertyExpression => () => this.BoolProperty;

        public ComplexType ComplexProperty
        {
            get => this.complexProperty;
            set => this.SetProperty(ref this.complexProperty, value);
        }

        public Expression<Func<int>> ComplexPropertyInnerComplexPropertyInnerComplexPropertyIntPropertyExpression =>
            () => this.ComplexProperty.InnerComplexProperty.InnerComplexProperty.IntProperty;

        public Expression<Func<int>> ComplexPropertyInnerComplexPropertyIntPropertyExpression =>
            () => this.ComplexProperty.InnerComplexProperty.IntProperty;

        public Expression<Func<int>> ComplexPropertyIntPropertyExpression => () => this.ComplexProperty.IntProperty;

        public int IntProperty

        {
            get => this.intProperty;
            set => this.SetProperty(ref this.intProperty, value);
        }

        public Expression<Func<int>> IntPropertyExpression => () => this.IntProperty;

        public bool Type { get; set; }

    }
}