using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers;

[ApiController]
[Route("api/companies/{companyId:guid}/employees")]
public class EmployeeController (IServiceManager service)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees(Guid companyId)
    {
        var employees = await service.EmployeeService.GetAllEmployees(companyId, false).ConfigureAwait(false);
        return Ok(employees);
    }

    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetEmployee(Guid companyId, Guid employeeId)
    {
        var employee = await service.EmployeeService.GetEmployee(companyId, employeeId, false).ConfigureAwait(false);
        return Ok(employee);
    }
}
