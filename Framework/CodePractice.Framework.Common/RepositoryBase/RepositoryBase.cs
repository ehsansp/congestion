using CodePractice.Framework.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CodePractice.Framework.Common.RepositoryBase;

public abstract class RepositoryBase<T, TContext> : IRepositoryBase<T, TContext> where T : class where TContext : DbContext
{
    protected TContext RepositoryContext { get; set; }
    public RepositoryBase(TContext repositoryContext)
    {
        RepositoryContext = repositoryContext;
    }
    public IQueryable<T> FindAll() => RepositoryContext.Set<T>();



    public IQueryable<T> Paginated(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, int? page = null, int? pageCont = null,
        params string[] Include)
    {
        var query = RepositoryContext.Set<T>().AsQueryable();
        if (Include != null)
        {
            //var ins = Include.Split(',');
            foreach (var i in Include)
                query = query.Include(i);
        }
        if (where != null)
        {
            query = query.Where(where);

        }

        if (orderby != null)
        {
            if (page.HasValue)
            {
                int? p = 0;
                if (pageCont.HasValue)
                {

                    if (page > 1)
                        p = ((page - 1) * pageCont);
                    query = orderby(query).Skip(p.Value).Take(pageCont.Value);
                }

                else
                {
                    if (page > 1)
                        p = ((page - 1) * 10);
                    query = orderby(query).Skip(p.Value).Take(10);
                }

            }
            else
            {
                query = orderby(query);
            }

        }

        return query;
    }

    public async Task<(IEnumerable<Z>? list, int count)> PaginatedWithTotal<Z, S>(PaginatedRequestObject<T, Z, S> request) where Z : class, new()
    {

        var pageObj = RepositoryContext.Set<T>().Include(x => x).OrderBy(request.OrderBy)
            .Select(request.Select)
            .Skip((request.Page - 1) * request.PageSize).Take(request.PageSize)
            .GroupBy(p => new { Total = RepositoryContext.Set<T>().Count() })
            .FirstOrDefault();
        if (pageObj == null || !pageObj.Any())
        {
            return (null, 0);
        }
        int total = pageObj.Key.Total;
        var res = pageObj.Select(p => p);
        return (res, total);
    }
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        RepositoryContext.Set<T>().Where(expression);
    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
    public void CreateRange(IEnumerable<T> entities)
    {
        RepositoryContext.Set<T>().AddRange(entities);
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
        RepositoryContext.Set<T>().UpdateRange(entities);
    }
    public Task<T> Max()
    {
        return RepositoryContext.Set<T>().MaxAsync();
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        RepositoryContext.Set<T>().RemoveRange(entities);
    }

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);

    public async Task DeleteById(object id)
    {
        try
        {
            var entity = await Find(id);
            RepositoryContext.Set<T>().Remove(entity);
        }
        catch (Exception ex)
        {

            throw new Exception("Error while deleting entity");
        }

    }

    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    public int GetCount(Expression<Func<T, bool>>? expression) => RepositoryContext.Set<T>().Count(expression);
    public void InsertRange(IEnumerable<T> entities) => RepositoryContext.Set<T>().AddRange(entities);

    public ValueTask<T?> Find(object id)
    {
        return RepositoryContext.Set<T>().FindAsync(id);
    }

    public bool Exists(Expression<Func<T, bool>> expression)
    {
        return RepositoryContext.Set<T>().Any(expression);
    }
    public EntityEntry<T> Entry(T en)
    {
        return RepositoryContext.Entry(en);
    }

    public string GetConnectionString()
    {
        return RepositoryContext.Database.GetDbConnection().ConnectionString;
    }
}