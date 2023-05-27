using System.Linq.Expressions;

namespace CodePractice.Core.Admin.Service;

public interface IServiceBase<TEntity> where TEntity : class
{
    Task<TEntity> GetByID(long id);
    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, int? page = null, int? pageCont = null);
    Task<IQueryable<T>> Select<T>(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, Expression<Func<TEntity, T>> select = null, int? page = null, int? pageCont = null) where T : class;

    Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> where = null);
    Task<int> Insert(TEntity entity);
    Task<int> InsertRange(List<TEntity> entity);
    Task<int> Update(TEntity entity);
    Task<int> Delete(long id);
    bool Any(Expression<Func<TEntity, bool>> where = null);
    bool All(Expression<Func<TEntity, bool>> where = null);
}
