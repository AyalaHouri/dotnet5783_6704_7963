using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
namespace BO
{
    public class Order
    {
        // Order fields
        public int ID { set; get; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public status OrderStatus { get; set; }
        public double TotalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem> Items { get; set; }
        public override string ToString() => $@"Order ID:{ID}
        Customer Name:{CustomerName}
        Customer Email:{CustomerEmail}
        Customer Adress:{CustomerAddress}
        Order Date:{OrderDate}
        Ship Date:{ShipDate}
        Delivery Date:{DeliveryDate}
        Order Status:{OrderStatus}
        Items:{string.Join('\n', Items)} ";
    }
}






