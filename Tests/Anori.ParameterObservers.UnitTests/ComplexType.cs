// -----------------------------------------------------------------------
// <copyright file="ComplexType.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.ParameterObservers.UnitTests
{
    using Anori.Parameters;

    public class ComplexType : TestPurposeBindableBase
    {
        private ComplexType _innerComplexProperty;

        private int _intProperty;

        public ComplexType InnerComplexProperty
        {
            get => this._innerComplexProperty;
            set => this.SetProperty(ref this._innerComplexProperty, value);
        }

        public int IntProperty
        {
            get => this._intProperty;
            set => this.SetProperty(ref this._intProperty, value);
        }
    }

    public class ComplexParameterType
    {
        public IParameter<ComplexParameterType> InnerComplexProperty { get; } = new Parameter<ComplexParameterType>();

        public IParameter<int> IntProperty { get; } = new Parameter<int>();
    }
}