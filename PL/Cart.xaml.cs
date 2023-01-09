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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        private BlApi.IBl _bl = BlApi.Factory.Get();
        static BO.Cart? cartpl;
        public Cart(BO.Cart cart)
        {
            InitializeComponent();
            namebox.Visibility = Visibility.Hidden;
            namelabl.Visibility = Visibility.Hidden;
            adresbox.Visibility = Visibility.Hidden;
            adresslab.Visibility = Visibility.Hidden;
            emailbox.Visibility = Visibility.Hidden;
            emailab.Visibility = Visibility.Hidden;
            finish.Visibility = Visibility.Hidden;
            try
            {
                if (cart != null)
                {
                    cartview.ItemsSource = cart.Items;
                    empty.Visibility = Visibility.Hidden;
                    cartpl = cart;
                }
                else
                {
                    cartview.Visibility = Visibility.Hidden;

                }
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
            }

           

        }

        private void cartview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void aproveorder_Click(object sender, RoutedEventArgs e)
        {
            cartlabel.Content = "aprove order";
            cartview.Visibility = Visibility.Hidden;
            finish.Visibility = Visibility.Hidden;
            namebox.Visibility = Visibility.Visible;
            namelabl.Visibility = Visibility.Visible;
            adresbox.Visibility = Visibility.Visible;
            adresslab.Visibility = Visibility.Visible;
            emailbox.Visibility = Visibility.Visible;
            emailab.Visibility = Visibility.Visible;
            cartpl.CustomerName = namebox.Text;
            cartpl.CustomerEmail = emailbox.Text;
            cartpl.CustomerAddress = adresbox.Text;
            aproveorder.Visibility = Visibility.Hidden;
            finish.Visibility = Visibility.Visible;
        }

        private void finish_Click(object sender, RoutedEventArgs e)
        {
            aproveorder.Visibility = Visibility.Hidden;
          //  DataContext=
            try { _bl.Cart.AproveOrder(cartpl); }

            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
                aproveorder_Click(sender, e);
            }
            new MainWindow().Show();
            Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Katalog().Show();
            Close();
        }
    }
}
