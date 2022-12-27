using DO;
using DalApi;

namespace Dal;
internal class DalOrderItem : IOrderItem
{

    public int Create(int ProductID, int OrderID, double Price, int Amount)
    {
        int ID = DataSource.Config.get_ID_OrderItem;
        OrderItem orderitem = new OrderItem(ID, ProductID, OrderID, Price, Amount);
        return ID;

    }
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.get_ID_OrderItem;
        DataSource.LOrderItem.Add(orderItem);
        DataSource.Config.I_OrderItem++;
        return orderItem.ID;
    }

    public void Update(int ID, OrderItem orderItem)
    {

        OrderItem temporderitem = DataSource.searchOrderItem(ID) ?? throw new MyException("the id is null");
        Delete(temporderitem);
        temporderitem.ID = ID;
        temporderitem.OrderID = orderItem.OrderID;
        temporderitem.Price = orderItem.Price;
        temporderitem.Amount = orderItem.Amount;
        temporderitem.ProductID = orderItem.ProductID;
        Add(temporderitem);
    }
    public void Delete(OrderItem dorderitem)
    {
        DataSource.LOrderItem.Remove(DataSource.searchOrderItem(dorderitem.ID));
        DataSource.Config.I_OrderItem--;
    }
    //public OrderItem Read(int I)
    //{
    //    if (DataSource.LOrderItem.Length < I || I < 0)
    //        throw new IndexOutOfRangeException("Read range Error");
    //    return DataSource.arrOrderItem[I];

    //}

    public OrderItem GetByID(int ID)
    {
        return DataSource.searchOrderItem(ID) ?? throw new MyException("the id is null");
    }
    public IEnumerable<OrderItem?> GetTheList()
    {
        return DataSource.LOrderItem;
    }


    public int orderitemlength()
    {
        return DataSource.Config.I_OrderItem;
    }

    public IEnumerable<OrderItem?> GetOrderItemsByOrderId(int orderId)
    {
        return DataSource.LOrderItem.Where(orderItem => orderItem?.ID == orderId);
    }

    IEnumerable<OrderItem?> IOrderItem.GetOrderItemsByproductId(int prudactid)
    {
        return DataSource.LOrderItem.Where(pruduct => pruduct?.ID == prudactid);
    }

    public IEnumerable<OrderItem?> GetTheList(Func<OrderItem?, bool>? func = null)
    {
        return DataSource.LOrderItem;
    }
}

