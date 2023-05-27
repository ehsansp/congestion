using System.Linq.Expressions;
using CodePractice.Core.Admin.Repository;

namespace CodePractice.Core.Admin.Service;

public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
{
    public virtual async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, int? page = null, int? pageCont = null)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        return (await _m.EntityRepository.Get(where, orderby, page, pageCont)).ToList();
    }

    public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> where = null)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        return await _m.EntityRepository.FirstOrDefault(where);
    }

    public virtual async Task<TEntity> GetByID(long id)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        return await _m.EntityRepository.GetByID(id);
    }

    public virtual async Task<int> Insert(TEntity entity)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        _m.EntityRepository.Insert(entity);
        return await _m.EntityRepository.Save();


    }
    public virtual async Task<int> InsertRange(List<TEntity> entity)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        _m.EntityRepository.InsertRange(entity);
        return await _m.EntityRepository.Save();
    }

    public virtual async Task<int> Update(TEntity entity)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        _m.EntityRepository.Update(entity);
        return await _m.EntityRepository.Save();
    }
    public virtual async Task<int> Delete(long id)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        _m.EntityRepository.Delete(id);
        return await _m.EntityRepository.Save();
    }

    public virtual bool Any(Expression<Func<TEntity, bool>> where = null)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        return _m.EntityRepository.Any(where);
    }

    public virtual bool All(Expression<Func<TEntity, bool>> where = null)
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        return _m.EntityRepository.All(where);
    }

    public virtual async Task<IQueryable<T>> Select<T>(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, Expression<Func<TEntity, T>> select = null, int? page = null, int? pageCont = null) where T : class
    {
        UnitWork<TEntity> _m = new UnitWork<TEntity>();
        var dd = _m.EntityRepository.Select<T>(where, orderby, select, page, pageCont).ToList();
        return dd.AsQueryable();
    }
}
