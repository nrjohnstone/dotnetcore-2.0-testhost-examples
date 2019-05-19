using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using webapi.test.TestDoubles;


namespace webapi.test.Simple
{
    public class SimpleIntegrationTestBase
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public SimpleTestStartup Startup { get; set; }

        internal InMemoryValueRepository ValueRepository { get; set; }

        public SimpleIntegrationTestBase()
        {
            Startup = new SimpleTestStartup();

            // Create test doubles, mocks so they can be Arranged before starting WebApi
            ValueRepository = new InMemoryValueRepository();
        }

        protected void StartServer()
        {
            AttachAllOverrides();
            
            string startupAssemblyFullname = Startup.GetType().Assembly.FullName;

            var builder = WebHost.CreateDefaultBuilder()
                .ConfigureServices(services => { services.AddSingleton<IStartup>(Startup); })
                .UseSetting(WebHostDefaults.ApplicationKey, startupAssemblyFullname);

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }

        /// <summary>
        /// Wire up all test doubles and mocks into the SimpleTestStartup before starting
        /// </summary>
        private void AttachAllOverrides()
        {
            Startup.ValueRepository = ValueRepository;
        }
    }
}