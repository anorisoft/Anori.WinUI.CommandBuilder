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
using System.Windows.Shapes;

namespace Anori.WinUI.Commands.GUITest
{
    /// <summary>
    /// Interaction logic for PropertyObservableTest.xaml
    /// </summary>
    public partial class PropertyObservableTest : Window
    {
        public PropertyObservableTest()
        {
            InitializeComponent();
            DataContext = new PropertyObservableTestViewModel();
        }
    }
}
