using BlApi;
namespace BlImplementation;

internal class Bl : IBl
{
    public  ICart Cart => new BlImplementation.Cart();
    public  IOrder Order => new BlImplementation.Order();
    public  IProduct Product => new BlImplementation.Product();

}

