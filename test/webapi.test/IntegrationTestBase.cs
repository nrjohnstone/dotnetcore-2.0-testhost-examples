using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace webapi.test
{
    public class IntegrationTestBase
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public TestStartup IntegrationTestStartup { get; set; }

        public IntegrationTestBase()
        {
            IntegrationTestStartup = new TestStartup();
        }

        protected void StartServer()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .ConfigureServices(services => { services.AddSingleton<IStartup>(IntegrationTestStartup); })
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(TestStartup).Assembly.FullName);

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }
    }
}