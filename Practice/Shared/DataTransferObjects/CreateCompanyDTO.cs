namespace Shared.DataTransferObjects;

public record CreateCompanyDTO(string Name, string Address, string Country, IEnumerable<CreateEmployeeDto> Employees);
