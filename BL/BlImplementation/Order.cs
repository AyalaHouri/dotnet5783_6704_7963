﻿
using System.Linq;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        DalApi.IDal? _idal = DalApi.Factory.Get();
        public IEnumerable<BO.OrderForList?> AskList()///returns list of OrderForList- get the detail from the datalayer
        {

            return _idal.Order.GetTheList().Select(order =>
            {
                int p=0;
                IEnumerable<DO.OrderItem?> orderitems = _idal.OrderItem.GetOrderItemsByOrderId(order?.ID ?? 0);
                return new BO.OrderForList
                {
                    ID = order?.ID ?? 0,
                    CustomerName = order?.CustomerName,
                    OrderStatus = getStatus((DO.Order)order),
                    AmountOfItems = orderitems.Where(orderitem=> (orderitem?.ID??0)== order?.ID).Sum(orderItem=> orderItem?.Amount ?? 0),
                    TotalPrice = orderitems.Sum(orderItem => (orderItem?.Amount ?? 0) * (orderItem?.Price ?? 0)),
                };
            });
        }

        private BO.Enum.status getStatus(DO.Order order)///returns the status of the objects 
        {
            return order switch
            {
                DO.Order order1 when order.DeliveryDate != DateTime.MinValue => BO.Enum.status.deliveredToCustomer,
                DO.Order order2 when order.DeliveryDate != DateTime.MinValue => BO.Enum.status.shipped,
                _ => BO.Enum.status.OrderConfirmed,
            };
        }

        //public BO.Order OrderDetail(int orderid)///returns the order details
        //{
        //    if (orderid > 0)
        //    {
        //        DO.Order FOrder = new DO.Order();
        //        FOrder = exist(orderid);
        //        IEnumerable<DO.OrderItem?> orderitems = _idal.OrderItem.GetOrderItemsByOrderId(FOrder.ID);
        //        BO.Order order = new BO.Order();
        //        order.ID = FOrder.ID;
        //        order.CustomerName = FOrder.CustomerName;
        //        order.CustomerAddress = FOrder.CustomerAddress;
        //        order.CustomerEmail = FOrder.CustomerEmail;
        //        order.OrderStatus = getStatus(FOrder);
        //        BO.OrderItem titem = new BO.OrderItem();
        //        order.Items = new List<BO.OrderItem>();
        //        foreach (DO.OrderItem item in orderitems)
        //        {
        //            titem.ID = item.ID;
        //            titem.ProductID = item.ProductID;
        //            titem.NameOfProduct = _idal.Product.GetByID(item.ProductID).NameOfProduct;
        //            titem.Amount = item.Amount;
        //            titem.Price = item.Price;

        //            titem.TotalPrice = orderitems?.Sum(orderItem => (orderItem?.Amount ?? 0) * (orderItem?.Price ?? 0));///calcuate the total price
        //            order.Items.Add(titem); //order.Items = orderitems, we need to convert from DO to BO
        //        }
        //        order.TotalPrice = (double)order.Items.Sum(item => item.TotalPrice);
        //        return order;
        //    }
        //    throw new Exception("NEGETIVE ID\n");
        //}
        public BO.Order OrderDetail(int orderid)///returns the order details
        {
            if (orderid > 0)
            {
                DO.Order FOrder = new DO.Order();
                FOrder = exist(orderid);
                IEnumerable<DO.OrderItem?> orderitems = _idal.OrderItem.GetOrderItemsByOrderId(FOrder.ID);
                BO.Order order = new BO.Order();
                order.ID = FOrder.ID;
                order.CustomerName = FOrder.CustomerName;
                order.CustomerAddress = FOrder.CustomerAddress;
                order.CustomerEmail = FOrder.CustomerEmail;
                order.OrderStatus = getStatus(FOrder);
                order.Items = new List<BO.OrderItem>();
                order.DeliveryDate= FOrder.DeliveryDate;
                order.OrderDate = FOrder.OrderDate;
                order.ShipDate = FOrder.ShipDate;
                var v = orderitems.Select(item =>
                {
                    return new BO.OrderItem()
                    {
                        ID = item?.ID ?? 0,
                        ProductID = item?.ProductID ?? 0,
                        NameOfProduct = _idal.Product.GetByID(item?.ProductID ?? 0).NameOfProduct,
                        Amount = item?.Amount ?? 0,
                        Price = item?.Price ?? 0,
                        TotalPrice = orderitems.Sum(orderItem => (orderItem?.Amount ?? 0) * (orderItem?.Price ?? 0)),
                    };

                }).ToList();
                order.Items = v;
                order.TotalPrice = (double)order.Items.Sum(item => item.TotalPrice);
                return order;
            }
            throw new BO.ExceptionLogi("NEGETIVE ID\n");
        }
        public DO.Order exist(int orderid)///checks if the order exist and returns it
        {
            DO.Order FOrder = new DO.Order();
            IEnumerable<DO.Order?> lorder = _idal.Order.GetTheList();
            return lorder.FirstOrDefault(item => item?.ID == orderid)?? throw new Exception("THE ID IS NOT EXIST\n");
        }
        public BO.Order orderupdat(int orderid)///updait the shipping date
        {
            if (orderid > 0)
            {
                DO.Order FOrder = new DO.Order();
                FOrder = exist(orderid);
                if (FOrder.ShipDate != DateTime.MinValue)
                {
                    throw new Exception("ALLREADY SHIPPED\n");
                }
                FOrder.ShipDate = DateTime.Now;
                _idal.Order.Update(orderid, FOrder);//updat the data
                BO.Order BOrder = new BO.Order();
                BOrder.ID = FOrder.ID;
                BOrder.OrderDate = FOrder.OrderDate;
                BOrder.ShipDate = DateTime.Now;
                BOrder.CustomerName = FOrder.CustomerName;
                BOrder.CustomerAddress = FOrder.CustomerAddress;
                BOrder.CustomerEmail = FOrder.CustomerEmail;
                BOrder.DeliveryDate = FOrder.DeliveryDate;
                BOrder.Items = new List<BO.OrderItem>();
                IEnumerable<DO.OrderItem?> orderitems = _idal.OrderItem.GetOrderItemsByOrderId(FOrder.ID);
                //var v = orderitems.Select(item =>
                //{
                //    return new BO.OrderItem()
                //    {
                //        ID = item?.ID ?? 0,
                //        ProductID = item?.ProductID ?? 0,
                //        NameOfProduct = _idal.Product.GetByID(item?.ProductID ?? 0).NameOfProduct,
                //        Amount = item?.Amount ?? 0,
                //        Price = item?.Price ?? 0,
                //        TotalPrice = orderitems?.Sum(orderItem => orderItem?.Amount ?? 0 * orderItem?.Price ?? 0),
                //    };

                //});
                var v = from item in orderitems
                        select new BO.OrderItem()
                        {
                            ID = item?.ID ?? 0,
                            ProductID = item?.ProductID ?? 0,
                            NameOfProduct = _idal.Product.GetByID(item?.ProductID ?? 0).NameOfProduct,
                            Amount = item?.Amount ?? 0,
                            Price = item?.Price ?? 0,
                            TotalPrice = orderitems?.Sum(orderItem => (orderItem?.Amount ?? 0) * (orderItem?.Price ?? 0)),
                        };
                BOrder.Items = v.ToList();
                return BOrder;
            }
            throw new Exception("NEGETIVE ID\n");
        }
        public BO.Order UpdateStock(int orderid)///update the stoke of the order

        {
            DO.Order FOrder = new DO.Order();
            FOrder = exist(orderid);
            if (FOrder.ShipDate != DateTime.MinValue && FOrder.DeliveryDate != DateTime.MinValue)
            {
                throw new BO.ExceptionLogi("DID NOT ARRIVED\n");
            }
            FOrder.DeliveryDate = DateTime.Now;
            _idal.Order.Update(orderid, FOrder);//updat the data
            BO.Order BOrder = new BO.Order();
            BOrder.ID = FOrder.ID;
            BOrder.OrderDate = FOrder.OrderDate;
            BOrder.ShipDate = FOrder.ShipDate;
            BOrder.CustomerName = FOrder.CustomerName;
            BOrder.CustomerAddress = FOrder.CustomerAddress;
            BOrder.CustomerEmail = FOrder.CustomerEmail;
            BOrder.DeliveryDate = FOrder.DeliveryDate;
            BOrder.Items = new List<BO.OrderItem>();
            IEnumerable<DO.OrderItem?> orderitems = _idal.OrderItem.GetOrderItemsByOrderId(FOrder.ID);
            BO.OrderItem titem = new BO.OrderItem();
            var v = from item in orderitems
                    select new BO.OrderItem()
                    {
                        ID = item?.ID ?? 0,
                        ProductID = item?.ProductID ?? 0,
                        NameOfProduct = _idal.Product.GetByID(item?.ProductID ?? 0).NameOfProduct,
                        Amount = item?.Amount ?? 0,
                        Price = item?.Price ?? 0,
                        TotalPrice = orderitems?.Sum(orderItem => (orderItem?.Amount ?? 0) * (orderItem?.Price ?? 0)),
                    };
            BOrder.Items = v.ToList();
            return BOrder;
            //var v = orderitems.Select(item =>
            //{
            //    return new BO.OrderItem()
            //    {
            //        ID = item?.ID ?? 0,
            //        ProductID = item?.ProductID ?? 0,
            //        NameOfProduct = _idal.Product.GetByID(item?.ProductID ?? 0).NameOfProduct,
            //        Amount = item?.Amount ?? 0,
            //        Price = item?.Price ?? 0,
            //        TotalPrice = orderitems?.Sum(orderItem => orderItem?.Amount ?? 0 * orderItem?.Price ?? 0),
            //    };

            //});
            //BOrder.Items = v.ToList();
            //return BOrder;
        }
        public BO.OrderTracking OrderTrackingFunc(int orderid)///track the order by ID,return the tracking detail(OrderTracking)
        {
            DO.Order FOrder = new DO.Order();
            FOrder = exist(orderid);
            BO.OrderTracking? orderTracking = new BO.OrderTracking();
            orderTracking.OrderStatus = getStatus(FOrder);
            orderTracking.Tracking = new List<Tuple<DateTime, BO.Enum.status>>();
            orderTracking.Tracking.Add(new Tuple<DateTime, BO.Enum.status>((DateTime?)FOrder.OrderDate??DateTime.Now, BO.Enum.status.OrderConfirmed));
            orderTracking.Tracking.Add(new Tuple<DateTime, BO.Enum.status>((DateTime?)FOrder.DeliveryDate??DateTime.Now, BO.Enum.status.deliveredToCustomer));
            orderTracking.Tracking.Add(new Tuple<DateTime, BO.Enum.status>((DateTime?)FOrder.ShipDate?? DateTime.Now, BO.Enum.status.shipped));
            return orderTracking;
        }
    }
}
