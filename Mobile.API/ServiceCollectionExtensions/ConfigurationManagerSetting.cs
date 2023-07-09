namespace Mobile.API.ServiceCollectionExtensions
{
    static class ConfigurationManagerSetting
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManagerSetting()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }
    }
}
