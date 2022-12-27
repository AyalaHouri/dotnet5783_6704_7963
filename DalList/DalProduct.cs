
using DO;
using static DO.Enums;

using DalApi;
namespace Dal;
internal class DalProduct : IProduct
{
    //public DalProduct()
    //{
    //    DataSource.s_Initialize();
    //}
    public int Create(string Name, double Price, Category category, int InStoke)
    {
        int ID = DataSource.Config.get_ID_Product;
        Product product = new Product(ID, Name, Price, category, InStoke);
        DataSource.LProduct[DataSource.Config.I_Product] = product;
        return ID;
    }
    public int Add(Product product)
    {
      //  product.ID = DataSource.Config.get_ID_Product;
        DataSource.LProduct.Add(product);
        DataSource.Config.I_Product++;
        return product.ID;
    }

    public void Update(int ID, Product product)
    {
        Delete(DataSource.searchProduct(ID) ?? throw new MyException("the id is null"));
        product.ID = ID;
        DataSource.LProduct.Add(product);
    }
    public void Delete(Product product)
    {
        DataSource.LProduct.Remove(product);
        DataSource.Config.I_Product--;
    }
    //public Product Read(int I)
    //{
    //    if (I < 0 || I > DataSource.arrProduct.Length) // if the index exist return the details else throw an Error
    //    {
    //        throw new IndexOutOfRangeException("Error");
    //    }
    //    return DataSource.arrProduct[I];
    //}
    public Product GetByID(int ID)
    {
        return DataSource.searchProduct(ID) ?? throw new MyException("the id is null");
    }
    public int ProductLength()
    {
        return DataSource.Config.I_Product;
    }
    public IEnumerable<Product?> GetTheList(Func<Product?, bool>? func = null)
    {
        return DataSource.LProduct;
    }
    //public IEnumerable<ProductItem> GetProductByProductId(int productId)
    //{
    //    return (IEnumerable<ProductItem>)DataSource.LProduct.Where(product => product.ID == productId);
    //}
}



