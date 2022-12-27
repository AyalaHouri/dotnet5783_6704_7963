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
        public ProductForLIST()
        {
            InitializeComponent();
            ProductList.ItemsSource = _bl?.Product.AskForPrudactList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
            
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int? x = null;
            new Product(x).Show();
            Close();
            //if(new Product().ShowDialog()==false)
            //    ProductList.ItemsSource = _bl.Product.AskForPrudactList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new MainWindow().ShowDialog();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void ProductList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = (BO.ProductForList)ProductList.SelectedItem;
            new Product(product.ID).Show();
            Close();
        }
    }
}
