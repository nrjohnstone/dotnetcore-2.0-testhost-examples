using System;
using System.Collections.Generic;
using FluentAssertions;
using webapi.test.Flexible;
using Xunit;

namespace webapi.test
{
    /// <summary>
    /// Demonstrate how settings values can be overridden for testing
    /// </summary>
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