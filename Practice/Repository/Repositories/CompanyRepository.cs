using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;
internal sealed class CompanyRepository(RepositoryContext repositoryContext) : RepositoryBase<Company>(repositoryContext), ICompanyRepository
{
    public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges)
        => await FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync().ConfigureAwait(false);

    public async Task<Company?> GetCompanyById(Guid companyId, bool trackChanges)
        => await FindByCondition(c => c.Id == companyId, trackChanges, nameof(Company.Employees))
            .SingleOrDefaultAsync().ConfigureAwait(false);

    public void CreateCompany(Company company)
        => Create(company);
}
