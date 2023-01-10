using BlApi;
using BO;
using static BO.Enum;
using static DO.Enums;

partial class Program
{
    static void order(ref IBl bl)///if you want to get detail about order
    {
        Console.WriteLine("Order:\n ");
        Console.WriteLine($"a In order to get the order list\n" +
             "b In order to get the order item by ID\n" +
             "c In order to update the shipping of the order\n" +
             "d In order to update the order delivery\n" +
             "e In order to order track the order\n" +
             "For exit, press 0.\n");
        bool isRead = char.TryParse(Console.ReadLine(), out char choice);
        int idtemp;
        char zero = '0';
        BO.Order order = new BO.Order();
        try
        {
            while (choice != zero)
            {
                switch (choice)
                {
                    case 'a':
                        {
                            IEnumerable<BO.OrderForList> lorderforlist = bl.Order.AskList();

                            foreach (var orderforlist in lorderforlist)
                                Console.WriteLine(orderforlist);
                            
                            break;
                        }
                    case 'b':
                        Console.WriteLine("Enter the order ID\n");
                        int.TryParse(Console.ReadLine(), out idtemp);
                        order = bl.Order.OrderDetail(idtemp);
                        Console.WriteLine(order);
                        break;
                    case 'c':
                        Console.WriteLine("Enter the order ID\n");
                        int.TryParse(Console.ReadLine(), out idtemp);
                        Console.Write(bl.Order.ordershipdateupdate(idtemp));
                        break;
                    case 'd':
                        Console.WriteLine("Enter the order ID\n");
                        int.TryParse(Console.ReadLine(), out idtemp);
                        Console.Write(bl.Order.UpdateStock(idtemp));
                        break;
                    case 'e':
                        Console.WriteLine("Enter the order ID\n");
                        int.TryParse(Console.ReadLine(), out idtemp);
                        bl.Order.OrderTrackingFunc(idtemp);
                        break;
                    default:
                        throw new Exception("error");
                }
                Console.WriteLine("Order:\n ");
                Console.WriteLine($"a In order to get the order list\n" +
                     "b In order to get the order item by ID\n" +
                     "c In order to update the shipping of the order\n" +
                     "d In order to update the order delivery\n" +
                     "e In order to order track the order\n" +
                     "For exit, press 0.\n");
                char.TryParse(Console.ReadLine(), out choice);
            }
        }
        catch (Exception messege)
        {
            Console.WriteLine(messege.Message);
        }
    }

static void product(ref IBl bl, BO.Cart tempcart)///if you want to get detail about product
    {
        Console.WriteLine("are you tha maneger?\n" +
            "press y or n\n");
        char.TryParse(Console.ReadLine(), out char option);
        char zero = '0';
        if (option == 'n')
        {
            

            Console.WriteLine($"Click a to get the product list\n" +
                $" Click b to get product details\n" +
                $"Click 0 to exite\n");
            char.TryParse(Console.ReadLine(), out char choice);
            while (choice != zero)
            {
                switch (choice)
                {
                    case 'a':
                        IEnumerable<BO.ProductForList> lproductforlist = bl.Product.AskForPrudactList();
                        foreach (var productforlist in lproductforlist)
                        {
                            Console.WriteLine(productforlist);
                        }
                        break;
                    case 'b':
                        //Console.WriteLine("enter the product id:\n");
                        //int.TryParse(Console.ReadLine(), out int productid);
                        //Console.WriteLine($"{bl.Product.prudactrequest(productid)}");
                        Console.WriteLine("enter the product id\n");
                        int.TryParse(Console.ReadLine(), out int productid);

                        Console.WriteLine($"{bl.Product.askedProductItem(productid, tempcart)}");
                        
                        break;
                    default:
                        throw new Exception("error\n");
                }
                Console.WriteLine($"Click a to get the product list\n" +
                $" Click b to get product details\n" +
                $"Click 0 to exite\n");
                char.TryParse(Console.ReadLine(), out choice);
            }
        }
        else
        {
            if (option == 'y')
            {
                Console.WriteLine("Click a to get the list of products\n" +
                    " Click b to get product details\n" +
                    " Click c to add a product\n" +
                    " Click d to delete a product\n" +
                    " Click e to update product details\n" +
                    "Click 0 for exite\n");
                char.TryParse(Console.ReadLine(), out char choice);
                while (choice != zero)
                {
                    switch (choice)
                    {
                        case 'a':

                            IEnumerable<BO.ProductForList> lproductforlist = bl.Product.AskForPrudactList();
                            foreach (var productforlist in lproductforlist)
                            {
                                Console.WriteLine(productforlist);
                            }
                                break;
                        case 'b':
                            Console.WriteLine("enter the product id:\n");
                            int.TryParse(Console.ReadLine(), out int productid);
                            Console.WriteLine($"{bl.Product.prudactrequest(productid)}");
                            break;
                        case 'c':
                            BO.Product prodect = new BO.Product();
                            Console.WriteLine("Enter  name of product:" + "\n");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter   price:\n");
                            int pri;
                            int.TryParse(Console.ReadLine(), out pri);
                            Console.WriteLine("Enter amount in Stoke:\n");
                            int instoke;
                            int.TryParse(Console.ReadLine(), out instoke);
                            category categorytemp;
                            Console.WriteLine("Enter category:\n");
                            category.TryParse(Console.ReadLine(), out categorytemp);
                            prodect.NameOfProduct = name;
                            prodect.Price = pri;
                            prodect.InStoke = instoke;
                            prodect.Category = categorytemp;
                            prodect.ID = BlImplementation.Config.get_ID_Product;
                            bl.Product.AddProduct(prodect);
                            Console.WriteLine("the product added\n");
                            break;
                        case 'd':
                            Console.WriteLine("enter the product id\n");
                            int.TryParse(Console.ReadLine(), out int productID);
                            bl.Product.DeleteProduct(productID);
                            Console.WriteLine("the product deleted\n");
                            break;
                        case 'e':
                            BO.Product pro = new BO.Product();
                            Console.WriteLine("Enter  the product ID" + "\n");
                            int id;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter  name of product" + "\n");
                            string Name = Console.ReadLine();
                            Console.WriteLine("Enter   price\n");
                            int price;
                            int.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter   amount in Stoke\n");
                            int Instoke;
                            int.TryParse(Console.ReadLine(), out Instoke);
                            Category Categorytemp;
                            Console.WriteLine("Enter   category\n");
                            category.TryParse(Console.ReadLine(), out Categorytemp);
                            pro.ID = id;
                            pro.NameOfProduct = Name;
                            pro.Price = price;
                            pro.InStoke = Instoke;
                            pro.Category = (category?)Categorytemp;
                            bl.Product.updateProduct(pro);
                            Console.WriteLine("the product updated\n");
                            break;
                        default:
                            throw new Exception("error\n");
                    }
                    Console.WriteLine("Click a to get the list of products\n" +
                   " Click b to get product details\n" +
                   " Click c to add a product\n" +
                   " Click d to delete a product\n" +
                   " Click e to update product details\n" +
                   "Click 0 for exite\n");

                    char.TryParse(Console.ReadLine(), out choice);
                }

            }
            else
            {
                throw new Exception("error");
            }
        }


    }
    static BO.Cart cart(ref IBl bl, BO.Cart tempcart)///if you want to get detail about cart
    {

        Console.WriteLine($"a In order to add a product to the cart\n" +
              "b In order to update amount of product in the cart\n" +
              "c In order to aprove the order\n" +
              "d In order to delet all the cart\n" +
              "For exit, press 0.\n");
        bool isRead = char.TryParse(Console.ReadLine(), out char choice);
        
        char zero = '0';
        try
        {
            while (choice != zero)
            {
                switch (choice)
                {
                    case 'a':
                        Console.WriteLine("enter product id");
                        int.TryParse(Console.ReadLine(), out int producti);
                        tempcart=bl.Cart.AddToCart(tempcart, producti);
                        Console.WriteLine("Product added successfully");
                        break;
                    case 'b':
                        Console.WriteLine("enter product id");
                        int.TryParse(Console.ReadLine(), out int productid);
                        Console.WriteLine("enter the Amount do you want");
                        int.TryParse(Console.ReadLine(), out int newAmount);
                        tempcart=bl.Cart.UpdateAmount(tempcart, productid, newAmount);
                        Console.WriteLine("the amount updated");
                        break;
                    case 'c':
                        Console.WriteLine("Enter your name\n");
                        tempcart.CustomerName = Console.ReadLine();
                        Console.WriteLine("Enter your Adress\n");
                        tempcart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("Enter your Email\n");
                        tempcart.CustomerEmail = Console.ReadLine();
                        bl.Cart.AproveOrder(tempcart);
                        Console.WriteLine("your order aproved:)");
                        break;
                    case 'd':
                        bl.Cart.DeletAll(tempcart);
                        Console.WriteLine("your cart is empty");
                        break;
                    default:
                        throw new Exception("error");
                }
                Console.WriteLine($"a In order to add a product to the cart\n" +
              "b In order to update amount of product in the cart\n" +
              "c In order to aprove the order\n" +
              "d In order to delet all the cart\n" +
              "For exit, press 0.");
                char.TryParse(Console.ReadLine(), out choice);
            }
            
        }
        catch (Exception messege)
        {
            Console.WriteLine(messege.Message);
        }
        return tempcart;
    }



    static void Main(string[] arr)//main function
    {
       BlApi.IBl bl = BlApi.Factory.Get();
    Console.WriteLine("press a for order\n" +
            "b for product\n" +
            "c for cart\n" +
            "press 0 for exsit\n");
        BO.Cart tempcart = new BO.Cart();
        tempcart.Items = new List<BO.OrderItem>();
        bool isRead = char.TryParse(Console.ReadLine(), out char choice);
        char zero = '0';
        try
        {
            while (choice != zero)
            {
                switch (choice)
                {
                    case 'a':
                        order(ref bl);
                        break;
                    case 'b':
                        product(ref bl,tempcart);
                        break;
                    case 'c':
                        cart(ref bl, tempcart);
                        break;


                    default:
                        throw new Exception("error");
                }
                Console.WriteLine("press a for order\n" +
            "b for product\n" +
            "c for cart\n" +
            "press 0 for exsit\n");
                char.TryParse(Console.ReadLine(), out choice);
            }
               
        }
        catch (Exception messege)
        {
            Console.WriteLine(messege.Message);
        }

    }
}

