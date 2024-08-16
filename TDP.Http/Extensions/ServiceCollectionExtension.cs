using TDP.Http.Interfaces;
using TDP.Http.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ServiceCollection AddServices(this ServiceCollection services)
    {
        services.AddSingleton<IWasteService, WasteService>();
        services.AddHttpClient<WasteService>();
        return services;
    }   
}
