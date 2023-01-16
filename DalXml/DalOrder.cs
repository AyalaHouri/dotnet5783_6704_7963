using DalApi;
using DO;
using System.Linq;

namespace Dal
{
    internal class DalOrder : IOrder
    {
        string orderPath= @"Order";
        public int Add(Order order)
        {
            List<DO.Order?> listoforder = XMLTools.LoudListFromXMLSerializer<DO.Order>(orderPath);
            if (listoforder.FirstOrDefault(item => item?.ID == order.ID) != null)
            {
                throw new MyException("THE ORDER IS EXIST, CAN NOT ADD\n");
            }
            listoforder.Add(order);
            XMLTools.SaveListToXMLSerializer(listoforder, orderPath);
            return order.ID;
        }

        public void Delete(Order order)
        {
            List<DO.Order?> listoforder = XMLTools.LoudListFromXMLSerializer<DO.Order>(orderPath);
            if (listoforder.RemoveAll(item => item?.ID == order.ID) == 0)
                throw new MyException("THE ORDER DOES NOT EXIST\n");
            XMLTools.SaveListToXMLSerializer(listoforder, orderPath);
        }

        public Order GetByID(int ID)
        {
            List<DO.Order?> listoforder = XMLTools.LoudListFromXMLSerializer<DO.Order>(orderPath);
            return listoforder.FirstOrDefault(item => item?.ID == ID) ?? throw new MyException("THE ID DOES NOT EXIST");
        }

        public IEnumerable<Order?> GetTheList(Func<Order?, bool>? func = null)
        {
            List<DO.Order?> listoforder = XMLTools.LoudListFromXMLSerializer<DO.Order>(orderPath);
            if (func == null)
                return listoforder.Select(item => item).OrderBy(item => item?.ID);
            return listoforder.Where(func).OrderBy(item => item?.ID);
        }

        public void Update(int ID, Order order)
        {
            Delete(order);
            Add(order);
        }
    }
}