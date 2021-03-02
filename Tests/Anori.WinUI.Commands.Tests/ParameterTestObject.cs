// -----------------------------------------------------------------------
// <copyright file="CommandTestObject - Copy.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.Tests
{
    using System;
    using System.Linq.Expressions;

    using Anori.WinUI.Common.Parameters;

    public class ParameterTestObject
    {
        public IParameter<bool> BoolParameter { get; } = new Parameter<bool>();

        public Expression<Func<IParameter<bool>>> BoolParameterExpression => () => this.BoolParameter;

        public IParameter<ComplexParameterType> ComplexProperty { get; } = new Parameter<ComplexParameterType>();

        public Expression<Func<IParameter<int>>> ComplexPropertyInnerComplexPropertyInnerComplexPropertyIntPropertyExpression =>
            () => this.ComplexProperty.Value.InnerComplexProperty.Value.InnerComplexProperty.Value.IntProperty;

        public Expression<Func<IParameter<int>>> ComplexPropertyInnerComplexPropertyIntPropertyExpression =>
            () => this.ComplexProperty.Value.InnerComplexProperty.Value.IntProperty;

        public Expression<Func<IParameter<int>>> ComplexPropertyIntPropertyExpression => () => this.ComplexProperty.Value.IntProperty;

        public IParameter<int> IntParameter { get; } = new Parameter<int>();

        public Expression<Func<IParameter<int>>> IntPropertyExpression => () => this.IntParameter;

        public bool Type { get; set; }

        public void Dollens()
        {
        }
    }
}