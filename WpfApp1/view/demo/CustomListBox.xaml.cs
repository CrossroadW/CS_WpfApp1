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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp1.view.demo
{
    public class ItemVM : INotifyPropertyChanged
    {
        private string _name;
        private string _description;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private void OnPropertyChanged(string prop)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ItemVM> Items { get; set; }= new ObservableCollection<ItemVM>
            {
                new ItemVM { Name = "项目1", Description = "这是第一个项目" },
                new ItemVM { Name = "项目2", Description = "这是第二个项目" },
                new ItemVM { Name = "项目3", Description = "这是第三个项目" },
                new ItemVM { Name = "项目4", Description = "这是第四个项目" }
            };


        public ICommand DeleteCommand { get; }
        public ICommand ModifyCommand { get; }

        public MainViewModel()
        {
            DeleteCommand = new RelayCommand<ItemVM>(DeleteItem);
            ModifyCommand = new RelayCommand<ItemVM>(ModifyItem);
        }

        private void DeleteItem(ItemVM item)
        {
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        private void ModifyItem(ItemVM item)
        {
            if (item != null)
            {
                // 这里可以打开编辑窗口或直接编辑
                item.Name += " (已修改)";
                item.Description += " - 修改时间: " + DateTime.Now.ToString("HH:mm:ss");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // RelayCommand 实现
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute == null || parameter == null || _canExecute((T)parameter);

        public void Execute(object? parameter)
        {
            if (parameter is T t)
            {
                _execute(t);
            }
            else if (parameter == null && default(T) == null)
            {
                // T is a reference type and parameter is null
                _execute((T?)parameter!);
            }
            // 如果 T 是值类型且 parameter 为 null，则不执行
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
    public partial class CustomListBox : Window
    {
        public CustomListBox()
        {
            InitializeComponent();
        }
    }
}
