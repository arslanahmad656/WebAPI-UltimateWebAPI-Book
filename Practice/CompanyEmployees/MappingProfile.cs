using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployees;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForCtorParam(nameof(CompanyDto.FullAddress), option => option.MapFrom(c => string.Join(' ', c.Address, c.Country)));
    }
}
