using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext repositoryContext;

    protected RepositoryBase(RepositoryContext repositoryContext)
    {
        this.repositoryContext = repositoryContext;
    }

    public IQueryable<T> FindAll(bool trackChanges)
        => trackChanges ? repositoryContext.Set<T>()
        : repositoryContext.Set<T>().AsNoTracking();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges, params string[] includes)
    {
        var query = trackChanges ? repositoryContext.Set<T>().Where(expression)
            : repositoryContext.Set<T>().Where(expression).AsNoTracking();
        if (includes.Length > 0)
        {
            var includesString = string.Join(".", includes);
            query = query.Include(includesString);
        }

        return query;
    }

    public void Create(T entity)
        => repositoryContext.Set<T>().Add(entity);

    public void Update(T entity)
        => repositoryContext.Set<T>().Update(entity);

    public void Delete(T entity)
        => repositoryContext.Set<T>().Remove(entity);
}
