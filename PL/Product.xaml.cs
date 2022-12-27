using System;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        public Product(int? id)
        {
            InitializeComponent();
            Categorycb.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
            IDtb.Text = id.ToString();
            if (id == null)
            {
                updatebutton.Visibility = Visibility.Hidden;
                TextBoxLable.Content = "add product:";
            }
            else
            {
                addbutton.Visibility = Visibility.Hidden;
                TextBoxLable.Content = "Update product:";
            }


        }
        private BlApi.IBl _bl = BlApi.Factory.Get();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product= new BO.Product();
            int.TryParse(IDtb.Text, out int id);
            product.ID = id;
            product.NameOfProduct = Nametb.Text;
            int.TryParse(Pricetb.Text, out int price);
            product.Price = price;
            int.TryParse(Amount.Text, out int amount);
            product.InStoke = amount;
            product.Category = (BO.Enum.category?)Categorycb.SelectedItem;
   
            
                try
                {
                
                _bl.Product.AddProduct(product);
                }
                catch(BO.ExceptionLogi ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (DO.MyException ex)
                {
                    MessageBox.Show(ex.Message);
                }




            new ProductForLIST().Show();
            Close();
            
        }

       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ProductForLIST().Show();
            Close();
        }

        private void Categorycb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void updatebutton_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            int.TryParse(IDtb.Text, out int id);
            product.ID = id;
            product.NameOfProduct = Nametb.Text;
            int.TryParse(Pricetb.Text, out int price);
            product.Price = price;
            int.TryParse(Amount.Text, out int amount);
            product.InStoke = amount;
            product.Category = (BO.Enum.category?)Categorycb.SelectedItem;


            try
            {
                _bl.Product.updateProduct(product);
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (DO.MyException ex)
            {
                MessageBox.Show(ex.Message);
            }
            new ProductForLIST().Show();
            Close();
        }

     
    }
}
