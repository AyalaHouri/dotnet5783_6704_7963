
using BlApi;
using System.Linq;

namespace BlImplementation
{
    internal class Cart : ICart //class for Implementation of functions
    {
        DalApi.IDal? _idal = DalApi.Factory.Get();

        public BO.Cart AddToCart(BO.Cart? cart, int productid)//mathode in order to add product to the cart
        {
            bool exist = false;
            DO.Product product = _idal.Product.GetByID(productid);

            if (cart == null)//if the cart is empty add to the cart the product
            {
                cart = new BO.Cart();
                BO.OrderItem neworderitem = new BO.OrderItem();
                neworderitem.ID = Config.get_ID_OrderItemBO;
                neworderitem.NameOfProduct = product.NameOfProduct;
                neworderitem.Price = product.Price;
                neworderitem.ProductID = product.ID;
                cart.Items = new List<BO.OrderItem?>();

                if (product.InStoke > 0)
                {
                    neworderitem.Amount = 1;
                    neworderitem.TotalPrice += neworderitem.Price;

                    cart.Items.Add(neworderitem);
                    cart.TotalPrice += neworderitem.Price;
                }
                else
                    throw new BO.ExceptionLogi("We are very sorry but the product is out of stock:(");
            }
            else
            {

                exist = cart.Items.Any(prod => prod?.ProductID == productid);
                if (exist)
                {
                    var matchingProduct = cart.Items.First(prod => prod.ProductID == productid);
                    if (product.InStoke > 0)
                    {
                        matchingProduct.Amount++;
                        matchingProduct.TotalPrice += matchingProduct.Price;
                        cart.TotalPrice += matchingProduct.Price;
                    }
                    else
                    {
                        throw new BO.ExceptionLogi("We are very sorry but the product is out of stock:(");
                    }
                }

                if (!exist)
                {
                    BO.OrderItem neworderitem = new BO.OrderItem();
                    neworderitem.ID = Config.get_ID_OrderItemBO;
                    neworderitem.NameOfProduct = product.NameOfProduct;
                    neworderitem.Price = product.Price;
                    neworderitem.ProductID = product.ID;


                    if (product.InStoke > 0)
                    {
                        neworderitem.Amount = 1;
                        neworderitem.TotalPrice += neworderitem.Price;
                        cart.Items.Add(neworderitem);
                        cart.TotalPrice += neworderitem.Price;
                    }
                    else
                        throw new BO.ExceptionLogi("We are very sorry but the product is out of stock:(");

                }
            }
            return cart;
        }
        public BO.Cart UpdateAmount(BO.Cart cart, int productid, int newAmount)//update the amount in the cart
        {
            bool exist = false;
            if (cart.Items == null)
            {
                throw new BO.ExceptionLogi("your cart is empty");
            }

            exist = cart.Items.Any(prod => prod?.ProductID == productid);
            if (exist)
            {
                var matchingProduct = cart.Items.First(prod => prod.ProductID == productid);
                if (matchingProduct.Amount < newAmount)
                {
                    while (matchingProduct.Amount != newAmount)
                    {
                        AddToCart(cart, productid);
                    }
                }
                if (matchingProduct.Amount > newAmount)
                {
                    if (matchingProduct.Amount == 1)
                    {
                        cart.TotalPrice -= matchingProduct.Price;
                        cart.Items.Remove(matchingProduct);
                    }
                    else
                    {
                        matchingProduct.Amount--;
                        cart.TotalPrice -= matchingProduct.Price;
                    }
                }
            }
            if (!exist)
            {
                throw new BO.ExceptionLogi("Error");
            }
            return cart;
        }
        public void DeletAll(BO.Cart cart)
        {

            int count = cart.Items.Count();
            for (int i = count - 1; i >= 0; i--)
            {
                cart.Items.RemoveAt(i);
            }
            cart.TotalPrice = 0;
        }
        public void AproveOrder(BO.Cart cart)//confirm the order
        {
            if (cart.Items == null)
                throw new BO.ExceptionLogi("your cart is empty");
            if (cart.CustomerAddress == "")
                throw new BO.ExceptionLogi("Customer Address is missing");
            if (cart.CustomerName == "")
                throw new BO.ExceptionLogi("Customer name is missing");
            if (cart.CustomerEmail == "")
                throw new BO.ExceptionLogi("Customer email is missing");
            ///if (cart.CustomerEmail.Contains("@gmail.com"))
            /// throw new BO.ExceptionLogi("Customer email is not valid");
            if (cart.Items.FirstOrDefault(item => item?.Price <= 1) != null)
                throw new BO.ExceptionLogi($"The {cart.Items.FirstOrDefault(item => item?.Price <= 1).NameOfProduct} product price is invalid");
            if (cart.Items.FirstOrDefault(item => item?.Amount < 1) != null)
                throw new BO.ExceptionLogi($"The {cart.Items.FirstOrDefault(item => item?.Price <= 1).NameOfProduct} product quantity is invalid");
            
            DO.Order neworder = new DO.Order();

            neworder.CustomerAddress = cart.CustomerAddress;
            neworder.CustomerEmail = cart.CustomerEmail;
            neworder.CustomerName = cart.CustomerName;
            neworder.OrderDate = DateTime.Now;
            DO.OrderItem neworderitem = new DO.OrderItem();
            int orderid = _idal.Order.Add(neworder);
            cart.Items.ForEach(prod =>
            {
                neworderitem.ProductID = prod?.ProductID ?? 0;
                neworderitem.OrderID = orderid;
                neworderitem.Price = prod?.Price ?? 0;
                neworderitem.Amount = prod?.Amount ?? 0;
                if (_idal.Product.GetByID(prod.ProductID).InStoke >= prod.Amount)
                {
                    DO.Product temprodect = new DO.Product();
                    temprodect = _idal.Product.GetByID(prod.ProductID);
                    temprodect.InStoke -= prod.Amount;
                    _idal.Product.Update(prod.ProductID, temprodect);
                    int orderitemid = _idal.OrderItem.Add(neworderitem);
                }
                else
                {
                    throw new BO.ExceptionLogi($"We are very sorry but the {prod.NameOfProduct} product is out of stock");
                }
            });


            DeletAll(cart);
        }


    }


}
