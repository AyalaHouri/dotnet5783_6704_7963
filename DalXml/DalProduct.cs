using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal
{
   
    internal class DalProduct : IProduct
    {
        string productpath = @"Product";
        public int Add(Product product)
        {
            List<DO.Product?> listofproduct = XMLTools.LoudListFromXMLSerializer<DO.Product>(productpath);
            if(listofproduct.FirstOrDefault(item=>item?.ID==product.ID) != null ) {
                throw new MyException("THE PRODUCT IS EXIST, CAN NOT ADD\n");
            }
            listofproduct.Add(product);
            XMLTools.SaveListToXMLSerializer(listofproduct, productpath);
            return product.ID;
        }

        public void Delete(Product product)
        {
            List<DO.Product?> listofproduct = XMLTools.LoudListFromXMLSerializer<DO.Product>(productpath);
            if (listofproduct.RemoveAll(item => item?.ID == product.ID) == 0)
                throw new MyException("THE PRODUCT DOES NOT EXIST\n");
            XMLTools.SaveListToXMLSerializer(listofproduct, productpath);
        }

        public Product GetByID(int ID)
        {
            List<DO.Product?> listofproduct = XMLTools.LoudListFromXMLSerializer<DO.Product>(productpath);
            return listofproduct.FirstOrDefault(item => item?.ID==ID)??throw new MyException("THE ID DOES NOT EXIST");
        }

        public IEnumerable<Product?> GetTheList(Func<Product?, bool>? func = null)
        {
            List<DO.Product?> listofproduct = XMLTools.LoudListFromXMLSerializer<DO.Product>(productpath);
            if (func == null)
                return listofproduct.Select(item => item).OrderBy(item => item?.ID);
            return listofproduct.Where(func).OrderBy(item => item?.ID);
        }

        public void Update(int ID, Product product)
        {
            Delete(product);
            Add(product);
        }
    }
}