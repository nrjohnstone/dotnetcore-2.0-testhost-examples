using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace webapi
{
    internal static class ServiceCollectionExtensions
    {
        public static void AddSimpleInjector(this IServiceCollection services, Container container)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }
    }
}