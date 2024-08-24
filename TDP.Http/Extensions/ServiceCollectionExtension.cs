using TDP.Http.Helpers;
using TDP.Http.Interfaces;
using TDP.Http.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ServiceCollection AddServices(this ServiceCollection services)
    {
        string? supabasUrl = Environment.GetEnvironmentVariable(variable: Constant.SupabaseUrl);
        string? supabasKey = Environment.GetEnvironmentVariable(variable: Constant.SupabaseKey);
        if (string.IsNullOrEmpty(supabasUrl) || string.IsNullOrEmpty(supabasKey))
        {
            string missingVariable = string.IsNullOrEmpty(supabasUrl) ? Constant.SupabaseUrl : Constant.SupabaseKey;
            Console.WriteLine($"{missingVariable} is missing from environment variables!");
        }
        else
        {
            services.AddHttpClient<IDatabaseService, DatabaseService>(httpCLient => {
                httpCLient.BaseAddress = new Uri(uriString: supabasUrl);
                httpCLient.DefaultRequestHeaders.Add(name: Constant.ApiKey, value: supabasKey);
            });
        }
        services.AddHttpClient<IWasteService, WasteService>(httpClient => {
            httpClient.BaseAddress = new Uri(uriString: Constant.BaseUrl);
        });
        return services;
    }
}
