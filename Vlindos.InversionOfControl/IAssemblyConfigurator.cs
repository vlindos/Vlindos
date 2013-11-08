using System;

namespace Vlindos.DependencyInjection
{
    public interface IAssemblyConfigurator
    {
        void Configure(IContainer container, Type[] assemblyTypes);
    }
}
