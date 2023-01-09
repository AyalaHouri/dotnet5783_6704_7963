

using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private BlApi.IBl bl = BlApi.Factory.Get();
        private void showproductlist_Click(object sender, RoutedEventArgs e)
        {
            new ProductForLIST().Show();
            Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addorder_Click(object sender, RoutedEventArgs e)
        {
            new Katalog().Show();
            Close();
        }

        private void trackorder_Click(object sender, RoutedEventArgs e)
        {
            new trackingorder().Show();
            Close();
        }
    } 
}
