using Contracts;
using Entities.Models;
using Service.Contracts;

namespace Service;

internal sealed class CompanyService(IRepositoryManager repository, ILoggerManager logger)
    : ICompanyService
{
    public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges)
    {
		try
		{
			var companies = await repository.Company.GetAllCompanies(trackChanges).ConfigureAwait(false);
			return companies;
		}
		catch (Exception ex)
		{
			logger.LogError($"Error occurred while getting all companies. {ex.Message}");
			throw;
		}
    }
}
