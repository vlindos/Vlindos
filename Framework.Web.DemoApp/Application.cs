﻿using System;
using Framework.Web.Application;
using Framework.Web.Tools;
using Vlindos.Common.Logging;
using Vlindos.InversionOfControl;
using Vlindos.InversionOfControl.ConventionConfigurators;
using Vlindos.Logging;
using Vlindos.Logging.Tools;

namespace Framework.Web.DemoApp
{
    public class Application : IApplication, IContainerAccessor
    {
        private ILogger _logger;
        private ISystem _loggingSystem;
        public IContainer Container { get; private set; }
        public ApplicationConfiguration Configuration { get; set; }

        public bool Initialize(out ApplicationConfiguration applicationConfiguration)
        {
            Container = new Container();

            // initialize container
            AppllicationConfigurator.Configure(
                AppDomain.CurrentDomain,
                Container,
                new IConventionConfigurator[] { 
                    new FactoriesConventionConfigurator(), 
                    new SingletonConventionConfigurator() });

            // initialize logger
            _loggingSystem = Container.Resolve<IFileConfigurationLoggingSystemInitializer>()
                                      .GetLoggingSystem();
            _loggingSystem.Start();
            _logger = Container.Resolve<ILogger>();
            _logger.Info("Starting Web Service ...");

            // initialize web application
            applicationConfiguration = Container.Resolve<IDefaultApplicationConfigurationGetter>()
                                                .GetApplicationConfiguration();

            return true;
        }

        public void Shutdown()
        {
            _logger.Info("Stopping Web Service ...");
            _loggingSystem.Stop();
            Container.Dispose();
        }
    }
}
