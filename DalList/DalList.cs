using DalApi;
namespace Dal;
internal sealed class DalList : IDal
{
    private static readonly IDal instance =  new DalList();
    public static IDal Instance { get => instance; } 
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();

   private DalList() { }
}