
using BO;
namespace BlApi
{
    public interface IProduct
    {
        //Product method declarations
        IEnumerable<ProductForList?> AskForPrudactList();
        //IEnumerable<ProductForList?> AskForCategory(BO.Enum.category category );
        DO.Product exist(int orderid);
        BO.Product prudactrequest(int idprudact);
        BO.ProductItem askedProductItem(int prudactid, BO.Cart cart);
        void AddProduct(BO.Product BProduct);
        void DeleteProduct(int IDProduct);
        void updateProduct(BO.Product DProduct);
        public IEnumerable<BO.ProductForList?> GetProducts(Func<DO.Product?, bool>? func);
        //public bool existbool(int productid);

        ///IEnumerable<ProductItem> GetOrderItemsByProductId(int productId);
    }
}


