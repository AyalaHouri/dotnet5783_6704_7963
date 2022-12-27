using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi
{
    public interface IOrderItem : ICrud<OrderItem>
    {
        IEnumerable<OrderItem?> GetOrderItemsByOrderId(int orderId);
        IEnumerable<OrderItem?> GetOrderItemsByproductId(int prudactid);
    }
}
