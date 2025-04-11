using System.Linq.Expressions;

namespace ProductAPI.Data.Repositories.Base;

public interface IRepositoryBase<T>
    where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetByFilterAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task SaveChangesAsync();
}
