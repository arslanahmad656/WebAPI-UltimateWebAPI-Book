using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    : IServiceManager
{
    private readonly Lazy<ICompanyService> companyService = new(() => new CompanyService(repository, logger, mapper));
    private readonly Lazy<IEmployeeService> employeeService = new(() => new EmployeeService(repository, logger, mapper));

    public ICompanyService CompanyService => companyService.Value;
    public IEmployeeService EmployeeService => employeeService.Value;
}
