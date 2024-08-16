using TDP.Http.Interfaces;
using TDP.Http.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddSingleton<IWasteService, WasteService>();
}
