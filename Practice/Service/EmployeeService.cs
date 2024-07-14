using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    : IEmployeeService
{
    private readonly ICompanyRepository _companyRepository = repository.GetRepository<ICompanyRepository>();
    private readonly IEmployeeRepository _employeeRepository = repository.GetRepository<IEmployeeRepository>();

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployees(Guid companyId, bool trackChanges)
    {
        _ = await _companyRepository.GetCompanyById(companyId, false).ConfigureAwait(false) ?? throw new CompanyNotFoundException(companyId);
        var employees = await _employeeRepository.GetAllEmployees(companyId, trackChanges).ConfigureAwait(false);

        return mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
    {
        _ = await _companyRepository.GetCompanyById(companyId, false).ConfigureAwait(false) ?? throw new CompanyNotFoundException(companyId);

        var employee = await _employeeRepository.GetEmployee(companyId, employeeId, trackChanges).ConfigureAwait(false);
        return employee is null ? throw new EmployeeNotFoundException(employeeId) : mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> CreateEmployee(Guid companyId, CreateEmployeeDto employeeDto, bool trackChanges)
    {
        _ = await _companyRepository.GetCompanyById(companyId, trackChanges).ConfigureAwait(false) ?? throw new CompanyNotFoundException(companyId);

        var employee = mapper.Map<Employee>(employeeDto);

        _employeeRepository.CreateEmployee(companyId, employee);

        await repository.Save().ConfigureAwait(false);

        return mapper.Map<EmployeeDto>(employee);
    }

    public async Task DeleteEmployee(Guid companyId, Guid employeeId, bool trackChanges)
    {
        _ = await _companyRepository.GetCompanyById(companyId, false).ConfigureAwait(false) ?? throw new CompanyNotFoundException(companyId);

        var employee = await _employeeRepository.GetEmployee(companyId, employeeId, trackChanges).ConfigureAwait(false) ?? throw new EmployeeNotFoundException(employeeId);

        _employeeRepository.DeleteEmployee(employee);

        await repository.Save().ConfigureAwait(false);
    }
}
