using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.Data;

namespace Service;

internal sealed class CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    : ICompanyService
{
    private readonly ICompanyRepository _companyRepository = repository.GetRepository<ICompanyRepository>();

    public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges)
    {
        var companies = await _companyRepository.GetAllCompanies(trackChanges).ConfigureAwait(false);
        return mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public async Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
        {
            throw new ParameterBadRequestException(nameof(ids), "Paramter is null.");
        }

        var companyEntities = await _companyRepository.GetCompaniesByIds(ids, trackChanges).ConfigureAwait(false);

        if (ids.Count() != companyEntities.Count())
        {
            throw new BadRequestException($"Expected {ids.Count()} companies but received {companyEntities.Count()} companies.");
        }

        return mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
    }

    public async Task<CompanyDto> GetCompanyById(Guid companyId, bool trackChanges)
    {
        var company = await _companyRepository.GetCompanyById(companyId, trackChanges);
        return company is null ? throw new CompanyNotFoundException(companyId) : mapper.Map<CompanyDto>(company);
    }

    public async Task<CompanyDto> CreateCompany(CreateCompanyDto companyDto)
    {
        var company = mapper.Map<Company>(companyDto);
        _companyRepository.CreateCompany(company);
        
        await repository.Save().ConfigureAwait(false);

        return mapper.Map<CompanyDto>(company);
    }

    public async Task<IEnumerable<CompanyDto>> CreateCompanies(IEnumerable<CreateCompanyDto> companies)
    {
        var companyEntities = mapper.Map<IEnumerable<Company>>(companies);
        foreach (var company in companyEntities)
        {
            _companyRepository.CreateCompany(company);
        }

        await repository.Save().ConfigureAwait(false);

        return mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
    }

    public async Task UpdateCompany(Guid companyId, UpdateCompanyDto companyDto, bool trackChanges)
    {
        var company = await _companyRepository.GetCompanyById(companyId, trackChanges).ConfigureAwait(false) ?? throw new CompanyNotFoundException(companyId);

        mapper.Map(companyDto, company);
        
        await repository.Save().ConfigureAwait(false);
    }

    public async Task DeleteCompany(Guid id, bool trackChanges)
    {
        var company = await _companyRepository.GetCompanyById(id, trackChanges).ConfigureAwait(false) ?? throw new CompanyNotFoundException(id);
        _companyRepository.DeleteCompany(company);
        await repository.Save().ConfigureAwait(false);
    }
}
