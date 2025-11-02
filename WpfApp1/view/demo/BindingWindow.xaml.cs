using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace WpfApp1.view.demo
{
    /// <summary>
    /// BindingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BindingWindow : Window
    {
        public BindingWindow()
        {
            InitializeComponent();
        }

        //private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    txt1.Text = slider.Value.ToString();
        //    txt2.Text = slider.Value.ToString();
        //    txt3.Text = slider.Value.ToString();
        //}

        //private void txt1_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (double.TryParse(txt1.Text, out double value))
        //        slider.Value = value;
        //}

        //private void txt2_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (double.TryParse(txt2.Text, out double value))
        //        slider.Value = value;
        //}

        //private void txt3_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (double.TryParse(txt3.Text, out double value))
        //        slider.Value = value;
        //}
    }
}
