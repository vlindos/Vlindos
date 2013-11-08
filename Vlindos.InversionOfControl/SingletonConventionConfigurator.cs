using System;

namespace Vlindos.DependencyInjection
{
    public class SingletonConventionConfigurator : IConventionConfigurator
    {
        public void Configure(IContainer container, Type componentType)
        {
            if (componentType.IsInterface || componentType.IsAbstract) return;

            foreach (var serviceType in componentType.GetInterfaces())
            {
                container.Register(serviceType, componentType, LifesStyleManagers.Singleton,
                    serviceType.FullName + " / " + componentType.FullName);
            }
        }
    }
}
