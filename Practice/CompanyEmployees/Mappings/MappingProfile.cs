using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForCtorParam(nameof(CompanyDto.FullAddress), option => option.MapFrom(c => string.Join(' ', c.Address, c.Country)));

        CreateMap<Employee, EmployeeDto>();

        CreateMap<CreateCompanyDto, Company>();

        CreateMap<CreateEmployeeDto, Employee>();

        CreateMap<UpdateEmployeeDto, Employee>();

        CreateMap<UpdateCompanyDto, Company>();
    }
}
