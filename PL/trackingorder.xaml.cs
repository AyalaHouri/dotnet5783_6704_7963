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
        public static int x;
        bool b;
        private BlApi.IBl _bl = BlApi.Factory.Get();
        public trackingorder(int y)
        {

            InitializeComponent();
            ordershow.Visibility = Visibility.Visible;
            backtosimolator.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Hidden;
            try
            {
                BO.OrderTracking ordertrack = _bl.Order.OrderTrackingFunc(y);
                stackPanel.Visibility = Visibility.Visible;
                stackPanel.DataContext = ordertrack;

            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
                new trackingorder().Show();
                Close();
            }
            x = y;
            tracktheid.Visibility = Visibility.Hidden;
            enterid.Visibility = Visibility.Hidden;
            TextID.Visibility = Visibility.Hidden;
            b = false;


        }
        public trackingorder()
        {
            InitializeComponent();
            stackPanel.Visibility = Visibility.Hidden;
            ordershow.Visibility = Visibility.Hidden;
            backtosimolator.Visibility = Visibility.Hidden;
            Dorder.Visibility = Visibility.Hidden;
            BackToM.Visibility = Visibility.Hidden;
            b = true;
        }

        public trackingorder(bool x)
        {
            InitializeComponent();
            stackPanel.Visibility = Visibility.Hidden;
            ordershow.Visibility = Visibility.Hidden;
            backtosimolator.Visibility = Visibility.Hidden;
            tracktheid.Visibility= Visibility.Hidden;
            back.Visibility = Visibility.Hidden;
        }
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
            TextID.Visibility = Visibility.Hidden;



        }

        private void ordershow_Click(object sender, RoutedEventArgs e)
        {
            new Order(x, b, b).Show();
            Close();

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void backtosimolator_Click(object sender, RoutedEventArgs e)
        {
            new sinulator().Show();
        }

        private void Dorder_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(enterid.Text, out int IDt);
            try
            {
                _bl.Order.Deleteorder(_bl.Order.OrderDetail(IDt));
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
            }
            new ProductForLIST().Show();
            Close();
        }

        private void BackToM_Click(object sender, RoutedEventArgs e)
        {
            new ProductForLIST().Show();
            Close();
        }
    }
}
