using CodePractice.Framework.Common.RepositoryBase;
using CodePractice.Product.Domain.Context;

namespace CodePractice.Product.Repository.GenericRepository.Repositories;

public class ProductRepository : RepositoryBase<Domain.Entities.Product, ProductContext>, IProductRepository
{
    public ProductRepository(ProductContext repositoryContext) : base(repositoryContext)
    {
    }
}