// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.CanExecuteChangedTests
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            CommandManager.RequerySuggested += this.CommandManagerOnRequerySuggested;

            this.DataContext = new MainViewModel(this);
        }

        private void CommandManagerOnRequerySuggested(object sender, EventArgs e)
        {
            Debug.WriteLine("On RequerySuggested {0}", sender?.GetType());
        }
    }
}