using Entities.Models;

namespace Contracts;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllEmployees(Guid companyId, bool trackChanges);

    Task<Employee?> GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
}
