using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CodePractice.Framework.Dto;

namespace CodePractice.Framework.Common.RepositoryBase;

public interface IRepositoryBase<T, TContext> where T : class where TContext : DbContext
{
    IQueryable<T> FindAll();

    public IQueryable<T> Paginated(Expression<Func<T, bool>> where = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, int? page = null, int? pageCont = null,
        params string[] Include);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    int GetCount(Expression<Func<T, bool>>? expression);
    void Create(T entity);
    void CreateRange(IEnumerable<T> entities);
    void UpdateRange(IEnumerable<T> entities);
    void DeleteRange(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    void InsertRange(IEnumerable<T> entities);
    bool Exists(Expression<Func<T, bool>> expression);

    Task DeleteById(object id);
    EntityEntry<T> Entry(T en);
    Task<T> Max();
    Task<(IEnumerable<Z>? list, int count)> PaginatedWithTotal<Z, S>(PaginatedRequestObject<T, Z, S> request) where Z : class, new();

    ValueTask<T?> Find(object id);

    string GetConnectionString();
}