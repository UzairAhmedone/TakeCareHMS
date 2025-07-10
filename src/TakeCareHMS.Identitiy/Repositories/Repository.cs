using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TakeCareHMS.Identitiy;

internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    internal readonly TakeCareHmsIdentityContext _dbContext;

    public Repository(TakeCareHmsIdentityContext context)
    {
        this._dbContext = context;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _ = await _dbContext.Set<TEntity>().AddAsync(entity);
        _ = await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task AddAsync(List<TEntity> entity)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entity);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        _ = _dbContext.Set<TEntity>().Remove(entity);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(List<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
        _ = await _dbContext.SaveChangesAsync();
    }
    //public async Task DeleteByIdAsync(int id)
    //{
    //    var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);

    //    if (entity != null)
    //    {
    //        _dbContext.Remove(entity);
    //        await _dbContext.SaveChangesAsync();
    //    }
    //}

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _ = await _dbContext.SaveChangesAsync();
        return entity;
    }
    //public async Task UpdateAsync(List<TEntity> entity)
    //{
    //    _dbContext.Entry(entity).State = EntityState.Modified;
    //    _ = await _dbContext.SaveChangesAsync();
    //}

    public virtual async Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, bool>>[] wheres)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        foreach (Expression<Func<TEntity, bool>> predicate in wheres)
        {
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
        }

        return await query.AsSplitQuery().ToListAsync();
    }

    public virtual async Task<IList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        foreach (Expression<Func<TEntity, object>> include in includes)
        {
            query = query.Include(include);
        }
        return await query.AsSplitQuery().ToListAsync();
    }

    public virtual async Task<IList<TEntity>> GetAllAsync()
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        return await query.AsSplitQuery().ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(params Expression<Func<TEntity, bool>>[] wheres)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        foreach (Expression<Func<TEntity, bool>> predicate in wheres)
        {
            query = query.Where(predicate);
        }
        return await query.FirstOrDefaultAsync();
    }
    public virtual async Task<bool?> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(predicate);
    }

    public virtual async Task<TEntity?> GetAsync(params Expression<Func<TEntity, object>>[] Includes)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        foreach (Expression<Func<TEntity, object>> predicate in Includes)
        {
            query = query.Include(predicate);
        }
        return await query.FirstOrDefaultAsync();
    }
    public async Task<IList<TEntity>> ExecuteStoredProcedureAllAsync(string spName)
    {
        return await _dbContext.Set<TEntity>().FromSqlRaw($"EXEC {spName}").ToListAsync();
    }


}
