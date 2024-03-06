using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager(IRepositoryManager repository, ILoggerManager logger)
    : IServiceManager
{
    private readonly Lazy<ICompanyService> companyService = new(() => new CompanyService(repository, logger));
    private readonly Lazy<IEmployeeService> employeeService = new(() => new EmployeeService(repository, logger));

    public ICompanyService CompanyService => companyService.Value;
    public IEmployeeService EmployeeService => employeeService.Value;
}
