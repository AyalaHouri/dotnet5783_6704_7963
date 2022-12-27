using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
namespace BO
{
    public class OrderTracking
    {
        //OrderTracking fields
        public int ID { set; get; }
        public status? OrderStatus { get; set; }
        public List<Tuple<DateTime, status>> Tracking { get; set; } = new();///return syring of order tracking


    }
}

//רשימה של צמדים (תאריך, תיאור התקדמות חבילה)


