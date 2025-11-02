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
    /// TriggerEventWindowDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CalculatorMainWindow : Window
    {
        public CalculatorMainWindow()
        {
            InitializeComponent();
        }
        private double _previousValue = 0;
        private string _operation = "";
        private string _currentInput = "";

        // Handle number and digit buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // Append the clicked button's value to the current input string
                _currentInput += button.Content.ToString();
                txtDisplay.Text += _currentInput;
            }
        }

        // Handle operation buttons (+, -, *, /)
        private void Button_Operation(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // 如果有当前输入，更新数值
                if (!string.IsNullOrEmpty(_currentInput))
                {
                    double currentValue = double.Parse(_currentInput);

                    // 如果是第一次操作，直接设置 previousValue
                    if (string.IsNullOrEmpty(_operation))
                    {
                        _previousValue = currentValue;
                    }
                    else
                    {
                        // 如果有之前的操作，先计算结果
                        _previousValue = PerformOperation(_previousValue, currentValue, _operation);
                    }
                }

                _operation = button.Content.ToString() ?? "";
                _currentInput = "";
                txtDisplay.Text += _operation;
            }
        }

        // Handle equal button to calculate the result
        private void Button_Equal(object sender, RoutedEventArgs e)
        {
            if ((!string.IsNullOrEmpty(_operation)) && !string.IsNullOrEmpty(_currentInput))
            {
                double currentValue = double.Parse(_currentInput);
                _previousValue = PerformOperation(_previousValue, currentValue, _operation);
                txtDisplay.Text += _previousValue.ToString();

                _operation = "";
                _currentInput = _previousValue.ToString();
            }
            else if (!string.IsNullOrEmpty(_operation))
            {
                txtDisplay.Text += _previousValue.ToString();

                _operation = "";
                _currentInput = _previousValue.ToString();
            }
        }

        // Perform the operation
        private double PerformOperation(double previousValue, double currentValue, string operation)
        {
            switch (operation)
            {
                case "+":
                    return previousValue + currentValue;
                case "-":
                    return previousValue - currentValue;
                case "*":
                    return previousValue * currentValue;
                case "/":
                    if (currentValue == 0)
                    {
                        MessageBox.Show("Cannot divide by zero!");
                        return previousValue; // 返回原值而不是0
                    }
                    return previousValue / currentValue;
                default:
                    return currentValue;
            }
        }

        // Handle clear button
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            _previousValue = 0;
            _operation = "";
            _currentInput = "";
            txtDisplay.Text = "";
        }
    }
}
