using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using static CompanyEmployees.Presentation.Helpers.RouteNames;

namespace CompanyEmployees.Presentation.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController(IServiceManager service)
    : ControllerBase
{
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await service.CompanyService.GetAllCompanies(false).ConfigureAwait(false);
        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = GetCompanyById)]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await service.CompanyService.GetCompanyById(id, false).ConfigureAwait(false);
        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyDTO company)
    {
        if (company is null)
        {
            return BadRequest($"{nameof(company)} is null.");
        }

        var createdCompany = await service.CompanyService.CreateCompany(company).ConfigureAwait(false);

        return CreatedAtRoute(GetCompanyById, new { id = createdCompany.Id }, createdCompany);
    }
}
