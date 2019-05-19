using WebApi;
using webapi.test.TestDoubles;

namespace webapi.test.Simple
{

    /// <summary>
    /// Demonstrate that if no customization on a per test basis is required then
    /// a simple implementation where all tests use the same overrides is easier.
    /// This is my preferred approach for everything currently.
    /// </summary>
    public class SimpleTestStartup : Startup
    {
        public TestSettings Settings { get; set; }

        internal InMemoryValueRepository ValueRepository { get; set; }

        public SimpleTestStartup()
        {
            Settings = new TestSettings();
        }

        protected override ISettings CreateSettings()
        {
            return Settings;
        }

        protected override void RegisterOverridesImplementation()
        {
            Container.RegisterInstance<IValueRepository>(ValueRepository);
        }
    }
}