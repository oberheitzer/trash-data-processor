using TDP.Http.Helpers;
using TDP.Http.Interfaces;
using TDP.Http.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ServiceCollection AddServices(this ServiceCollection services)
    {
        services.AddHttpClient<IWasteService, WasteService>(httpClient => {
            httpClient.BaseAddress = new Uri(uriString: Constant.BaseUrl);
        });
        return services;
    }
}
