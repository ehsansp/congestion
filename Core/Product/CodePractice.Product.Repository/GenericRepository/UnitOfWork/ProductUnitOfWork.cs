using CodePractice.Framework.Common.RepositoryBase;
using CodePractice.Product.Domain.Context;
using CodePractice.Product.Repository.GenericRepository.Repositories;

namespace CodePractice.Product.Repository.GenericRepository.UnitOfWork;

public class ProductUnitOfWork : BaseUnitOfWork<ProductContext>, IProductUnitOfWork
{
    public ProductUnitOfWork(ProductContext context) : base(context)
    {
    }

    private IProductRepository _productRepository;

    public IProductRepository ProductRepository
    {
        get
        {
            if (_productRepository == null)
            {
                _productRepository = new ProductRepository(context);
            }
            return _productRepository;
        }
    }
}