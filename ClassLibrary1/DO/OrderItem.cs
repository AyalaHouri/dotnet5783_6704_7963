
namespace DO;


/// struct for orderitem

public struct OrderItem
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }

    public OrderItem(int ID, int ProductID, int OrderID, double Price, int Amount)
    {
        this.ID = ProductID;
        this.ProductID = ProductID;
        this.OrderID = OrderID;
        this.Price = Price;
        this.Amount = Amount;
    }

    public override string ToString() => $@"OrderIten ID:{ID}
     Product ID:{ProductID}
     Order ID:{OrderID}
     Price:{Price}
     Amount:{Amount}";
}
