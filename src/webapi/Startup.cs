using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using WebApi.Ioc;

namespace WebApi
{
    public class Startup : IStartup
    {
        protected readonly Container Container = new Container();

        public void Configure(IApplicationBuilder app)
        {
            Settings settings = CreateSettings();

            RegisterDependencies(settings);
            RegisterOverrides();

            IHostingEnvironment env = GetHostingEnvironment(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddSimpleInjector(Container);

            return services.BuildServiceProvider();
        }

        protected Settings CreateSettings()
        {
            var configurationBuilder = CreateConfigurationBuilder();
            return new Settings(configurationBuilder);
        }

        /// <summary>
        /// Test Seam:
        /// Allow test projects to override and extend configuration builder
        /// by adding extra providers etc...
        /// </summary>
        protected virtual IConfigurationBuilder CreateConfigurationBuilder()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            return builder;
        }

        private void RegisterDependencies(Settings settings)
        {
            IocConfiguration.Initialize(Container, settings);
        }

        private void RegisterOverrides()
        {
            Container.Options.AllowOverridingRegistrations = true;
            RegisterOverridesImplementation();
            Container.Options.AllowOverridingRegistrations = false;
        }

        /// <summary>
        /// Test Seam:
        /// Allow test projects access to the Container directly so
        /// that test projects can replace existing registrations
        /// </summary>
        protected virtual void RegisterOverridesImplementation()
        {

        }

        /// <summary>
        /// Test Seam:
        /// Allow test projects to return IHostingEnvironment for testing
        /// specific scenarios around Host Environment values
        /// </summary>
        protected virtual IHostingEnvironment GetHostingEnvironment(IApplicationBuilder app)
        {
            return app.ApplicationServices.GetService<IHostingEnvironment>();
        }
    }
}
