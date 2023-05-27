using System.Collections.Generic;
using CodePractice.Core.Admin.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace CodePractice.Core.Admin.Repository;

public class UnitWork<TEntity> where TEntity : class
{
    AdminContext DC = new AdminContext();
    private DbSet<TEntity> dbSet;
    public UnitWork()
    {
        dbSet = DC.Set<TEntity>();
    }
    private Repository<TEntity> _EntityRepository;
    public Repository<TEntity> EntityRepository
    {
        get
        {
            if (_EntityRepository == null)
            {

                _EntityRepository = new Repository<TEntity>(DC);
            }
            return _EntityRepository;
        }
    }

    public void Dispose()
    {
        DC.Dispose();
    }
}
