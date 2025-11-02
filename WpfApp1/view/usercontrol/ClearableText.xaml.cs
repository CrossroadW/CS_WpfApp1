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

namespace WpfApp1.view.usercontrol
{
    /// <summary>
    /// ClearableText.xaml 的交互逻辑
    /// </summary>
    public partial class ClearableText : UserControl
    {
        private string placeholder = string.Empty; // 修复 CS8618

        public string PlaceHolder
        {
            get { return placeholder; }
            set { placeholder = value;
            tbPlaceHolder.Text = "ggggggggggggg";
            }
        }

        public ClearableText()
        {
            InitializeComponent();
        }
        
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            textInput.Clear();
            textInput.Focus();
        }

        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textInput.Text))
                tbPlaceHolder.Visibility = Visibility.Visible;
            else
                tbPlaceHolder.Visibility = Visibility.Hidden;
            
        }
    }
}
