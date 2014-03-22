using System;
using Framework.Web.Application;
using Framework.Web.Tools;
using Vlindos.InversionOfControl;
using Vlindos.InversionOfControl.ConventionConfigurators;
using Vlindos.Logging;
using Vlindos.Logging.Tools;

namespace Framework.Web.DemoApp
{
    public class Application : IApplication
    {
        private ISystem _loggingSystem;
        private Container _container;

        public ApplicationConfiguration  Initialize()
        {
            _container = new Container();

            // initialize container
            AppllicationConfigurator.Configure(
                AppDomain.CurrentDomain,
                _container,
                new IConventionConfigurator[] { 
                    new FactoriesConventionConfigurator(), 
                    new SingletonConventionConfigurator() });

            // initialize logger
            _loggingSystem = _container.Resolve<IFileConfigurationLoggingSystemInitializer>()
                                       .GetLoggingSystem();
            _loggingSystem.Start();
            _loggingSystem.Logger
                          .Info("Starting Web Service ...");

            // initialize web application
            return _container.Resolve<IDefaultApplicationConfigurationGetter>()
                             .GetApplicationConfiguration();
        }

        public void Shutdown()
        {
            _loggingSystem.Logger
                          .Info("Stopping Web Service ...");
            _loggingSystem.Stop();
            _container.Dispose();
        }
    }
}
