using System;
using Vlindos.InversionOfControl;
using Vlindos.InversionOfControl.ConventionConfigurators;

namespace Vlindos.DemoApp
{
    class Bootstrapper
    {
        static void Main(string[] args)
        {
            var container = new Container();

            AppllicationConfigurator.Configure(
                AppDomain.CurrentDomain, 
                container, 
                new IConventionConfigurator[] { 
                    new FactoriesConventionConfigurator(), 
                    new SingletonConventionConfigurator() });

            var application = container.Resolve<IApplication>();
            application.Run(args);
        }
    }
}
