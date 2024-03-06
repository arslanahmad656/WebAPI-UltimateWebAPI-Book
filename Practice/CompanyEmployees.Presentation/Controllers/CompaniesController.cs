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
		try
		{
			var companies = await service.CompanyService.GetAllCompanies(false).ConfigureAwait(false);
			return Ok(companies);
		}
		catch(Exception ex)
		{
			return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
		}
    }
}
