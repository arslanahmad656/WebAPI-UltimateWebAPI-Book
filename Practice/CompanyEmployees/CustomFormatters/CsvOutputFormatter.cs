using Microsoft.AspNetCore.Mvc.Formatters;
using Shared.DataTransferObjects;
using Shared.Extensions;
using System.Net.Http.Headers;
using System.Text;

namespace CompanyEmployees.CustomFormatters;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add("text/csv");
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    protected override bool CanWriteType(Type? type)
    {
        if (typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type) || typeof(CompanyDto).IsAssignableFrom(type))
        {
            return base.CanWriteType(type);
        }

        return false;
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var csvText = context.Object switch
        {
            CompanyDto company => company.ToCsv(),
            IEnumerable<CompanyDto> companies => string.Join(Environment.NewLine, companies.Select(c => c.ToCsv())),
            _ => throw new NotSupportedException($"The type {context.Object?.GetType().FullName} cannot be formatted as CSV by this formatter.")
        };

        await context.HttpContext.Response.WriteAsync(csvText ?? "").ConfigureAwait(false);
    }
}
