

//using static DO.Enums;
//using DO;
//namespace Dal;
//using DalList;
//partial class Program
//{
//    static void choosenOrder(ref DalOrder dalorder)
//    {
//        Order Torder = new Order();//temp order
//        Console.WriteLine("\nOrder\n" +
//              "a In order to add a object\n" +
//              "b In order to present the object by ID\n" +
//              "c In order to present the object by Index\n" +
//              "d In order to present the list of the entity\n" +
//              "e In order to update detail to the object\n " +
//              "f In order to delete an object\n" +
//              "For exit, enter other letter\n");
//        bool isRead = char.TryParse(Console.ReadLine(), out char option);
//        switch (option)
//        {
//            case 'a':
//                Console.WriteLine("Enter your name, Email and adress");
//                Torder.CustomerName = Console.ReadLine();
//                Torder.CustomerEmail = Console.ReadLine();
//                Torder.CustomerAddress = Console.ReadLine();
//                dalorder.Create(Torder.CustomerName, Torder.CustomerEmail, Torder.CustomerAddress);
//                break;
//            case 'b':
//                Console.WriteLine("Enter the ID:");
//                isRead = int.TryParse(Console.ReadLine(), out int ID);
//                Console.Write(dalorder.ReadID(ID) + "\n");
//                break;
//            case 'c':
//                Console.WriteLine("Enter the index");
//                isRead = int.TryParse(Console.ReadLine(), out int I);
//                Console.Write(dalorder.Read(I) + "\n");
//                break;
//            case 'd':
//                Console.WriteLine("Order:" + "\n");
//                for (int _i = 0; _i < dalorder.Order_Length(); _i++)
//                { 
//                    Console.WriteLine(dalorder.Read(_i));
//                }
//                break;
//            case 'e':
//                Console.WriteLine("Enter the ID of the orter that you want to update");
//                isRead = int.TryParse(Console.ReadLine(), out int IDup);
//                Console.WriteLine("Enter the updates that you want to change");
//                Torder.CustomerName = Console.ReadLine();
//                Torder.CustomerEmail = Console.ReadLine();
//                Torder.CustomerAddress = Console.ReadLine();
//                Order updateObject = new Order(IDup, Torder.CustomerName, Torder.CustomerEmail, Torder.CustomerAddress);
//                dalorder.Update(IDup, updateObject);
//                break;
//            case 'f':
//                Console.WriteLine("Enter the ID of the orter that you want to delete");
//                isRead = int.TryParse(Console.ReadLine(), out int IDdel);
//                dalorder.Delete(IDdel);
//                break;
//            default:
//                Console.WriteLine("Error");
//                break;


//        }
//    }
//        static void choosenOrderItem(ref DalOrderItem dalorderitem)
//        {
//            OrderItem TorderItem = new OrderItem();//temp orderitem
//            Console.WriteLine("\norder itemes\n" +
//                  "a In order to add a object\n" +
//                  "b In order to present the object by ID\n" +
//                  "c In order to present the object by Index\n" +
//                  "d In order to present the list of the  items in the order\n" +
//                  "e In order to present the list of the entity\n" +
//                  "f In order to update detail to the object\n " +
//                  "g In order to delete an object\n" +
//                  "For exit, enter other letter\n");
//            bool isRead = char.TryParse(Console.ReadLine(), out char option);
//            switch (option)
//            {
//                case 'a':
//                    Console.WriteLine("Enter your  Product ID, Order ID,price and amount");
//                int ProductID;
//                int OrderID;
//                double price;
//                int Amount;
//                int.TryParse(Console.ReadLine(), out ProductID);
//                int.TryParse(Console.ReadLine(), out OrderID);
//                double.TryParse(Console.ReadLine(), out price);
//                int.TryParse(Console.ReadLine(), out Amount);
//                dalorderitem.Create(ProductID, OrderID, price, Amount);
//                break;
//                case 'b':
//                    Console.WriteLine("Enter the ID:");
//                    isRead = int.TryParse(Console.ReadLine(), out int ID);
//                    Console.Write(dalorderitem.ReadID(ID)+"\n");
//                    break;
//                case 'c':
//                    Console.WriteLine("Enter the index");
//                    isRead = int.TryParse(Console.ReadLine(), out int I);
//                    Console.Write(dalorderitem.Read(I) + "\n");
//                    break;
//                case 'd':
//                    Console.WriteLine("Enter the order ID" + "\n");
//                    isRead = int.TryParse(Console.ReadLine(), out int orderID);
//                    Console.Write(dalorderitem.ReadItem(orderID));
//                    break;
//                 case 'e':
//                    Console.WriteLine("order itemes:" + "\n");
//                    for (int _i = 0; _i < dalorderitem.orderitemlength(); _i++)
//                    { 
//                       Console.WriteLine(dalorderitem.Read(_i));
//                    }
//                    break;
//                 case 'f':
//                    Console.WriteLine("Enter the ID of the orter that you want to update" + "\n");
//                    isRead = int.TryParse(Console.ReadLine(), out int IDup);
//                    Console.WriteLine("Enter the details of the order itemm that you want to update" + "\n");
//                    TorderItem.ProductID = Console.Read();
//                    TorderItem.OrderID = Console.Read();
//                    TorderItem.Price = Console.Read();
//                    TorderItem.Amount = Console.Read();
//                    dalorderitem.Update(IDup,TorderItem);
//                    break;
//                case 'g':

