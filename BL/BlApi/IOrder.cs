using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        //order method declarations
        IEnumerable<OrderForList?> AskList();
        public BO.Order OrderDetail(int orderid);

        public BO.Order ordershipdateupdate(int orderid);
        public BO.Order UpdateStock(int orderid);
        public BO.OrderTracking OrderTrackingFunc(int orderid);
        public BO.Order UpDate(int orderID, int productID, int plus_minus);
        public int? ordertosimulator();
        public void Deleteorder(BO.Order order);
    }
}
