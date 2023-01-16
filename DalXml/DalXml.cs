
using DalApi;
namespace Dal
{
    sealed internal class DalXml : IDal
    {
        private DalXml() { }
        public static IDal Instance { get; } = new DalXml();
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();
        public IProduct Product => new DalProduct();
    }
}
