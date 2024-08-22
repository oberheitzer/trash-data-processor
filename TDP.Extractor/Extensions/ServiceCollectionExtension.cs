using TDP.Extractor.Interfaces;
using TDP.Extractor.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddExtractorServices(this IServiceCollection services)
    {
        services.AddScoped<ICalendarService, CalendarService>();
        return services;
    }
}
