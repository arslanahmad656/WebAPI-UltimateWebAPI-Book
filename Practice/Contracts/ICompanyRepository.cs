using Entities.Models;

namespace Contracts;

public interface ICompanyRepository : IRepository
{
    Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);
    Task<IEnumerable<Company>> GetCompaniesByIds(IEnumerable<Guid> ids, bool trackChanges);
    Task<Company?> GetCompanyById(Guid companyId, bool trackChanges);
    void CreateCompany(Company company);
    void DeleteCompany(Company company);
}
