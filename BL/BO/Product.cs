using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enum;
namespace BO
{
    public class Product
    {
        //Product fields
        public int ID { set; get; }
        public string? NameOfProduct { set; get; }
        public double Price { set; get; }
        public category? Category { set; get; }
        public int InStoke { set; get; }
        public override string ToString() => $@"
        Product ID:{ID}
        name of produdt: {NameOfProduct}, 
        category : {Category}
    	Price: {Price}
    	Amount in stock: {InStoke}
        ";

    }
}
