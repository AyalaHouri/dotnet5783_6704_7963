﻿using DalApi;
using DO;
using static DO.Enums;
namespace Dal;

internal static class DataSource
{

    internal static readonly Random random = new Random();
    internal static List<Order?> LOrder = new List<Order?>();
    internal static List<OrderItem?> LOrderItem = new List<OrderItem?>();
    internal static List<Product?> LProduct = new List<Product?>();

    //Name data and price data

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
        return LProduct.FirstOrDefault(item => item?.ID == ID) ?? throw new DO.MyException("NOT FOUND\n");

    }
    internal static void s_Initialize()
    {
        string[,] productsNames = new string[5, 4] { {"mascara","eyeliner","lipstick","shadows" },
                                                           {"bodycream","facecream","perfume","bodysoap" },
                                                           {"lest","babyliss","straightener","shaver" },
                                                           {"necklace","hairdye","ring","comb" },
                                                            {"Dark Chocolate","White Chocolate","Strobery Chocolate","heart Chocolate"}
                                                            };
        int[,] productsPrice = new int[5, 4] { { 210,180,120,500},
                                                           {200,150,250,500},
                                                           {900,600,750,400},
                                                           {700,90,350,75},
                                                           {700,90,350,75} };

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
            p.OrderDate = DateTime.Now.AddDays(-1 * random.Next(0, 30));
            if (p.OrderDate < DateTime.Now.AddDays(-10))
            {
                p.ShipDate = p.OrderDate + new TimeSpan(random.Next(0, 9), random.Next(0, 23), random.Next(0, 59), random.Next(0, 59));

            }
            if (p.ShipDate < DateTime.Now.AddDays(-15))
            {
                p.DeliveryDate = p.ShipDate + new TimeSpan(random.Next(0, 4), random.Next(0, 23), random.Next(0, 59), random.Next(0, 59));
            }
            p.ID = Config.get_ID_Order;
            if (i == 19)
            {
                p.ShipDate = null;
                p.DeliveryDate = null;
                p.OrderDate = DateTime.Now.AddDays(-150);
            }
            LOrder.Add(p);
        }
        Config.I_Order = 20;

        //Product

        Product _product = new Product();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {

                int x = Config.get_ID_Product;
                _product.ID = x;
                int num = random.Next(0, 3);
                //All this fuss is to get a random category
                var v = Enum.GetValues(typeof(Category));
                Category eunmVal = (Category)v.GetValue(i);


                _product.Category = eunmVal;
                _product.NameOfProduct = productsNames[i, j];
                _product.Price = productsPrice[i, j];
                _product.InStoke = random.Next(2, 12);

                LProduct.Add(_product);
            }
        }
        Config.I_Product = 20;

        //OrderItem



        for (int i = 0; i < 40; i++)
        {
            int Index = random.Next(0, 10);
            int ProductID;
            Product? Productt = LProduct[Index];
            ProductID = Productt?.ID ?? throw new MyException("error");
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
                return ID_Product;
            }
        }
    }
}
