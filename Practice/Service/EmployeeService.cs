using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    : IEmployeeService
{
    public async Task<IEnumerable<EmployeeDto>> GetAllEmployees(Guid companyId, bool trackChanges)
    {
        _ = await repository.Company.GetCompanyById(companyId, false).ConfigureAwait(false) ?? throw new CompanyNotFoundException(companyId);
        var employees = await repository.Employee.GetAllEmployees(companyId, trackChanges).ConfigureAwait(false);

        return mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
    {
        _ = await repository.Company.GetCompanyById(companyId, false).ConfigureAwait(false) ?? throw new CompanyNotFoundException(companyId);

        var employee = await repository.Employee.GetEmployee(companyId, employeeId, trackChanges).ConfigureAwait(false);
        return employee is null ? throw new EmployeeNotFoundException(employeeId) : mapper.Map<EmployeeDto>(employee);
    }
}
