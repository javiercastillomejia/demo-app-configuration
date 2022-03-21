using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ApiDemoAppConfigurationNetCore31.Extensions
{
    public static class AzureAppConfigurationBuilder
    {
        public static IWebHostBuilder AzureAppConfigurationBuild(this IWebHostBuilder webBuilder)
        {
            return webBuilder.ConfigureAppConfiguration(config =>
            {
                config.Build();
                var azureAppConnectionString = config.Build().GetConnectionString("AppConfig");
                config.AddAzureAppConfiguration(options =>
                {
                    options.Connect(azureAppConnectionString);
                    options.ConfigureRefresh(refresh =>
                    {
                        refresh
                            .Register("Sentinel", true)
                            .SetCacheExpiration(TimeSpan.FromSeconds(5));
                    });
                });
            });
        }
    }
}
