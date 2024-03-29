﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using static CompanyEmployees.Presentation.Helpers.RouteNames;

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

    [HttpGet("{employeeId:guid}", Name = GetEmployeeById)]
    public async Task<IActionResult> GetEmployee(Guid companyId, Guid employeeId)
    {
        var employee = await service.EmployeeService.GetEmployee(companyId, employeeId, false).ConfigureAwait(false);
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] CreateEmployeeDto employeeDto)
    {
        if (employeeDto is null)
        {
            return BadRequest($"{nameof(employeeDto)} is required.");
        }

        var employee = await service.EmployeeService.CreateEmployee(companyId, employeeDto, false).ConfigureAwait(false);

        return CreatedAtRoute(GetEmployeeById, new {companyId, employeeId = employee.Id}, employee);
    }
}
