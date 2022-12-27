namespace DO;
/// struct for orders

public struct Order
{
    Random random = new Random();
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }

    public Order(int id, string customerName, string customerEmail, string customerAddress)
    {

        this.ID = id;
        this.CustomerName = customerName;
        this.CustomerEmail = customerEmail;
        this.CustomerAddress = customerEmail;
        this.OrderDate = DateTime.Now;
        this.ShipDate = this.OrderDate + new TimeSpan(random.Next(0, 2), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59)); // random time from the order time 
        this.DeliveryDate = this.ShipDate + new TimeSpan(random.Next(0, 7), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59)); // random time from the ship time
    }
    public override string ToString() => $@"Order ID:{ID}
     CustomerName={CustomerName}
     CustomerEmail={CustomerEmail}
     CustomerAdress={CustomerAddress}
     OrderDate={OrderDate}
     ShipDate={ShipDate}
     DeliveryDate={DeliveryDate}";
}



