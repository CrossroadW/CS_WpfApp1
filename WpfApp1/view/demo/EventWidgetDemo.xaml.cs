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

namespace WpfApp1.view.demo
{
    /// <summary>
    /// EventWidgetDemo.xaml 的交互逻辑
    /// </summary>
    public partial class EventWidgetDemo : Window
    {
        public EventWidgetDemo()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            btn.Content = "鼠标进入";
        }

        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;
            if (null == btn) return;
            btn.Content = "鼠标离开";
        }
    }
}
