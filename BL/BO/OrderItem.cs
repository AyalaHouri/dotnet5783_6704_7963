using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        //OrderItem fields
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string? NameOfProduct { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double? TotalPrice { get; set; }
        public override string ToString() 
        { return $@"ID:{ID}
          Product ID:{ProductID}
          Name Of Product:{NameOfProduct}
          Price:{Price}
          Amount:{Amount}
          Total Price:{TotalPrice}";
        }
    }
}
