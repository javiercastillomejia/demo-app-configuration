namespace ApiDemoAppConfigurationNet60.Extensions
{
    public static class AzureAppConfigurationBuilder
    {
        public static IHostBuilder AzureAppConfigurationBuild(this IHostBuilder hostBuilder, string azureAppConnectionString)
        {
            return hostBuilder.ConfigureAppConfiguration(config =>
            {
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
