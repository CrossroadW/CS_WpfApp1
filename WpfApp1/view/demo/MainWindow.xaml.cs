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

namespace WpfApp1.view.demo
{

    public class Person
    {
        public required string Name
        {
            get;
            set;
        }
        public required int Age
        {
            get;
            set;
        }
    }
    public  class NotiyBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class PersonViewModel: NotiyBase
    {
        private Person _person;
        private ICommand? _updateCommand;
        public PersonViewModel(Person p)
        {
            _person = p;
        }
        public string Name
        {
            get
            {
                return _person.Name;
            }
            set
            {
                if (_person.Name != value)
                {
                    _person.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Age
        {
            get
            {
                return _person.Age;
            }
            set
            {
                if (_person.Age != value)
                {
                    _person.Age = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                _updateCommand ??= new RelayCommand(param => {
                    Name = "Updated Name";
                    Age = 30;
                }, param => true);
                return _updateCommand;
            }
        }
    }
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> execute;
        private readonly Func<object, bool> canExecute;
        public RelayCommand(Action<object?> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || parameter == null || canExecute(parameter);
        }
        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var p = new Person
            {
                Age = 42,
                Name = "lisi"
            };
            DataContext = new PersonViewModel(p);
        }
    }
}