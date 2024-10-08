﻿using Microsoft.Extensions.DependencyInjection;

namespace TDP.Main.Helpers;

public static class Starter
{
    public static ServiceProvider Build()
    {
        var services = new ServiceCollection();
        services.AddHttpServices();
        services.AddExtractorServices();
        return services.BuildServiceProvider();
    }
}