//                    Console.WriteLine("Enter the ID of the orter that you want to delete" + "\n");
//                    isRead = int.TryParse(Console.ReadLine(), out int IDdel);
//                    dalorderitem.Delete(IDdel);
//                    break;
//            default:
//                    Console.WriteLine("Error");
//                    break;


//            }
//        }
//    static void choosenProduct(ref DalProduct dalproduct)
//    {
//        Product Tproduct = new Product();//temp order
//        Console.WriteLine("\nProduct\n" +
//              "a In order to add a object" + "\n" +
//              "b In order to present the object by ID" + "\n" +
//              "c In order to present the object by Index" + "\n" +
//              "d In order to present the list of the entity" + "\n" +
//              "e In order to update detail to the object " + "\n" +
//              "f In order to delete an object" + "\n" +
//              "For exit, enter other letter" + "\n");
//        bool isRead = char.TryParse(Console.ReadLine(), out char option);
//        switch (option)
//        {
//            case 'a':
//                Console.WriteLine("Enter  name of product, price and inStoke" + "\n");
//                string name= Console.ReadLine();
//                int pri= Console.Read();
//                Category category;
//                Category.TryParse(Console.ReadLine(), out category);
//                int instoke= Console.Read();
//                dalproduct.Create(name, pri, category, instoke);
//                break;
//            case 'b':
//                Console.WriteLine("Enter the ID:" + "\n");
//                isRead = int.TryParse(Console.ReadLine(), out int ID);
//                Console.Write(dalproduct.ReadID(ID) + "\n");
//                break;
//            case 'c':
//                Console.WriteLine("Enter the index" + "\n");
//                isRead = int.TryParse(Console.ReadLine(), out int I);
//                Console.Write(dalproduct.Read(I) + "\n");
//                break;
//            case 'd':
//                Console.WriteLine("Product:" + "\n");
//                for (int _i = 0; _i < dalproduct.ProductLength(); _i++)
//                {
//                    Console.WriteLine(dalproduct.Read(_i));
//                }
//                break;
//            case 'e':
//                Console.WriteLine("Enter the ID of the orter that you want to update");
//                isRead = int.TryParse(Console.ReadLine(), out int IDup);
//                Console.WriteLine("Enter the updates that you want to change");
//                string nameup = Console.ReadLine();
//                int priup = Console.Read();
//                Category categoryup;
//                Category.TryParse(Console.ReadLine(), out categoryup);
//                int instokeup = Console.Read();
//                Product updateObject = new Product(IDup,nameup,priup,categoryup,instokeup);
//                dalproduct.Update(IDup, updateObject);
//                break;
//            case 'f':
//                Console.WriteLine("Enter the ID of the product that you want to delete" + "\n");
//                isRead = int.TryParse(Console.ReadLine(), out int IDdel);
//                dalproduct.Delete(IDdel);
//                break;
//            default:
//                Console.WriteLine("Error");
//                break;


//        }
//    }

//    static void Main(string[] arr) {
//        Console.WriteLine("Welcome to our shop!\n" +
//            " in order to start please enter tou choice\n" +
//            "0 Exit\n"+
//            "1  for product\n"+
//            "2 for order\n" +
//            "3 for order item\n");
//        DalOrder dalorder = new DalOrder();
//        DalOrderItem dalorderitem = new DalOrderItem();
//        DalProduct dalproduct = new DalProduct();
//        bool isRead = char.TryParse(Console.ReadLine(), out char option);
//        try
//        {
//            while (option != '0')
//            {

//                switch (option)
//                {

//                    case '1':
//                        choosenOrder(ref dalorder);
//                        break;
//                    case '2':
//                        choosenOrderItem(ref dalorderitem);
//                        break;
//                    case '3':
//                        choosenProduct(ref dalproduct);
//                        break;
//                }
//                Console.WriteLine("Enter your choice");
//                option = Console.ReadLine()[0];
//            }
//            Console.WriteLine("Goodbuy");
//        }
//        catch (Exception messege)
//        {
//            Console.WriteLine(messege.Message);
//        }
//    } 
//}