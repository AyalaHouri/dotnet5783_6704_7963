

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        DalApi.IDal? _idal = DalApi.Factory.Get();
        ///let us take detail from the data layer

        public IEnumerable<BO.ProductForList> AskForPrudactList()///returns list of product from thr data layer
        {
            return _idal.Product.GetTheList().Select(product =>
            {

                return new BO.ProductForList
                {
                    ID = product?.ID ?? 0,
                    NameOfProduct = product?.NameOfProduct,
                    PricePerProduct = product?.Price ?? 0,
                    Category = (BO.Enum.category)product?.Category,
                };
            });
        }
        public bool existbool(int productid)///checks if the product exist, if it is->will return else throw an exeption
        {
            DO.Product FOrder = new DO.Product();
            IEnumerable<DO.Product?>? lorder = _idal?.Product.GetTheList();
            foreach (DO.Product? item in lorder)
            {
                if (item?.ID == productid) { return true; }
            }
            return false;
        }
        public DO.Product exist(int productid)///checks if the product exist, if it is->will return else throw an exeption
        {
            DO.Product FOrder = new DO.Product();
            IEnumerable<DO.Product?>? lorder = _idal?.Product.GetTheList();
            foreach (DO.Product? item in lorder)
            {
                if (item?.ID == productid) { return FOrder = (DO.Product)item; }
            }
            throw new Exception("THE ID IS NOT EXIST\n");
        }
        public BO.Product prudactrequest(int idprudact)///ask for product detail by id,if the product exist->returns the product else will throw an exeption
        {
            if (idprudact > 0)
            {
                DO.Product DProdact = new DO.Product();
                DProdact = exist(idprudact);
                BO.Product BProduct = new BO.Product();
                BProduct.ID = idprudact;
                BProduct.NameOfProduct = DProdact.NameOfProduct;
                BProduct.InStoke = DProdact.InStoke;
                BProduct.Price = DProdact.Price;
                BProduct.Category = (BO.Enum.category)DProdact.Category;
                return BProduct;
            }
            throw new Exception("NEGETIVE ID\n");
        }
        public BO.ProductItem askedProductItem(int prudactid, BO.Cart cart)///returns ProductItem, build ProductItem, update it and returns it
        {
            if (prudactid > 0)
            {
                DO.Product DProdact = new DO.Product();
                DProdact = exist(prudactid);
                BO.ProductItem productItem = new BO.ProductItem();
                productItem.ID = prudactid;
                productItem.NameOfProduct = DProdact.NameOfProduct;
                productItem.Price = DProdact.Price;

                if (DProdact.InStoke > 0)
                    productItem.Available = true;
                else
                    productItem.Available = false;
                //OrderItem temporderitem= _idal.Ord
                IEnumerable<DO.OrderItem?>? orderitems = _idal?.OrderItem.GetOrderItemsByproductId(DProdact.ID);
                productItem.AmountInCart = 0;
                if (cart == null)
                    throw new Exception("cart is not exist\n");
                foreach (var item in cart.Items)
                {
                    if (item.ProductID == prudactid)
                    {
                        productItem.AmountInCart++;
                    }
                }
                return productItem;
            }
            throw new Exception("NEGETIVE ID\n");
        }
        public void AddProduct(BO.Product BProduct)///add new product to the list of product
        {
            if (BProduct.ID > 0 && BProduct.NameOfProduct != " " && BProduct.Price > 0 && BProduct.InStoke > 0&&BProduct.Category!=null)
            {
                DO.Product DProduct = new DO.Product();
                DProduct.NameOfProduct = BProduct.NameOfProduct;
                DProduct.ID = BProduct.ID; 
                DProduct.Price = BProduct.Price;
                DProduct.Category = (DO.Enums.Category)BProduct.Category;
                int flag = _idal.Product.Add(DProduct);
                if (flag == 0)
                {
                    throw new Exception("CAN NOT ADD\n");
                }///UPDATE THE DATA
            }
            else
                throw new BO.ExceptionLogi("CAN NOT ADD\n");

        }

        public void DeleteProduct(int IDProduct)///delete the product from the list
        {
            IEnumerable<DO.OrderItem?> lorder = _idal.OrderItem.GetTheList();
            IEnumerable<DO.Product?> lproduct = _idal.Product.GetTheList();
            foreach (DO.Product? itemproduct in lproduct)
            {
                if (itemproduct?.ID == IDProduct)
                {
                    foreach (DO.OrderItem? item in lorder)
                    {
                        if (item?.ProductID == IDProduct)
                        {
                            throw new Exception("THE PRODUCT IS EXIST IN ORDERS, CAN NOT DELETE\n");
                        }
                    }
                    DO.Product tproduct = _idal.Product.GetByID(IDProduct);
                    _idal.Product.Delete(tproduct);
                    return;
                }
            }
            throw new Exception("THE PRODUCT ISNOT EXIST\n");
        }
        public void updateProduct(BO.Product DProduct)///update product by id
        {

            if (DProduct.ID > 0 && DProduct.NameOfProduct != " " && DProduct.Price > 0 && DProduct.InStoke > 0)
            {
                IEnumerable<DO.Product?>? lproduct = _idal?.Product.GetTheList();
                foreach (DO.Product? itemproduct in lproduct)
                {
                    if (itemproduct?.ID == DProduct.ID)
                    {
                        DO.Product tProduct = new DO.Product();
                        tProduct.ID = DProduct.ID;
                        tProduct.NameOfProduct = DProduct.NameOfProduct;
                        tProduct.InStoke = DProduct.InStoke;
                        tProduct.Category = (DO.Enums.Category?)DProduct.Category;
                        tProduct.Price = DProduct.Price;

                        _idal.Product.Update(DProduct.ID, tProduct);
                        return;
                    }
                }
                throw new BO.ExceptionLogi("THE PRODUCT ISNOT EXIST\n");
            }
            throw new BO.ExceptionLogi("CAN'T UPDATE\n");
        }
        //public IEnumerable<BO.ProductForList?> AskForCategory(BO.Enum.category category)///return a list of all the products that match the category
        //{
        //    List<BO.ProductForList> listcategory = new List<BO.ProductForList>();
        //    IEnumerable<BO.ProductForList> listofall = AskForPrudactList();
        //    foreach (var product in listofall)
        //    {
        //        if (product.Category == category)
        //            listcategory.Add(product);
        //    }
        //    return listcategory;
        //}
        public IEnumerable<BO.ProductForList?> GetProducts(Func<DO.Product?, bool>? func)
        {
            List<BO.ProductForList?> productsForList = new List<BO.ProductForList?>();
            List<DO.Product?> products = new List<DO.Product?>();
            products = (List<DO.Product?>)_idal.Product.GetTheList();
            if (func != null)
            {
                foreach (DO.Product? product in products)
                {
                    if (func(product))
                    {

                        productsForList.Add(new BO.ProductForList
                        {
                            ID = (int)product?.ID!,
                            Category = (BO.Enum.category?)product?.Category,
                            NameOfProduct = product?.NameOfProduct,
                            PricePerProduct = (int)product?.Price!
                        });
                    }
                }
                return productsForList;
            }
            else
            {
                foreach (DO.Product? product in products)
                {

                    productsForList.Add(new BO.ProductForList
                    {
                        ID = (int)product?.ID!,
                        Category = (BO.Enum.category?)product?.Category,
                        NameOfProduct = product?.NameOfProduct,
                        PricePerProduct = (int)product?.Price!
                    });

                }
                return productsForList;
            }

        }

    }
}