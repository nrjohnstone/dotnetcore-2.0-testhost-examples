using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace webapi.test
{
    public class ConfigurationBuilderCustomizationTests : IntegrationTestBase
    {
        [Fact]
        public async void CanOverrideConfigurationSettings()
        {
            var expectedDataSource = $"Test_{Guid.NewGuid().ToString()}";
            IntegrationTestStartup.AddSetting("SqlServer:DataSource", expectedDataSource);

            StartServer();

            // act
            var response = await Client.GetAsync("/api/values/settings/sqlserver/datasource");

            // assert
            string datasource = await response.Content.ReadAsStringAsync();
            datasource.Should().Be(expectedDataSource);
        }
    }
}