using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using WebApi;
using Xunit;

namespace webapi.test.Flexible
{
    /// <summary>
    /// These tests demonstrate that it is possible to inject custom registrations into
    /// the startup of the webapi on a per test basis
    /// </summary>
    public class FlexibleIocCustomRegistrationTests : FlexbileIntegrationTestBase
    {
        internal class ValueRepositoryIntegrationTest : IValueRepository
        {
            public string Get(string key)
            {
                return $"integrationValue{key}";
            }
        }

        [Fact]
        public async Task WhenUsingNoOverrides_ShouldUseActualRegistrations()
        {
            StartServer();

            // act
            var response = await Client.GetAsync("/api/values");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            string[] values = JsonConvert.DeserializeObject<string[]>(json);
            values[0].Should().Be("value1");
            values[1].Should().Be("value2");
        }

        [Fact]
        public async Task WhenUsingOverrides_ShouldUseOverrideRegistrations()
        {
            IntegrationFlexibleTestStartup.AddCustomRegistration(container => container.Register<IValueRepository, ValueRepositoryIntegrationTest>());

            StartServer();

            // act
            var response = await Client.GetAsync("/api/values");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            string[] values = JsonConvert.DeserializeObject<string[]>(json);
            values[0].Should().Be("integrationValue1");
            values[1].Should().Be("integrationValue2");
        }
    }
}
