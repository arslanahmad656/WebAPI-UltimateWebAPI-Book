using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Net;

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await service.CompanyService.GetCompanyById(id, false).ConfigureAwait(false);
        return Ok(company);
    }
}
