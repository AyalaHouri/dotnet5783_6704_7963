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

namespace PL
{
    /// <summary>
    /// Interaction logic for trackingorder.xaml
    /// </summary>
    public partial class trackingorder : Window
    {
        private BlApi.IBl _bl = BlApi.Factory.Get();
        public trackingorder()
        {
            InitializeComponent();
            stackPanel.Visibility = Visibility.Hidden;
            ordershow.Visibility = Visibility.Hidden;
        }
        public static int x;

        private void tracktheid_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(enterid.Text, out int IDt);
            ordershow.Visibility = Visibility.Visible;
            try
            {
                BO.OrderTracking ordertrack = _bl.Order.OrderTrackingFunc(IDt);
                stackPanel.Visibility = Visibility.Visible;
                stackPanel.DataContext = ordertrack;

            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
                new trackingorder().Show();
                Close();
            }
            x = IDt;
            tracktheid.Visibility = Visibility.Hidden;
            enterid.Visibility = Visibility.Hidden;
            LABELID.Visibility = Visibility.Hidden;



        }

        private void ordershow_Click(object sender, RoutedEventArgs e)
        {
            new Order(x, true).Show();
            Close();

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
