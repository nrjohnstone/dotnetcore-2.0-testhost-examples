using SimpleInjector;

namespace WebApi.Ioc
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