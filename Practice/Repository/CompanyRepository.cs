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

    public async Task<Company?> GetCompanyById(Guid companyId, bool trackChanges)
        => await this.FindByCondition(c => c.Id == companyId, trackChanges)
            .SingleOrDefaultAsync().ConfigureAwait(false);

    public void CreateCompany(Company company)
        => this.Create(company);
}
