namespace CodePractice.Framework.Dto.ProductService;

public class ProductsListViewModel
{
    public long ProductId { get; set; }
    public string Title { get; set; }
    public int Weight { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
}