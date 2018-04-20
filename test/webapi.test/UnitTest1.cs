using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace webapi.test
{
    public class UnitTest1 : IntegrationTestBase
    {
        [Fact]
        public async Task Test1()
        {
            StartServer();

            var response = await Client.GetAsync("/api/values");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            string[] values = JsonConvert.DeserializeObject<string[]>(json);
            values[0].Should().Be("value1");
            values[1].Should().Be("value2");
        }
    }

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }

    public class IntegrationTestBase
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        public TestStartup IntegrationTestStartup { get; set; }

        public IntegrationTestBase()
        {
            IntegrationTestStartup = new TestStartup(new ConfigurationRoot(new List<IConfigurationProvider>()));
        }

        protected void StartServer()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IStartup>(IntegrationTestStartup);
                });

            builder = WebHost.CreateDefaultBuilder()
                .ConfigureServices(services => { services.AddSingleton<IStartup>(IntegrationTestStartup); })
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(TestStartup).Assembly.FullName);

            _server = new TestServer(builder);

            Client = _server.CreateClient();

            //Client.DefaultRequestHeaders.Add("ApiKey", "Ok");
            //Client.DefaultRequestHeaders.Add("Coolblue-CorrelationId", Guid.NewGuid().ToString());
            //Client.DefaultRequestHeaders.Add("Coolblue-AttemptId", Guid.NewGuid().ToString());
        }
    }
}
