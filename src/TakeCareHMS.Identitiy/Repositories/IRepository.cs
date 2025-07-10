using System.Linq.Expressions;

namespace TakeCareHMS.Identitiy;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task AddAsync(List<TEntity> entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(List<TEntity> entities);
    //Task DeleteByIdAsync(int id);  
    Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, bool>>[] wheres);
    Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(params Expression<Func<TEntity, bool>>[] wheres);
    Task<TEntity?> GetAsync(params Expression<Func<TEntity, object>>[] Includes);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<bool?> AnyAsync(Expression<Func<TEntity, bool>> predicate);
}
