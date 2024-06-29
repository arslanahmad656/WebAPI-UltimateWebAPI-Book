namespace Contracts;

public interface IRepositoryManager
{
    TRepository GetRepository<TRepository>() where TRepository : IRepository;

    public Task Save();
}
