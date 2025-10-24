using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class DummyItem
    {
        // 这个类什么都不用写，只是为了生成一行数据
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var items = new List<DummyItem>();
            for (int i = 0; i < 5; i++)
            {
                items.Add(new DummyItem());
            }
            var data = (DataGrid)FindName("myDataGrid");
            data.ItemsSource = items;
        }
    }
}