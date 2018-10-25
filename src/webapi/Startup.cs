using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using webapi.Ioc;

namespace webapi
{
    public class Startup : IStartup
    {
        protected readonly Container Container = new Container();
        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app)
        {
            RegisterDependencies();
            RegisterOverrides();

            IHostingEnvironment env = GetHostingEnvironment(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }

        protected virtual void RegisterOverrides()
        {            
        }

        private void RegisterDependencies()
        {
            IocConfiguration.Initialize(Container);
        }

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            IntegrateSimpleInjector(services);

            return services.BuildServiceProvider();
        }

        // Virtual method here allows integration testing of environment specific logic 
        protected virtual IHostingEnvironment GetHostingEnvironment(IApplicationBuilder app)
        {
            return app.ApplicationServices.GetService<IHostingEnvironment>();
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(Container));

            services.UseSimpleInjectorAspNetRequestScoping(Container);
        }
    }
}
