using TDP.Http.Helpers;
using TDP.Http.Interfaces;
using TDP.Http.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ServiceCollection AddServices(this ServiceCollection services)
    {
        string? supabaseUrl = Environment.GetEnvironmentVariable(variable: Constant.SupabaseUrl);
        string? supabaseKey = Environment.GetEnvironmentVariable(variable: Constant.SupabaseKey);
        if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
        {
            string missingVariable = string.IsNullOrEmpty(supabaseUrl) ? Constant.SupabaseUrl : Constant.SupabaseKey;
            Console.WriteLine($"{missingVariable} is missing from environment variables!");
        }
        else
        {
            services.AddHttpClient<IDatabaseService, DatabaseService>(httpCLient => {
                httpCLient.BaseAddress = new Uri(uriString: supabaseUrl);
                httpCLient.DefaultRequestHeaders.Add(name: Constant.ApiKey, value: supabaseKey);
            });
        }
        services.AddHttpClient<IWasteService, WasteService>(httpClient => {
            httpClient.BaseAddress = new Uri(uriString: Constant.BaseUrl);
        });
        return services;
    }
}
