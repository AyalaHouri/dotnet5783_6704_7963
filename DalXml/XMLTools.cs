using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dal;
internal class XMLTools
{

   const string s_dir = @"C:\miniproject\dotnet5783_6704_7963\dotnet5783_6704_7963\Data\";
    static XMLTools()
    {
        if (!Directory.Exists(s_dir)) Directory.CreateDirectory(s_dir);
    }
    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T?> list, String entity)
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer serializer = new(typeof(List<T?>));
            serializer.Serialize(file, list);
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException (filePath, $"fail to create xml file: {filePath}", ex);
            throw new Exception($"fail to create xml file: {filePath}", ex);
        }
    }
    public static List<T?> LoudListFromXMLSerializer<T>(String entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception ex)
        {
            // DO.XMLFileLoadCreateException (filePath, $"fail to load xml file: (filePath)", ax);
            throw new Exception($"fail to load xml file: {filePath}", ex);
        }

    }
    #endregion

    /*
     * 
     * 
     *  internal static readonly Random random = new Random();
    internal static List<Order?> LOrder = new List<Order?>();
    internal static List<OrderItem?> LOrderItem = new List<OrderItem?>();
    internal static List<Product?> LProduct = new List<Product?>();

    //Name data and price data
    internal static string[,] productsNames = new string[5, 4] { {"mascara","eyeliner","lipstick","shadows" },
                                                           {"bodycream","facecream","perfume","bodysoap" },
                                                           {"lest","babyliss","straightener","shaver" },
                                                           {"necklace","hairdye","ring","comb" },
                                                            {"Dark Chocolate","White Chocolate","Strobery Chocolate","heart Chocolate"}
                                                            };
    internal static int[,] productsPrice = new int[5, 4] { { 210,180,120,500},
                                                           {200,150,250,500},
                                                           {900,600,750,400},
                                                           {700,90,350,75},
                                                           {700,90,350,75} };

    static DataSource()
    {
        //The constructor fills some of the arrays in a random way at the very beginning
        s_Initialize();
    }
    internal static void addOrder(Order a)
    {

        LOrder.Add(a);
        Config.I_Order++;


    }

    private static Order addOrder(string CustomerName, string CustomerEmail, string CstomerAddress)
    {
        //It randomly updates order data other than user data

        Order order = new Order();
        order.ID = DataSource.Config.get_ID_Order; ;
        order.CustomerName = CustomerName;
        order.CustomerEmail = CustomerEmail;
        order.CustomerAddress = CstomerAddress;
        order.OrderDate = DateTime.Now;//fill the currect hour

        //It randomly updates when the product is shipped eighty percent of the time it has already been shipped otherwise it hasn't been shipped yet
        int _random = random.Next(0, 10);
        if (_random <= 8)
        {
            order.ShipDate = order.OrderDate + new TimeSpan(random.Next(0, 2), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59));

            //In case it is sent then fifty percent that yours has already reached the customer
            if (_random <= 4)
            {
                order.DeliveryDate = order.ShipDate + new TimeSpan(random.Next(0, 7), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59));
            }
            else
            {
                order.DeliveryDate = DateTime.MinValue;

            }
        }
        else
        {
            order.ShipDate = DateTime.MinValue;

        }

        return order;
    }
    internal static void addOrderItem(OrderItem a)
    {

        LOrderItem.Add(a);
        Config.I_OrderItem++;

    }
    private static OrderItem addOrderItem(int ProductID, int OrderID, double Price)
    {
        //This constructor depends on both user and order data

        OrderItem _orderItem = new OrderItem();
        int tid = Config.get_ID_OrderItem;
        _orderItem.ID = tid;
        _orderItem.ProductID = ProductID;
        _orderItem.OrderID = OrderID;
        _orderItem.Price = Price;
        _orderItem.Amount = random.Next(1, 4);
        return _orderItem;
    }
    internal static void addProduct(Product a)
    {

        LProduct.Add(a);
        Config.I_Product++;

    }
 

    internal static Order? searchOrder(int? ID)
    {
        foreach (Order? order in LOrder)
        {
            if (order?.ID == ID)
                return order;
        }
        throw new DO.MyException("NOT FOUND\n");
    }
    internal static OrderItem? searchOrderItem(int ID)
    {
        foreach (OrderItem? orderitem in LOrderItem)
        {
            if (orderitem?.ID == ID)
                return orderitem;
        }

        throw new DO.MyException("NOT FOUND\n");
    }
    internal static Product? searchProduct(int ID)
    {
        return LProduct.FirstOrDefault(item=>item?.ID==ID)?? throw new DO.MyException("NOT FOUND\n");

    }
    internal static void s_Initialize()
    {
        //This function builds our python arrays with data

        //User data
        string[] CustomerName = new string[] { "Ayala", "Chani", "Gila", "Haya", "Rina" };
        string[] CustomerEmail = new string[] { "Ayala@gmail.com", "Chani@gmail.com", "Gila@gmail.com", "Haya@gmail.com", "Rina@gmail.com" };
        string[] CustomerAddress = new string[] { "Jerusalem", "TelAviv", "Tsrufa", "Chayfa", "Ashdod" };

        //Order
        for (int i = 0; i < 20; i++)
        {
            int Index = random.Next(0, 4);
            Order p = new Order();
            p.CustomerName = CustomerName[Index];
            p.CustomerEmail = CustomerEmail[Index];
            p.CustomerAddress = CustomerAddress[Index];
            //p.DeliveryDate = DateTime.Now;
            p.OrderDate = DateTime.Now;
            p.DeliveryDate = DateTime.Now + new TimeSpan(random.Next(0, 7), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59));
            p.ShipDate = p.DeliveryDate + new TimeSpan(random.Next(0, 7), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59));
            p.ID = Config.get_ID_Order;
            LOrder.Add(p);
        }
        Config.I_Order = 20;

        //Product
        int ind = 0;
        for (int i = 1; i < 11; i++)
        {

            Product _product = new Product();

            _product.ID = Config.get_ID_Product;
            int num = random.Next(0, 3);
            //All this fuss is to get a random category
            var v = Enum.GetValues(typeof(Category));
            Category eunmVal = (Category)v.GetValue(ind);


            _product.Category = eunmVal;
            _product.NameOfProduct = productsNames[ind, num];
            _product.Price = productsPrice[ind, num];
            _product.InStoke = random.Next(2, 12);
            if (i % 2 == 0)
                ind++;
         LProduct.Add(_product);
        }
        Config.I_Product = 10;

        //OrderItem



        for (int i = 0; i < 40; i++)
        {
            int Index = random.Next(0, 10);
            int ProductID;
            Product? Productt = LProduct[Index];
            ProductID = Productt?.ID??throw new MyException("error");
            double Price;
            Price = Productt?.Price ?? throw new MyException("error");

            Index = random.Next(0, 20);
            int OrderID;
            Order? temporder = LOrder[Index];
            OrderID = temporder?.ID ?? throw new MyException("error");


            LOrderItem.Add(addOrderItem(ProductID, OrderID, Price));
        }

        Config.I_OrderItem = 40;

    }


    internal static class Config
    {
        //The index of how many we filled the dataset
        internal static int I_Order = 0;
        internal static int I_OrderItem = 0;
        internal static int I_Product = 0;

        //The product will go up by one every time we make another order
        private static int ID_Order = 100000;
        public static int get_ID_Order
        {
            get
            {
                ID_Order++;
                return ID_Order;
            }
        }

        //OrderItem will go up by one every time we make another order
        private static int ID_OrderItem = 100000;
        public static int get_ID_OrderItem
        {
            get
            {
                ID_OrderItem++;
                return ID_OrderItem;
            }
        }

        //This product gives a random number with six digits
        private static int ID_Product = 100000;
        public static int get_ID_Product
        {
            get
            {
                ID_Product++;
                //  ID_Product = random.Next(100000, 1000000);
                return ID_Product;
            }
        }
    }



    private void ResetData(){

     List<Product> list = new(BLObject.ViewDroneToList());
            FileStream file = new FileStream(@"C:\miniproject\dotnet5783_6704_7963\שלב 6 1\dotnet5783_6704_7963\Data\Product.xml", FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();

     List<Order> list = new(BLObject.ViewDroneToList());
            FileStream file = new FileStream(@"C:\Users\yohan\source\repos\yohanan400\dotNet5782_7227_3024\DroneToList2.xml", FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();

     List<OrderItem> list = new(BLObject.ViewDroneToList());
            FileStream file = new FileStream(@"C:\Users\yohan\source\repos\yohanan400\dotNet5782_7227_3024\DroneToList2.xml", FileMode.Create);
            XmlSerializer x = new XmlSerializer(list.GetType());
            x.Serialize(file, list);
            file.Close();

     */

}
