using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal///this class give us a option to get to the detail layer
    {
        IOrder Order { get; }
        IOrderItem OrderItem { get; }
        IProduct Product { get; }
    }
}
