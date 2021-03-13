﻿// -----------------------------------------------------------------------
// <copyright file="PropertyObservableTest.xaml.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest
{
    using System.Windows;

    /// <summary>
    ///     Interaction logic for PropertyObservableTest.xaml
    /// </summary>
    public partial class PropertyObservableAsyncTestView : Window
    {
        public PropertyObservableAsyncTestView()
        {
            this.InitializeComponent();
            this.DataContext = new PropertyObservableAsyncTestViewModel();
        }
    }
}