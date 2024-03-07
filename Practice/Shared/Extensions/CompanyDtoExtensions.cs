using Shared.DataTransferObjects;

namespace Shared.Extensions;

public static class CompanyDtoExtensions
{
    public static string ToCsv(this CompanyDto company) => $"{company.Id},{company.Name},{company.FullAddress}";
}
