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
    /// Interaction logic for Katalog.xaml
    /// </summary>
    public partial class Katalog : Window
    {
        private BlApi.IBl _bl = BlApi.Factory.Get();
       static BO.Cart cart;
        public Katalog()
        {
            InitializeComponent();
            productlist.ItemsSource = _bl?.Product.AskForPrudactList();
            comboboxcategory.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
        }
        public Katalog(BO.Cart dcart)
        {
            InitializeComponent();
            productlist.ItemsSource = _bl?.Product.AskForPrudactList();
            comboboxcategory.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
            cart = dcart;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void comboboxcategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            productlist.ItemsSource = _bl?.Product.GetProducts(a => a?.Category.ToString() == comboboxcategory.SelectedItem.ToString());

        }

        private void productlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)productlist.SelectedItem;
            new Product(product,cart).Show();
            Close();
        }

        private void cartshow_Click(object sender, RoutedEventArgs e)
        {
            new Cart(cart).Show();
            Close();
        }



        //private void backbutton_Click(object sender, RoutedEventArgs e)
        //{
        //    new MainWindow().Show();
        //    Close();
        //}


    }
}
