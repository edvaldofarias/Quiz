using System.Globalization;

namespace Quiz.WebApi.Infrastructures.Services.Culture;

[ExcludeFromCodeCoverage]
internal static class CultureService
{
    internal static void AddCultureService(this IServiceCollection _)
    {
        var cultureInfo = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
    }
}