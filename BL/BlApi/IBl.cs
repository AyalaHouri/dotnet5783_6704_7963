
using BO;
namespace BlApi
{
    /// <summary>
    /// In charge of (kind of father of) cart,order,product
    /// </summary>
    public interface IBl
    {
        /// <summary>
        /// the shopping cart
        /// </summary>
        public ICart Cart { get; }
        /// <summary>
        /// order 
        /// </summary>
        public IOrder Order { get; }
        /// <summary>
        /// product
        /// </summary>
        public IProduct Product { get; }
    }
}

