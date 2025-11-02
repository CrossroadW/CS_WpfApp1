using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.view.demo
{
    /// <summary>
    /// AnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationWindow : Window
    {
        public AnimationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(); ;
            animation.From = btn.Width;
            animation.To = btn.Width + 25;
            animation.Duration = TimeSpan.FromMilliseconds(60);
            animation.AutoReverse = true;
            animation.RepeatBehavior = new RepeatBehavior(5);
            animation.Completed += (object? sender, EventArgs e) =>
            {
                btn.BeginAnimation(Button.WidthProperty, null);
                DoubleAnimation animation2 = new DoubleAnimation();
                animation2.BeginTime = TimeSpan.FromMilliseconds(60); // 延迟2秒执行

                animation2.From = btn.Width;
                animation2.To = btn.Width - 30;
                animation2.Duration = TimeSpan.FromMilliseconds(60);
                animation2.RepeatBehavior = new RepeatBehavior(5); 
                animation2.Completed += (o, o2) =>
                {
                    btn.BeginAnimation(Button.WidthProperty, null);

                    btn.Content = "动画已经完成";
                    btn.Width = double.NaN;
                };
                btn.BeginAnimation(Button.WidthProperty, animation2);


            };
            btn.BeginAnimation(Button.WidthProperty, animation);
        }
    }
}
