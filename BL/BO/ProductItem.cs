using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
namespace BO
{
    public class ProductItem
    {
        //ProductItem fields
        public int ID { set; get; }
        public string? NameOfProduct { set; get; }
        public double Price { set; get; }
        public category? Category { set; get; }
        public bool Available { set; get; }
        public int AmountInCart { set; get; }
        public override string ToString() => $@"ID:{ID}
        Name Of Product:{NameOfProduct}
        Price:{Price}
        Category:{Category}
        Available:{Available}
        Amount In Cart:{AmountInCart}";
    }
}


