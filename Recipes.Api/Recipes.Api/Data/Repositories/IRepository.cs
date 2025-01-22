using System.Linq.Expressions;

namespace RecipesApi.Data.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<bool> Add(TEntity entity);
    Task<bool> Update(TEntity entity);
    Task<bool> Delete(TEntity entity);
    IQueryable<TEntity> All();
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> where);
}