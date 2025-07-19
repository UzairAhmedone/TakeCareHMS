using System.Linq.Expressions;

namespace TakeCareHms.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationTokens);
    Task<DbResults> UpdateAsync(TEntity entity, CancellationToken cancellationTokens);
    Task<DbResults> AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<DbResults> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<DbResults> DeleteAsync(TEntity entity, CancellationToken cancellationTokens);
}
