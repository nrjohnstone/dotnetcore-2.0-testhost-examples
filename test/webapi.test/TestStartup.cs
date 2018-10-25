using System;
using System.Collections.Generic;
using Microsoft.Azure.KeyVault.Models;
using SimpleInjector;
using Action = System.Action;

namespace webapi.test
{
    public class TestStartup : Startup
    {
        private readonly List<Action<Container>> _customRegistrations = new List<Action<Container>>();

        public TestStartup() : base()
        {
        }

        public void AddCustomRegistration(Action<Container> iocOverride)
        {
            _customRegistrations.Add(iocOverride);
        }

        protected override void RegisterOverrides()
        {
            Container.Options.AllowOverridingRegistrations = true;

            foreach (var customRegistration in _customRegistrations)
            {
                customRegistration.Invoke(Container);
            }
        }
    }
}