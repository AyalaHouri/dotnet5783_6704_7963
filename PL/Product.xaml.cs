﻿using Microsoft.VisualBasic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window, INotifyPropertyChanged
    {
        private BlApi.IBl _bl = BlApi.Factory.Get();
        public event PropertyChangedEventHandler PropertyChanged;/// <summary>
        BO.Cart caart;                                                      /// we add inotify-propertychange
                                                                            /// 
        public Product(BO.ProductForList prod, BO.Cart cart)///katalog
        {
            InitializeComponent();
            cartback.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Hidden;
            updatebutton.Visibility = Visibility.Hidden;
            addbutton.Visibility = Visibility.Hidden;
            TextBoxLable.Content = "product:";
            IDtb.Visibility = Visibility.Hidden;
            Categorycb.Visibility = Visibility.Hidden;
            Nametb.Visibility = Visibility.Hidden;
            Pricetb.Visibility = Visibility.Hidden;
            Amount.Visibility = Visibility.Hidden;
            amountlabel.Visibility = Visibility.Hidden;
            labelcategory.Content = prod.Category.ToString();
            labelname.Content = prod.NameOfProduct.ToString();
            labelprice.Content = prod.PricePerProduct.ToString();
            labelid.Content = prod.ID.ToString();
            cartt = cart;
            x = prod.ID;
            caart = cart;


        }

        public Product(BO.Product product)///product for list
        {
            InitializeComponent();
            IDtb.IsEnabled = false;
            cartback.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Visible;
            Categorycb.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
            //IDtb.Text = ProductObservableCollection[0].ID.ToString();
            DataContext = product;
            labelcategory.Visibility = Visibility.Hidden;
            labelname.Visibility = Visibility.Hidden;
            labelprice.Visibility = Visibility.Hidden;
            labelid.Visibility = Visibility.Hidden;
            addtocart.Visibility = Visibility.Hidden;
            addbutton.Visibility = Visibility.Hidden;
            TextBoxLable.Content = "Update Product:";
            Dproduct.Visibility = Visibility.Hidden;
        }
        public Product()///product for list
        {
            InitializeComponent();
            Categorycb.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
            labelcategory.Visibility = Visibility.Hidden;
            labelname.Visibility = Visibility.Hidden;
            labelprice.Visibility = Visibility.Hidden;
            labelid.Visibility = Visibility.Hidden;
            addtocart.Visibility = Visibility.Hidden;
            updatebutton.Visibility = Visibility.Hidden;
            TextBoxLable.Content = "Add Product:";
        }
        public Product(bool t)
        {
            InitializeComponent();
            cartback.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Visible;
            Categorycb.ItemsSource = Enum.GetValues(typeof(BO.Enum.category));
            labelcategory.Visibility = Visibility.Hidden;
            labelname.Visibility = Visibility.Hidden;
            labelprice.Visibility = Visibility.Hidden;
            labelid.Visibility = Visibility.Hidden;
            addtocart.Visibility = Visibility.Hidden;
            addbutton.Visibility = Visibility.Hidden;
            updatebutton.Visibility = Visibility.Hidden;
            Amount.Visibility = Visibility.Hidden;
            Pricetb.Visibility = Visibility.Hidden;
            Nametb.Visibility= Visibility.Hidden;
            Categorycb.Visibility = Visibility.Hidden;
            amountlabel.Visibility = Visibility.Hidden;
            Lprice.Visibility = Visibility.Hidden;
            Lcategory.Visibility = Visibility.Hidden;
            Lname.Visibility = Visibility.Hidden;
            TextBoxLable.Content = "Delete Product:";
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));///to refrash this window
        }

        private ObservableCollection<BO.Product> _ProductObservableCollection;/// <summary>
                                                                              /// observes colection we save the data
        public ObservableCollection<BO.Product> ProductObservableCollection///to protect the data
        {
            get { return _ProductObservableCollection; }
            set
            {
                _ProductObservableCollection = value;
                OnPropertyChanged(nameof(ProductObservableCollection));
            }
        }
        static BO.Cart? cartt;
        static int x;



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new BO.Product();
            IDtb.IsEnabled = false;
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
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            catch (DO.MyException ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
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
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            catch (DO.MyException ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            new ProductForLIST().Show();
            Close();
        }

        private void addtocart_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                cartt = _bl.Cart.AddToCart(cartt, x);
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            new Katalog(cartt).Show();
            Close();
        }

        private void cartback_Click(object sender, RoutedEventArgs e)
        {
            new ProductForLIST().Show();
            Close();
        }

        private void Dproduct_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(IDtb.Text, out int id);
            try
            {
                _bl.Product.DeleteProduct(id);
            }
            catch (BO.ExceptionLogi ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            catch (DO.MyException ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            new ProductForLIST().Show();
            Close();
        }
    }
}