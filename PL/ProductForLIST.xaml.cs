using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace PL
{
    /// <summary>
    /// Interaction logic for ProductForLIST.xaml
    /// </summary>
    public partial class ProductForLIST : Window
    {
        private BlApi.IBl _bl = BlApi.Factory.Get();
        static int x ;
        public ProductForLIST()
        {
            InitializeComponent();
            ProductList.ItemsSource = _bl?.Product.AskForPrudactList();
            orderlist.ItemsSource = _bl?.Order.AskList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
            orderlist.Visibility = Visibility.Hidden;
        }
     
        

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ProductList.ItemsSource = _bl?.Product.GetProducts(a => a?.Category.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void CategorySelector_SizeChanged(object sender, SizeChangedEventArgs e)
        {

          //CategorySelector.ItemsSource = _bl.Product.AskForCategory();
        }

       

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    new MainWindow().ShowDialog();
        //    Close();
        //}

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void ProductList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = _bl.Product.prudactrequest(((BO.ProductForList)ProductList.SelectedItem).ID);
            new Product(product).Show();
            Close();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            orderlist.Visibility = Visibility.Hidden;
           ProductList.Visibility = Visibility.Visible;
            CategorySelector.Visibility = Visibility.Visible;
        }

        private void orderbutton_Click(object sender, RoutedEventArgs e)
        {
            ProductList.Visibility = Visibility.Hidden;
            orderlist.Visibility = Visibility.Visible;
            CategorySelector.Visibility = Visibility.Hidden;
            
        }

        private void addproduct_Click(object sender, RoutedEventArgs e)
        {
            new Product().Show();
            Close();

        }

        private void orderlist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var order = (BO.OrderForList)orderlist.SelectedItem;
            try
            {
                new Order(order.ID, false).Show();
                Close();
            }
           
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void orderlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
