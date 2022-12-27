using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
namespace BO
{
    public class OrderForList
    {
        //OrderForList fields
        public int ID { set; get; }
        public string? CustomerName { get; set; }
        public status? OrderStatus { get; set; }
        public int AmountOfItems { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString()
        {
            return $@"ID:{ID}
         Customer Name:{CustomerName}
         Order Status:{OrderStatus}
         Amount Of Items:{AmountOfItems}
         Total Price:{TotalPrice}";
        }
    }
}

