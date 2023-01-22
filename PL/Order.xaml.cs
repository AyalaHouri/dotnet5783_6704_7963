using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Order.xaml
    /// </summary>

    public partial class Order : Window
    {
        private BlApi.IBl _bl = BlApi.Factory.Get();
        BO.Order order { get; set; }
        public Order(int id, bool p, bool t)
        {
            InitializeComponent();
            order = _bl.Order.OrderDetail(id);
            Backtotrack.Visibility = Visibility.Hidden;
            if (order.DeliveryDate != null)
            {              
                Deliverd.Visibility = Visibility.Hidden;
                Shipped.Visibility = Visibility.Hidden;
            }
            if (order.ShipDate != null && order.DeliveryDate == null)
            {
                Shipped.Visibility = Visibility.Hidden;
            }
            if (p)
            {
                Backtotrack.Visibility = Visibility.Visible;
                back.Visibility = Visibility.Hidden;
                Deliverd.Visibility = Visibility.Hidden;
                Shipped.Visibility = Visibility.Hidden;
            }
            DataContext = order;
            itemsview.ItemsSource = order.Items;
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

        private void Deliverd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _bl.Order.UpdateStock(order.ID);
                int id = order.ID;
                order = _bl.Order.OrderDetail(id);
                Deliverd.IsEnabled = false;
                DataContext = order;
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
        }

        private void Shipped_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _bl.Order.ordershipdateupdate(order.ID);
                int id = order.ID;
                order = _bl.Order.OrderDetail(id);
                Shipped.IsEnabled = false;
                Deliverd.IsEnabled = true;
                DataContext = order;
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
        }

        private void Backtotrack_Click(object sender, RoutedEventArgs e)
        {
            new trackingorder().Show();
            Close();
        }
        private void Dencrease(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (sender as Button);
                ///BO.ProductForList p = (sender as Button).DataContext as BO.ProductForList;
                order = _bl.Order.UpDate(order.ID, (int)button.Tag, -1);
                itemsview.ItemsSource = order.Items;
                itemsview.Items.Refresh();
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }

        }

        private void Increase(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var updatedOrder = _bl.Order.UpDate(order.ID, (int)button.Tag, 1);
            order= updatedOrder;
            itemsview.ItemsSource = order.Items;
            itemsview.Items.Refresh();
        }
    }
}
