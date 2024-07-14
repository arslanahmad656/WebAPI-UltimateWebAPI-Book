namespace Shared.DataTransferObjects;
public record UpdateCompanyDto(string Name, int Age, string Position, IEnumerable<UpdateEmployeeDto>? Employees);