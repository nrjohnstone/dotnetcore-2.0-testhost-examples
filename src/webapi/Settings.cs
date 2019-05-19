using System;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
    public class Settings : ISettings
    {
        private Settings(IConfigurationBuilder configurationBuilder)
        {
            if (configurationBuilder == null) throw new ArgumentNullException(nameof(configurationBuilder));
            var configuration = configurationBuilder.Build();

            DataSource = configuration["SqlServer:DataSource"];
        }

        public static Settings Create()
        {
            var configurationBuilder = CreateBuilder();
            return new Settings(configurationBuilder);
        }

        private static IConfigurationBuilder CreateBuilder()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            return builder;
        }

        public string DataSource { get; private set; }

    }
}