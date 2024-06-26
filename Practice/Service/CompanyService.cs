﻿using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

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

    public async Task<CompanyDto> GetCompanyById(Guid companyId, bool trackChanges)
    {
        var company = await _companyRepository.GetCompanyById(companyId, trackChanges);
        return company is null ? throw new CompanyNotFoundException(companyId) : mapper.Map<CompanyDto>(company);
    }

    public async Task<CompanyDto> CreateCompany(CreateCompanyDTO companyDto)
    {
        var company = mapper.Map<Company>(companyDto);
        _companyRepository.CreateCompany(company);
        
        await repository.Save().ConfigureAwait(false);

        return mapper.Map<CompanyDto>(company);
    }
}
