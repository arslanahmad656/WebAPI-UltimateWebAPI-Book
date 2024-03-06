using Contracts;
using Entities.Models;

namespace Repository;
public class EmployeeRepository(RepositoryContext repositoryContext) : RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
{
    //public async Task<IEnumerable<Employee>> GetAllEmployees(bool trackChanges)
    //    => await this.FindAll(trackChanges)
}
