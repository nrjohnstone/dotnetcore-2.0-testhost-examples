using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using WebApi;

namespace webapi.test
{
    /// <summary>
    /// This startup Test Double demonstrates how to override the Startup for the WebApi
    /// and provide a manner in which each unit test can also add custom registrations
    /// </summary>
    public class FlexibleTestStartup : Startup
    {
        private readonly List<Action<Container>> _customRegistrations = new List<Action<Container>>();

        public List<KeyValuePair<string, string>> Settings;

        public FlexibleTestStartup() : base()
        {
            Settings = new List<KeyValuePair<string, string>>();
        }

        public void AddCustomRegistration(Action<Container> iocOverride)
        {
            _customRegistrations.Add(iocOverride);
        }

        protected override IConfigurationBuilder CreateConfigurationBuilder()
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(Settings);
            return builder;
        }

        protected override void RegisterOverridesImplementation()
        {
            foreach (var customRegistration in _customRegistrations)
            {
                customRegistration.Invoke(Container);
            }
        }

        public void AddSetting(string key, string value)
        {
            Settings.Add(new KeyValuePair<string, string>(key,value));
        }
    }
}