
namespace BlImplementation
{
    public class Config
    {
        private static int ID_Product = 100000;
        public static int get_ID_Product
        {
            get
            {
                ID_Product++;
                return ID_Product;
            }
        }
        private static int ID_Cart = 100000;
        public static int get_ID_Cart
        {
            get
            {
                ID_Cart++;
                return ID_Cart;
            }
        }
        private static int ID_OrderItemBO = 100000;
        public static int get_ID_OrderItemBO
        {
            get
            {
                ID_OrderItemBO++;
                return ID_OrderItemBO;
            }
        }
    }
}

