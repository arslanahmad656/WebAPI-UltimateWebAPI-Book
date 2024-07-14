namespace Shared.DataTransferObjects;

public record CreateCompanyDto(string Name, string Address, string Country, IEnumerable<CreateEmployeeDto> Employees);
