using System;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
    public class Settings : ISettings
    {
        public Settings(IConfigurationBuilder configurationBuilder)
        {
            if (configurationBuilder == null) throw new ArgumentNullException(nameof(configurationBuilder));
            var configuration = configurationBuilder.Build();

            DataSource = configuration["SqlServer:DataSource"];
        }
        
        public string DataSource { get; }

    }
}