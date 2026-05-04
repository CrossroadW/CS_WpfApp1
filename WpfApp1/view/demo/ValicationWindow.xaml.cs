using System;
using System.Collections.Generic;
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

namespace WpfApp1.view.demo
{
    public class PositiveIntegerRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return new ValidationResult(false, "不能为空");

            if (!int.TryParse(value.ToString(), out var number))
                return new ValidationResult(false, "必须是整数");

            if (number <= 0)
                return new ValidationResult(false, "必须大于 0");

            return ValidationResult.ValidResult;
        }
    }
    public class ValicationVM : INotifyPropertyChanged
    {
        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
            }
        }

         public event PropertyChangedEventHandler? PropertyChanged;
    }
    public partial class ValicationWindow : Window
    {
        public ValicationWindow()
        {
            InitializeComponent();
        }
    }
}
