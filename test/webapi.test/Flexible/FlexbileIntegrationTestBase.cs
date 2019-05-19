using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace webapi.test.Flexible
{
    public class FlexbileIntegrationTestBase
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public FlexibleTestStartup IntegrationFlexibleTestStartup { get; set; }

        public FlexbileIntegrationTestBase()
        {
            IntegrationFlexibleTestStartup = new FlexibleTestStartup();
        }

        protected void StartServer()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .ConfigureServices(services => { services.AddSingleton<IStartup>(IntegrationFlexibleTestStartup); })
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(FlexibleTestStartup).Assembly.FullName);

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }
    }
}