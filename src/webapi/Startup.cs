using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using webapi.Ioc;

namespace webapi
{
    public class Startup : IStartup
    {
        protected readonly Container Container = new Container();

        public void Configure(IApplicationBuilder app)
        {
            Settings settings = CreateSettings();

            RegisterDependencies(settings);

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
        /// Allow test projects to override and extend configuration builder
        /// by adding extra providers etc...
        /// </summary>
        /// <returns></returns>
        protected virtual IConfigurationBuilder CreateConfigurationBuilder()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            return builder;
        }

        protected virtual void RegisterOverrides()
        {            
        }

        private void RegisterDependencies(Settings settings)
        {
            IocConfiguration.Initialize(Container, settings);
            RegisterOverrides();
        }

        // Virtual method here allows integration testing of environment specific logic 
        protected virtual IHostingEnvironment GetHostingEnvironment(IApplicationBuilder app)
        {
            return app.ApplicationServices.GetService<IHostingEnvironment>();
        }
    }
}
