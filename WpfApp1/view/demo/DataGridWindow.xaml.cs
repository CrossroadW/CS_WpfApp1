using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace WpfApp1.view.datagrid
{
    public class Record : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string prop)
                 => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        private string _name = string.Empty;
        private string _classname = string.Empty;
        private string _address = string.Empty;
        private bool _isEditing = false;
        public bool IsEditing
        {
            get => _isEditing; set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }
        public Record Clone() => new Record
        {
            UserName = this.UserName,
            ClassName = this.ClassName,
            Address = this.Address
        };

        public string UserName
        {
            get => _name; set
            {
                _name = value; OnPropertyChanged(nameof(UserName));
            }
        }
        public string ClassName
        {
            get => _classname; set
            {
                _classname = value; OnPropertyChanged(nameof(ClassName));
            }

        }
        public string Address
        {
            get => _address; set
            {
                _address = value; OnPropertyChanged(nameof(Address));
            }
        }
    }
    public class RecordVM
    {
        public ObservableCollection<Record> Records { get; } = new ObservableCollection<Record>
        {
             new Record { UserName = "张三", ClassName = "一班", Address = "北京市" },
             new Record { UserName = "李四", ClassName = "二班", Address = "上海市" } ,
             new Record { UserName = "王五", ClassName = "三班", Address = "广州市" } 
        };
        private readonly Dictionary<Record, Record> _backup = new Dictionary<Record, Record>();

        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public RecordVM()
        {

            EditCommand = new RelayCommand<Record>(OnEdit);
            SaveCommand = new RelayCommand<Record>(OnSave);
            CancelCommand = new RelayCommand<Record>(OnCancel);
        }

        private void OnEdit(Record rec)
        {
            if (rec == null) return;
            // 若已有其他行在编辑，可选择先结束/阻止；这里允许多行编辑或你可自行限制
            if (!_backup.ContainsKey(rec))
                _backup[rec] = rec.Clone();

            rec.IsEditing = true;
        }

        private void OnSave(Record rec)
        {
            if (rec == null) return;
            // 在真实场景，这里同步到数据库或 API
            if (_backup.ContainsKey(rec))
                _backup.Remove(rec);

            rec.IsEditing = false;
        }

        private void OnCancel(Record rec)
        {
            if (rec == null) return;
            if (_backup.TryGetValue(rec, out var old))
            {
                // 恢复备份值
                rec.UserName = old.UserName;
                rec.ClassName = old.ClassName;
                rec.Address = old.Address;
                _backup.Remove(rec);
            }
            rec.IsEditing = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public partial class DataGridWindow : Window
    {
        public DataGridWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var vm = this.Resources["recordVM"] as RecordVM;
            if (vm != null)
            {
                var record = vm.Records.ElementAt(vm.Records.Count - 1);
                record.UserName += "+update";
                vm.Records.Add(new Record
                {
                    UserName = $"cnt: {vm.Records.Count}",
                    ClassName = "22",
                    Address = "hello"
                });
            }
        }
            }
    #region
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;
        public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute((T)parameter!);
        public void Execute(object? parameter) => _execute((T)parameter!);
        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
    #endregion
    public class BoolToVisibilityConverter : IValueConverter
    {
    
        public bool Inverse { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool b) flag = b;

            if (parameter is string s && s.Equals("invert", StringComparison.OrdinalIgnoreCase))
                flag = !flag;

            if (Inverse) flag = !flag;

            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
            {
                var visible = v == Visibility.Visible;
                if (parameter is string s && s.Equals("invert", StringComparison.OrdinalIgnoreCase))
                    visible = !visible;
                return Inverse ? !visible : visible;
            }
            return false;
        }
    }
}
