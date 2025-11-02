using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WpfApp1.view.grid
{

    /// <summary>
    /// GridWindow.xaml 的交互逻辑
    /// </summary>

    public partial class GridWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Person> People { get; set; }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public GridWindow()
        {
            InitializeComponent();
            DataContext = this;
            People = new ObservableCollection<Person>
            {
                new Person { Name = "Alice", Age = 30 },
                new Person { Name = "Bob", Age = 25 },
                new Person { Name = "Charlie", Age = 35 }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            People = new ObservableCollection<Person>
            {
                new Person { Name = "123", Age = 30 },
                new Person { Name = "123", Age = 25 },
                new Person { Name = "ggg", Age = 35 }
            };
            // 或者通过更新属性，确保绑定更新
            OnPropertyChanged(nameof(People));
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

}
