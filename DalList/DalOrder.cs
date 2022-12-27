using DO;
using static DO.Enums;

using DalApi;
namespace Dal;
internal class DalOrder : IOrder
{

    public int Add(Order order)
    ///add order to orderlist
    {
        DataSource.LOrder.Add(order);
        DataSource.Config.I_Order++;
        return order.ID;


    }
    public void Update(int ID, Order order)
    {
        Order temporderitem = DataSource.searchOrder(ID) ?? throw new MyException("the id is null");
        Delete(temporderitem);
        Add(order);

    }
    public void Delete(Order dorder)
    {
        DataSource.LOrder.Remove(DataSource.searchOrder(dorder.ID));
        DataSource.Config.I_Order--;
    }

    public int Order_Length()
    {
        return DataSource.Config.I_Order;
    }
    public Order GetByID(int ID)
    {
        Order order = DataSource.searchOrder(ID) ?? throw new MyException("the id is null");
        return order;
    }
    public IEnumerable<Order?> GetTheList(Func<Order?, bool>? func = null)
    {
        return DataSource.LOrder;
    }

}

