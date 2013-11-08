using System;
using System.Linq;

namespace Vlindos.InversionOfControl.ConventionConfigurators
{
    public interface IConventionConfigurator
    {
        void Configure(IContainer container, Type component);
    }

    public class FactoriesConventionConfigurator : IConventionConfigurator
    {
        public void Configure(IContainer container, Type component)
        {
            if (!component.Name.EndsWith("Factory") || !component.IsInterface) return;

            var factoryGetMethods = component.GetMethods()
                .Where(x => x.Name.StartsWith("Get") && x.ReturnType.IsInterface);

            container.RegisterAsProxyFactory(component, factoryGetMethods);
        }
    }
}
