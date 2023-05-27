using Microsoft.EntityFrameworkCore;

namespace CodePractice.Product.Domain.Context;

public class ProductContext: DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {

    }

    public DbSet<Entities.Product> Products { get; set; }
}