
using BlApi;
namespace BlImplementation
{
    internal class Cart : ICart //class for Implementation of functions
    {
        DalApi.IDal? _idal = DalApi.Factory.Get();
 
        public BO.Cart AddToCart(BO.Cart cart, int productid)//mathode in order to add product to the cart
        {
            bool exist = false;
            DO.Product product = _idal.Product.GetByID(productid);

            if (cart.Items == null)//if the cart is empty add to the cart the product
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
                    cart.Items = new List<BO.OrderItem>();
                    cart.Items.Add(neworderitem);
                    cart.TotalPrice += neworderitem.Price;
                }
                else
                    throw new BO.ExceptionLogi("We are very sorry but the product is out of stock:(");
            }
            else
            {

                foreach (var prod in cart.Items)//check if the product is allrady exsite
                {
                    if (prod.ProductID == productid)
                    {
                        exist = true;
                        if (product.InStoke > 0)
                        {
                            prod.Amount++;
                            prod.TotalPrice += prod.Price;
                            cart.TotalPrice += prod.Price;
                        }
                        else
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

            foreach (var prod in cart.Items)
            {
                if (prod.ProductID == productid)
                {
                    exist = true;


                    if (prod.Amount < newAmount)
                    {
                        while (prod.Amount != newAmount)
                        {
                            AddToCart(cart, productid);
                        }

                    }
                    if (prod.Amount > newAmount)
                    {
                        if (prod.Amount == 1)
                        {
                            cart.TotalPrice -= prod.Price;
                            cart.Items.Remove(prod);
                        }
                        else
                        {
                            prod.Amount--;
                            cart.TotalPrice -= prod.Price;
                        }
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
            for (int i = 0; i < count; i++)
            {
                cart.Items.RemoveAt(i);
            }
            cart.TotalPrice = 0;
        }
        public void AproveOrder(BO.Cart cart)//confirm the order
        {
            if (cart.Items == null)
                throw new BO.ExceptionLogi("your cart is empty");
            if (cart.CustomerAddress == null)
                throw new BO.ExceptionLogi("Customer Address is missing");
            if (cart.CustomerName == null)
                throw new BO.ExceptionLogi("Customer name is missing");
            if (cart.CustomerEmail == null)
                throw new BO.ExceptionLogi("Customer email is missing");
            //  if (cart.CustomerEmail.Contains("@gmail.com"))
            //  throw new ExceptionLogi("Customer email is not valid");
            foreach (var prod in cart.Items)
            {
                if (prod.Price <= 1)
                    throw new BO.ExceptionLogi($"The {prod.NameOfProduct} product price is invalid");
                if (prod.Amount < 1)
                    throw new BO.ExceptionLogi($"The {prod.NameOfProduct} product quantity is invalid");
            }
            DO.Order neworder = new DO.Order();
            neworder.CustomerAddress = cart.CustomerAddress;
            neworder.CustomerEmail = cart.CustomerEmail;
            neworder.CustomerName = cart.CustomerName;
            neworder.OrderDate = DateTime.Now;
            DO.OrderItem neworderitem = new DO.OrderItem();
            int orderid = _idal.Order.Add(neworder);
            foreach (var prod in cart.Items)
            {
                neworderitem.ProductID = prod.ProductID;
                neworderitem.OrderID = orderid;
                neworderitem.Price = prod.Price;
                neworderitem.Amount = prod.Amount;
                if (_idal.Product.GetByID(prod.ProductID).InStoke >= prod.Amount)
                {
                    DO.Product temprodect = new DO.Product();
                    temprodect = _idal.Product.GetByID(prod.ProductID);
                    temprodect.InStoke -= prod.Amount;
                    _idal.Product.Update(prod.ProductID, temprodect);
                    int orderitemid = _idal.OrderItem.Add(neworderitem);
                }
                else { throw new BO.ExceptionLogi($"We are very sorry but the {prod.NameOfProduct} product is out of stock"); }
            }

            DeletAll(cart);
        }


    }


}
