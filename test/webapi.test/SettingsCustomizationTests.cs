using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace webapi.test
{
    public class SettingsCustomizationTests : FlexbileIntegrationTestBase
    {
        [Fact]
        public async void CanOverrideSettings()
        {
            var expectedDataSource = $"Test_{Guid.NewGuid().ToString()}";

            IntegrationFlexibleTestStartup.Settings.DataSource = expectedDataSource;

            StartServer();

            // act
            var response = await Client.GetAsync("/api/values/settings/sqlserver/datasource");

            // assert
            string datasource = await response.Content.ReadAsStringAsync();
            datasource.Should().Be(expectedDataSource);
        }
    }
}