﻿using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployees(Guid companyId, bool trackChanges);

    Task<EmployeeDto> GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);

    Task<EmployeeDto> CreateEmployee(Guid companyId, CreateEmployeeDto employeeDto,  bool trackChanges);

    Task UpdateEmployee(Guid companyId, Guid employeeId, UpdateEmployeeDto employeeDto, bool trackChanges);

    Task DeleteEmployee(Guid companyId, Guid employeeId, bool trackChanges);
}
