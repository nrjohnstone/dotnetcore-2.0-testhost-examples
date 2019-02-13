using Microsoft.AspNetCore.Mvc;
using SimpleInjector;

namespace webapi.Ioc
{
    public static class IocConfiguration 
    {
        public static void Initialize(Container container, ISettings settings)
        {
            container.RegisterInstance(settings);

            container.Register<IValueRepository, ValueRepositoryInMemory>();
        }
    }
}