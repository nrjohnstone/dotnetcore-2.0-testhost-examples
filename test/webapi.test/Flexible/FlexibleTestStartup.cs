using System;
using System.Collections.Generic;
using SimpleInjector;
using WebApi;

namespace webapi.test.Flexible
{
    /// <summary>
    /// This startup Test Double demonstrates how to override the Startup for the WebApi
    /// and provide a manner in which each unit test can also add custom registrations
    /// </summary>
    public class FlexibleTestStartup : Startup
    {
        private readonly List<Action<Container>> _customRegistrations = new List<Action<Container>>();

        public TestSettings Settings { get; set; }

        public FlexibleTestStartup()
        {
            Settings = new TestSettings();
        }

        public void AddCustomRegistration(Action<Container> iocOverride)
        {
            _customRegistrations.Add(iocOverride);
        }

        protected override ISettings CreateSettings()
        {
            return Settings;
        }
        
        protected override void RegisterOverridesImplementation()
        {
            foreach (var customRegistration in _customRegistrations)
            {
                customRegistration.Invoke(Container);
            }
        }
    }
}