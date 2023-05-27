using CodePractice.Product.Repository.GenericRepository.Repositories;

namespace CodePractice.Product.Repository.GenericRepository.UnitOfWork;

public interface IProductUnitOfWork
{
    public IProductRepository ProductRepository { get; }
    Task<int> Save(CancellationToken token = default);
}