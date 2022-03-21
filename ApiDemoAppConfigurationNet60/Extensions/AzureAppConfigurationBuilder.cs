using Azure.Identity;

namespace ApiDemoAppConfigurationNet60.Extensions
{
    public static class AzureAppConfigurationBuilder
    {
        public static IHostBuilder AzureAppConfigurationBuild(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration(config =>
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
                    options.ConfigureKeyVault(kv =>
                    {
                        kv.SetCredential(new DefaultAzureCredential());
                    });
                });
            });
        }
    }
}
