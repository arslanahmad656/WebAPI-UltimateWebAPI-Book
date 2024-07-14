using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges);

    Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);

    Task<CompanyDto> GetCompanyById(Guid companyId, bool trackChanges);

    Task<CompanyDto> CreateCompany(CreateCompanyDTO companyDto);

    Task<IEnumerable<CompanyDto>> CreateCompanies(IEnumerable<CreateCompanyDTO> companies);
    Task DeleteCompany(Guid id, bool trackChanges);
}
