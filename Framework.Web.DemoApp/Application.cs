using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Models;
using Vlindos.Common.Logging;
using Vlindos.InversionOfControl;

namespace Framework.Web.DemoApp
{
    public class Application : IApplication
    {
        private IContainer _container;
        private ILogger _logger;

        public ApplicationConfiguration Configuration { get; set; }

        public bool Initialize()
        {
            _container = new Container();
            _logger = _container.Resolve<ILogger>();

            _logger.Info("Starting Web Service ...");
 
            Configuration = new ApplicationConfiguration
            {
                PerformerManger = new PerformerManager(_container),
                PerformerException = null,
                Endpoints = new List<object> {  },
                GlobalFilters = null,
            };

            return true;
        }

        public void Dispose()
        {
            _logger.Info("Stopping Web Service ...");
            _container.Dispose();
        }
    }
}
