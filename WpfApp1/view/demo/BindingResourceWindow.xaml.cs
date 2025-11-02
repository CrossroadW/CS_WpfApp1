using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace convert
{
    public class BoolToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = (bool)value;
            return flag ? "开" : "关";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 如果你支持反向绑定这里返回值
            // 我们暂时不需要双向，直接不实现
            throw new NotImplementedException();
        }
    }
}
namespace WpfApp1.view.binding
{
    public class Person
    {
        public string? Name { get; set; }
        public DateTime Birthdate { get; set; } = DateTime.Now;
    }
    public class PersonVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Person _person = new Person();

        public string? Name
        {
            get => _person.Name;
            set
            {
                _person.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public DateTime Birthdate
        {
            get => _person.Birthdate;
            set
            {
                _person.Birthdate = value;
                OnPropertyChanged(nameof(Birthdate));
            }
        }

        private void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        // 示例命令（代替 Button_Click)
        public void AddSuffix()
        {
            Name += "1.";
            Birthdate = DateTime.Now;
            var p = new Person { Name = "ggg" };
            MyItems.Add(p);

        }

        public ObservableCollection<Person> MyItems { get; set; } = new ObservableCollection<Person>
        {
            new Person { Name = "Alice" },
            new Person { Name = "Bob" },
            new Person { Name = "Charlie" }
        };

    }
    /// <summary>
    /// BindingResourceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BindingResourceWindow : Window
    {

        public PersonVM ViewModel { get; } = new PersonVM();

        public BindingResourceWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddSuffix();
        }
    }

}
