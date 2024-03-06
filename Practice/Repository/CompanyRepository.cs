using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
internal sealed class CompanyRepository(RepositoryContext repositoryContext) : RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
    public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges)
        => await this.FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync().ConfigureAwait(false);
}
