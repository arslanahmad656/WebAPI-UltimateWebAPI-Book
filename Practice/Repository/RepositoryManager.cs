using Contracts;
using Entities.Exceptions;
using System.Reflection;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Dictionary<Type, IRepository> _repositories = [];
    private readonly string repositoryNamespace = "Repository.Repositories";

    public TRepository GetRepository<TRepository>() where TRepository : IRepository
    {
        var requestedRepoType = typeof(TRepository);

        try
        {
            if (_repositories.TryGetValue(requestedRepoType, out var requestedRepo))
            {
                return (TRepository)requestedRepo;
            }

            var requestedRepoConcreteType = Assembly.GetExecutingAssembly().GetTypes()
               .FirstOrDefault(t => !t.IsAbstract && !t.IsInterface && t.IsClass && string.Equals(t.Namespace, repositoryNamespace, StringComparison.Ordinal) && requestedRepoType.IsAssignableFrom(t));
            
            if (requestedRepoConcreteType is null)
            {
                throw new Exception($"Repository of type {requestedRepoType.Name} could not be found.");
            }

            var instance = Activator.CreateInstance(requestedRepoConcreteType, repositoryContext);
            if (instance is null)
            {
                throw new Exception("Instance could not be created.");
            }

            var typedInstance = (TRepository)instance;
            _repositories[requestedRepoType] = typedInstance;
            return typedInstance;
        }
        catch (Exception ex)
        {
            throw new RepositoryException(requestedRepoType, RepositoryExceptionType.ResolutionError, ex);
        }
    }

    public async Task Save()
        => await repositoryContext.SaveChangesAsync().ConfigureAwait(false);
}
