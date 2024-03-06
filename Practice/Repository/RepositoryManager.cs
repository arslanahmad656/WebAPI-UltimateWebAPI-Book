using Contracts;

namespace Repository;

public class RepositoryManager(RepositoryContext repositoryContext) : IRepositoryManager
{
    private readonly Lazy<ICompanyRepository> companyRepository = new(() => new CompanyRepository(repositoryContext));
    private readonly Lazy<IEmployeeRepository> employeeRepository = new(() => new EmployeeRepository(repositoryContext));

    public ICompanyRepository CompanyRepository => companyRepository.Value;
    public IEmployeeRepository EmployeeRepository => employeeRepository.Value;

    public async Task Save()
        => await repositoryContext.SaveChangesAsync().ConfigureAwait(false);
}
