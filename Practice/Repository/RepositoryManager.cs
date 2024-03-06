using Contracts;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<ICompanyRepository> companyRepository = new(() => new CompanyRepository(repositoryContext));
    private readonly Lazy<IEmployeeRepository> employeeRepository = new(() => new EmployeeRepository(repositoryContext));

    public ICompanyRepository Company => companyRepository.Value;
    public IEmployeeRepository Employee => employeeRepository.Value;

    public async Task Save()
        => await repositoryContext.SaveChangesAsync().ConfigureAwait(false);
}
