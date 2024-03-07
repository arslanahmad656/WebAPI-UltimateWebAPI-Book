using System.Runtime.Serialization;

namespace Shared.DataTransferObjects;

[DataContract]
public record EmployeeDto
    (
        [property: DataMember] Guid Id, 
        [property: DataMember] string Name, 
        [property: DataMember] int Age, 
        [property: DataMember] string Position
    );
