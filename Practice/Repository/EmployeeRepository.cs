using Contracts;
using Entities.Models;

namespace Repository;
public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    protected EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }
}
