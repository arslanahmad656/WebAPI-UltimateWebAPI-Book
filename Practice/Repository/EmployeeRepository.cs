using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class EmployeeRepository(RepositoryContext repositoryContext) : RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetAllEmployees(Guid companyId, bool trackChanges)
        => await this.FindByCondition(e => e.CompanyId == companyId, trackChanges).ToListAsync().ConfigureAwait(false);

    public async Task<Employee?> GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
        => await this.FindByCondition(e => e.CompanyId == companyId && e.Id == employeeId, false).SingleOrDefaultAsync().ConfigureAwait(false);

    public void CreateEmployee(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }
}
