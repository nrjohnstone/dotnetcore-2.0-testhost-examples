using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Localization;
using Newtonsoft.Json;
using webapi.test.Flexible;
using webapi.test.TestDoubles;
using Xunit;

namespace webapi.test.Simple
{
    /// <summary>
    /// Demonstrate that if no per test customization of the IOC container is required
    /// then a simple implementation is easier to understand
    /// </summary>
    public class SimpleIocCustomRegistrationTests : SimpleIntegrationTestBase
    {
        [Fact]
        public async Task ShouldUseOverrideRegistrations()
        {
            ValueRepository.Values["1"] = "CustomValueFor1";
            ValueRepository.Values["2"] = "CustomValueFor2";

            StartServer();

            // act
            var response = await Client.GetAsync("/api/values");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            string[] values = JsonConvert.DeserializeObject<string[]>(json);
            values[0].Should().Be("InMemoryValueRepository:CustomValueFor1");
            values[1].Should().Be("InMemoryValueRepository:CustomValueFor2");
        }

        [Fact]
        public async Task LateOverride_ShouldUseLateImplementationForRegistration()
        {
            ValueRepository.Values["1"] = "CustomValueFor1";
            ValueRepository.Values["2"] = "CustomValueFor2";

            // We can also replace our default test implementation here if we want to
            var newImMemoryRepository = new InMemoryValueRepository();
            ValueRepository = newImMemoryRepository;
            newImMemoryRepository.Values["1"] = "NewCustomValueFor3";
            newImMemoryRepository.Values["2"] = "NewCustomValueFor4";

            StartServer();

            // act
            var response = await Client.GetAsync("/api/values");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var json = await response.Content.ReadAsStringAsync();
            string[] values = JsonConvert.DeserializeObject<string[]>(json);
            values[0].Should().Be("InMemoryValueRepository:NewCustomValueFor3");
            values[1].Should().Be("InMemoryValueRepository:NewCustomValueFor4");
        }

    }
}