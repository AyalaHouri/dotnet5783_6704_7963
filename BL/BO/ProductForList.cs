using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
namespace BO
{
    public class ProductForList
    {
        //ProductForList fields
        public int ID { set; get; }
        public string? NameOfProduct { set; get; }
        public double PricePerProduct { set; get; }
        public category? Category { set; get; }
        public override string ToString() => $@"ID:{ID}
        Name Of Product:{NameOfProduct}
        Price Per Product:{PricePerProduct}
        Category:{Category}
";
    }
}


