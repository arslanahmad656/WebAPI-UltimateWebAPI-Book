using System.Runtime.Serialization;

namespace Shared.DataTransferObjects;

[DataContract]
public record CompanyDto
    (
        [property: DataMember] Guid Id, 
        [property: DataMember] string Name, 
        [property: DataMember] string FullAddress,
        [property: DataMember] IEnumerable<EmployeeDto> Employees
    );
