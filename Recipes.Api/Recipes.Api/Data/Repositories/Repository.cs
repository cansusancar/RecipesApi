using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace RecipesApi.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DataContext _context;
    private readonly DbSet<TEntity> _dbSet;


    public Repository(DataContext context)
    {
        _context = context;

        _dbSet = _context.Set<TEntity>();
    }

    public async Task<bool> Add(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return await Commit();
    }

    public async Task<bool> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        return await Commit();
    }

    public async Task<bool> Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        return await Commit();
    }

    public IQueryable<TEntity> All()
    {
        return _dbSet;
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where)
    {
        return _dbSet.Where(where);
    }

    private async Task<bool> Commit()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }
}