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

namespace PairsGame.View
{
    /// <summary>
    /// Interaction logic for CustomSizeWindow.xaml
    /// </summary>
    public partial class CustomSizeWindow : Window
    {
        public CustomSizeWindow()
        {
            InitializeComponent();
        }
        public void OnClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
