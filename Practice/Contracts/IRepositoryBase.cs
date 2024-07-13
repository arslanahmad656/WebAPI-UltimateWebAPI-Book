using System.Linq.Expressions;

namespace Contracts;

public interface IRepositoryBase<T>
{
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges, params string[] includes);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}
