// -----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Anori Soft">
// Copyright (c) Anori Soft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanExecuteChanged.Tests
{
    using System.Diagnostics;

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