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
        var v=DataSource.LOrderItem.OrderBy(item => item?.ID);
        DataSource.LOrderItem = v.ToList();
        return orderItem.ID;
    }

    public void Update(int ID, OrderItem orderItem)
    {

        OrderItem temporderitem = DataSource.searchOrderItem(orderItem.ID) ?? throw new MyException("the id is null");
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
        return DataSource.LOrderItem.Where(orderItem => orderItem?.OrderID == orderId);
    }

    IEnumerable<OrderItem?> IOrderItem.GetOrderItemsByproductId(int prudactid)
    {
        return DataSource.LOrderItem.Where(orderItem => orderItem?.ProductID == prudactid);
    }

    public IEnumerable<OrderItem?> GetTheList(Func<OrderItem?, bool>? func = null)
    {
        return DataSource.LOrderItem;
    }
}

