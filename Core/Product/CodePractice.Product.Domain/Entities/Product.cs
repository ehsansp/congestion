namespace CodePractice.Product.Domain.Entities;

public class Product
{
    public long ProductId { get; set; }
    public string Title { get; set; }
    public int Weight { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
}