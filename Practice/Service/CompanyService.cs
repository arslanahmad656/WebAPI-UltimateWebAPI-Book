using AutoMapper;
using Contracts;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    : ICompanyService
{
    public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges)
    {
		try
		{
			var companies = await repository.Company.GetAllCompanies(trackChanges).ConfigureAwait(false);
			return mapper.Map<IEnumerable<CompanyDto>>(companies);
		}
		catch (Exception ex)
		{
			logger.LogError($"Error occurred while getting all companies. {ex.Message}");
			throw;
		}
    }
}
