using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
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
}
