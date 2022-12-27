using static DO.Enums;

namespace DO;


/// struct for products

public struct Product
{
    public int ID { get; set; }
    public string? NameOfProduct { get; set; }
    public double Price { get; set; }
    public Category? Category { get; set; }
    public int InStoke { get; set; }

    public Product(int id, string name, double price, Category category, int instoke)
    {
        this.ID = id;
        this.NameOfProduct = name;
        this.Price = price;
        this.Category = category;
        this.InStoke = instoke;
    }


    public override string ToString() => $@"
        Product ID:{ID}
        name of produdt: {NameOfProduct}, 
        category : {Category}
    	Price: {Price}
    	Amount in stock: {InStoke}
        ";
}
