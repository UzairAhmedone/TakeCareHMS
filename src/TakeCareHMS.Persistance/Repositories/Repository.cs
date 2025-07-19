using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;
using TakeCareHMS.Persistance;

namespace TakeCareHms.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly TakeCareHmsEntityContext context;
    public Repository(TakeCareHmsEntityContext context)
    {
        this.context = context;
    }

    public async Task<DbResults> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var results = new DbResults();
        if (entity == null)
        {
            results.AddError("Entity cannot be null.");
            return results;
        }
        var response = context.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return results;
    }

    public async Task<DbResults> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var results = new DbResults();

        var rowsDeleted = await context.Set<TEntity>()
                                .Where(predicate)
                                .ExecuteDeleteAsync(cancellationToken);

        return results;
    }
    public async Task<DbResults> DeleteAsync(TEntity entity, CancellationToken cancellationTokens)
    {
        var results = new DbResults();
        try
        {
            context.Remove(entity);
            await context.SaveChangesAsync(cancellationTokens);
        }
        catch (Exception)
        {
            results.AddError("An error occurred while updating the entity.");
        }
        return results;
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationTokens)
    {
        return await context.Set<TEntity>()
                            .FirstOrDefaultAsync<TEntity>(predicate, cancellationTokens);
    }

    public async Task<DbResults> UpdateAsync(TEntity entity, CancellationToken cancellationTokens)
    {
        var results = new DbResults();
        try
        {
            context.Update(entity);
            await context.SaveChangesAsync(cancellationTokens);
        }
        catch (Exception)
        {
            results.AddError("An error occurred while updating the entity.");
        }
        return results;
    }
}
