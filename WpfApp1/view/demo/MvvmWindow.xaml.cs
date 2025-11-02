using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.view.demo2
{
    public class Person
    {
        public required string Name { get; set; }
        public required int Age { get; set; }
    }

    // ✅ ViewModel 继承 ObservableObject（自动实现 INotifyPropertyChanged）
    public partial class PersonViewModel : ObservableObject
    {
        private readonly Person _person;

        public PersonViewModel(Person p)
        {
            _person = p;
        }

        // ✅ 使用 [ObservableProperty] 自动生成属性 + 通知逻辑
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int age;

        // ✅ 使用 [RelayCommand] 自动生成 ICommand
        [RelayCommand]
        private void Update()
        {
            Name = "Updated Name";
            Age = 30;
        }
    }

    public partial class MvvmWindow : Window
    {
        public MvvmWindow()
        {
            InitializeComponent();

            var p = new Person { Age = 42, Name = "lisi" };
            var vm = new PersonViewModel(p)
            {
                Name = p.Name,
                Age = p.Age
            };
            DataContext = vm;
        }
    }
}

