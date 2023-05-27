using Microsoft.EntityFrameworkCore;

namespace CodePractice.Framework.Common.RepositoryBase;

public abstract class BaseUnitOfWork<TContext> : IDisposable where TContext : DbContext
{
    protected readonly TContext context;
    private bool disposed = false;
    public BaseUnitOfWork(TContext context)
    {
        this.context = context;
    }

    public virtual async Task<int> Save(CancellationToken token = default)
    {
        var entries = context.ChangeTracker.Entries();

        //foreach (var entry in entries)
        //{
        //    //if (entry.State == EntityState.Added)
        //    //{


        //    //}

        //    if (entry.State == EntityState.Modified)
        //    {
        //        var createdByIdExists = entry.Entity.GetType().GetProperty("CreatedById");
        //        var createdOnExists = entry.Entity.GetType().GetProperty("CreatedOn");

        //        if (createdByIdExists != null)
        //        {
        //            context.Entry(entry.Entity).Property("CreatedById").IsModified = false;
        //        }

        //        if (createdOnExists != null)
        //        {
        //            context.Entry(entry.Entity).Property("CreatedOn").IsModified = false; ;
        //        }
        //    }
        //}

        var res = await context.SaveChangesAsync(token);
        return res;


        //context.ChangeTracker.Entries<AuditableEntity>

        //if (res == 0)
        //{
        //    var exceptionInfo = context.ChangeTracker.Entries()
        //        .Where(p => p.State != EntityState.Unchanged).ToList();
        //    throw new Exception("Save Changes Did not work!");
        //}


    }
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
