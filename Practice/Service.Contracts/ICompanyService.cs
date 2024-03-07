using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges);
}
