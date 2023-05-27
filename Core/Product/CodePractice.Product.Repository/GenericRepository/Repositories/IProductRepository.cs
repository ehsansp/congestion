using CodePractice.Framework.Common.RepositoryBase;
using CodePractice.Product.Domain.Context;

namespace CodePractice.Product.Repository.GenericRepository.Repositories;

public interface IProductRepository : IRepositoryBase<Domain.Entities.Product, ProductContext>
{
    
}