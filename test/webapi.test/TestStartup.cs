using System;
using System.Collections.Generic;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration;
using SimpleInjector;
using Action = System.Action;

namespace webapi.test
{
    public class TestStartup : Startup
    {
        private readonly List<Action<Container>> _customRegistrations = new List<Action<Container>>();

        public List<KeyValuePair<string, string>> Settings;

        public TestStartup() : base()
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

        protected override void RegisterOverrides()
        {
            Container.Options.AllowOverridingRegistrations = true;

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