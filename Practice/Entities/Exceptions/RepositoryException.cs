namespace Entities.Exceptions;

public class RepositoryException(Type repositoryType, RepositoryExceptionType errorType, Exception? innerException = null) 
    : Exception(CreateMessage(repositoryType, errorType), innerException)
{
    private static string CreateMessage(Type repositoryType, RepositoryExceptionType errorType) => $"""
        There is a problem with {repositoryType.Name} type repository.{errorType switch
    {
        RepositoryExceptionType.NotFound => " The requested type of the repository could not be found.",
        RepositoryExceptionType.ResolutionError => " The requested type of the repository could not be resolved.",
        _ => ""
    }}
        """;
}
