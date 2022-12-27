using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface ICart
{
    //cart method declarations
    public Cart AddToCart(Cart cart, int product);
    public Cart UpdateAmount(Cart cart, int product, int newAmount);
    public void AproveOrder(Cart cart);
     public void DeletAll(Cart cart);



}
