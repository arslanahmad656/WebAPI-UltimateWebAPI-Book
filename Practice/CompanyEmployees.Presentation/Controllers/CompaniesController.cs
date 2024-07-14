using CompanyEmployees.Presentation.ModelBinders;
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
    [HttpGet]
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

    [HttpGet("collection/({ids})", Name = GetCompaniesByIds)]
    public async Task<IActionResult> GetByIds([ModelBinder(typeof(EnumerableModelBinder))]IEnumerable<Guid> ids)
    {
        var companies = await service.CompanyService.GetByIds(ids, false).ConfigureAwait(false);
        return Ok(companies);
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

    [HttpPost("collection")]
    public async Task<IActionResult> CreateCompanies([FromBody] IEnumerable<CreateCompanyDTO> companies)
    {
        if (companies is null)
        {
            return BadRequest("Companies is null.");
        }

        var createdCompanies = await service.CompanyService.CreateCompanies(companies).ConfigureAwait(false);

        return CreatedAtRoute(GetCompaniesByIds, new { ids = string.Join(",", createdCompanies.Select(c => c.Id)) }, createdCompanies);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await service.CompanyService.DeleteCompany(id, true).ConfigureAwait(false);
        return NoContent();
    }
}
