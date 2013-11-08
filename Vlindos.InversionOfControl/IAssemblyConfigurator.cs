using System;

namespace Vlindos.InversionOfControl
{
    public interface IAssemblyConfigurator
    {
        void Configure(IContainer container, Type[] assemblyTypes);
    }
}
