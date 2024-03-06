using Contracts;
using Entities.Models;

namespace Repository;
public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    protected CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }
}
