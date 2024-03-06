using Entities.Models;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);
}
