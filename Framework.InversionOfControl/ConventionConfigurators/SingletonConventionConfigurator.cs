using System;
using Vlindos.InversionOfControl.LifestyleManagers;

namespace Vlindos.InversionOfControl.ConventionConfigurators
{
    public class SingletonConventionConfigurator : IConventionConfigurator
    {
        public void Configure(IContainer container, Type componentType)
        {
            if (componentType.IsInterface || componentType.IsAbstract) return;

            foreach (var serviceType in componentType.GetInterfaces())
            {
                container.Register(serviceType, componentType, Lifestyle.Singleton,
                    serviceType.FullName + " / " + componentType.FullName);
            }
        }
    }
}
