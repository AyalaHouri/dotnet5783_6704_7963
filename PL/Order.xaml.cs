using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Order.xaml
    /// </summary>
    public partial class Order : Window
    {

        private BlApi.IBl _bl = BlApi.Factory.Get();
        BO.Order order;
        public Order(int id,bool p)
        {
            InitializeComponent();
             order= _bl.Order.OrderDetail(id);
            if (p)
            {
                back.Visibility = Visibility.Hidden;
                UpdateStock.Visibility = Visibility.Hidden;

            }
            DataContext= order;

            //idla.Content = order.ID.ToString();
            //cnla.Content = order.CustomerName.ToString();
            //cala.Content = order.CustomerAddress.ToString();
            //cela.Content = order.CustomerEmail.ToString();
            //odla.Content = order.OrderDate.ToString();
            //sdla.Content = order.ShipDate.ToString();
            //ddla.Content = order.DeliveryDate.ToString();
            //statusla.Content = order.ShipDate.ToString();
            //totalla.Content = order.TotalPrice.ToString();
            items.ItemsSource = order.Items;
        }

        private void mainwindow_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new ProductForLIST().Show();
            Close();
        }

        private void UpdateStock_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _bl.Order.UpdateStock(order.ID);
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
           

        }
    }
}
