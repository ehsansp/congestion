using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CodePractice.Core.Admin.Domain.Context;

namespace CodePractice.Core.Admin.Repository;

public class Repository<TEntity> where TEntity : class
{
    private AdminContext DC;
    private DbSet<TEntity> _dbSet;
    public Repository(AdminContext dc)
    {
        DC = dc;
        _dbSet = dc.Set<TEntity>();
    }
    public virtual async Task<TEntity> GetByID(long id, string Include = null, string Relation = null)
    {
        var k = await _dbSet.FindAsync(id);
        if (Include != null)
        {
            var ins = Include.Split(',');
            foreach (var i in ins)
                await DC.Entry(k).Reference(i).LoadAsync();
        }
        if (Relation != null)
        {
            var re = Relation.Split(',');
            foreach (var i in re)
                await DC.Entry(k).Collection(i).LoadAsync();

        }

        return k;
    }
    public virtual void Insert(TEntity entity)
    {
        _dbSet.Add(entity);
    }
    public virtual void InsertRange(List<TEntity> entity)
    {
        _dbSet.AddRangeAsync(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        DC.Entry(entity).State = EntityState.Modified;
    }
    public virtual void Delete(TEntity entity)
    {

        if (DC.Entry(entity).State == EntityState.Deleted)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }
    public virtual void Delete(long id)
    {
        var entity = GetByID(id);
        Delete(entity.Result);
    }
    public virtual Task<int> Save()
    {

        try
        {
            return DC.SaveChangesAsync();
        }
        catch
        {
            return Task.FromResult(0);
        }

    }
    public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, int? page = null, int? pageCont = null, string Include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (Include != null)
        {
            var ins = Include.Split(',');
            foreach (var i in ins)
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
    public virtual IQueryable<T> Select<T>(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, Expression<Func<TEntity, T>> select = null, int? page = null, int? pageCont = null, string Include = null) where T : class
    {
        IQueryable<TEntity> query = _dbSet;
        if (Include != null)
        {
            var ins = Include.Split(',');
            foreach (var i in ins)
                query = query.Include(i);
        }
        IQueryable<T> r = null;
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

        r = query.Select(select);

        return r;
    }
    public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> where = null, string Include = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (Include != null)
        {
            var ins = Include.Split(',');
            foreach (var i in ins)
                query = query.Include(i);
        }
        if (where != null)
        {
            query = query.Where(where);
        }



        return await query.FirstOrDefaultAsync();
    }

    public virtual bool Any(Expression<Func<TEntity, bool>> where = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }


        return query.Any();
    }

    public virtual bool All(Expression<Func<TEntity, bool>> where = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            return query.All(where);
        }


        return false;
    }

    public virtual async Task<long> GetCount(Expression<Func<TEntity, bool>> where = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (where != null)
        {
            return await query.LongCountAsync(where);
        }
        else
        {
            return await query.LongCountAsync();
        }
    }

    public virtual void load(TEntity entity, string rilation)
    {
        var re = rilation.Split(',');
        foreach (var i in re)
            DC.Entry(entity).Reference(i).Load();




    }
    public virtual void loadDetails(TEntity entity, string rilation)
    {
        var re = rilation.Split(',');
        foreach (var i in re)
            DC.Entry(entity).Collection(i).Load();





    }
}
