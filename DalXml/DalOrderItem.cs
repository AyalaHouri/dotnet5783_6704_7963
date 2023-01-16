using DalApi;
using DO;
using System.Linq;

namespace Dal
{
    internal class DalOrderItem : IOrderItem
    {
        string orderItempath= @"OrderItem";
        public int Add(OrderItem orderItem)
        {
            List<DO.OrderItem?> listoforderItem = XMLTools.LoudListFromXMLSerializer<DO.OrderItem>(orderItempath);
            if (listoforderItem.FirstOrDefault(item => item?.ID == orderItem.ID) != null)
            {
                throw new MyException("THE ORDERITEM IS EXIST, CAN NOT ADD\n");
            }
            listoforderItem.Add(orderItem);
            XMLTools.SaveListToXMLSerializer(listoforderItem, orderItempath);
            return orderItem.ID;
        }

        public void Delete(OrderItem orderItem)
        {
            List<DO.OrderItem?> listoforderItem = XMLTools.LoudListFromXMLSerializer<DO.OrderItem>(orderItempath);
            if (listoforderItem.RemoveAll(item => item?.ID == orderItem.ID) == 0)
                throw new MyException("THE ORDERITEM DOES NOT EXIST\n");
            XMLTools.SaveListToXMLSerializer(listoforderItem, orderItempath);
        }

        public OrderItem GetByID(int ID)
        {
            List<DO.OrderItem?> listoforderItem = XMLTools.LoudListFromXMLSerializer<DO.OrderItem>(orderItempath);
            return listoforderItem.FirstOrDefault(item => item?.ID == ID) ?? throw new MyException("THE ID DOES NOT EXIST");
        }

        public IEnumerable<OrderItem?> GetOrderItemsByOrderId(int orderId)
        {
            List<DO.OrderItem?> listoforderItem = XMLTools.LoudListFromXMLSerializer<DO.OrderItem>(orderItempath);
            return listoforderItem.Where(orderItem => orderItem?.ID == orderId);
        }

        public IEnumerable<OrderItem?> GetOrderItemsByproductId(int prudactid)
        {
            List<DO.OrderItem?> listoforderItem = XMLTools.LoudListFromXMLSerializer<DO.OrderItem>(orderItempath);
            return listoforderItem.Where(orderItem => orderItem?.ProductID == prudactid);
        }

        public IEnumerable<OrderItem?> GetTheList(Func<OrderItem?, bool>? func)
        {
            List<DO.OrderItem?> listoforderItem = XMLTools.LoudListFromXMLSerializer<DO.OrderItem>(orderItempath);
            if (func == null)
                return listoforderItem.Select(item => item).OrderBy(item => item?.ID);
            return listoforderItem.Where(func).OrderBy(item => item?.ID);
        }

        public void Update(int ID, OrderItem orderItem)
        {
            Delete(orderItem);
            Add(orderItem);
        }
    }
}