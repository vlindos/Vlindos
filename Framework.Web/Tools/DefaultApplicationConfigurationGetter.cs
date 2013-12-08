using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.Filters.Global;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Tools
{
    public interface IDefaultApplicationConfigurationGetter
    {
        ApplicationConfiguration GetApplicationConfiguration();
    }

    public class DefaultApplicationConfigurationGetter : IDefaultApplicationConfigurationGetter
    {
        private readonly IPerformerManager _performerManager;
        private readonly IPerformerException _performerException;
        private readonly List<IGlobalFilter> _globalFilters;
        private readonly List<IServerSideHttpEndpoint> _endpoints;

        public DefaultApplicationConfigurationGetter(
            IPerformerManager performerManager, 
            IPerformerException performerException, 
            List<IGlobalFilter> globalFilters,
            List<IServerSideHttpEndpoint> endpoints)
        {
            _performerManager = performerManager;
            _performerException = performerException;
            _globalFilters = globalFilters;
            _endpoints = endpoints;
        }

        public ApplicationConfiguration GetApplicationConfiguration()
        {
            return new ApplicationConfiguration
            {
                PerformerManger = _performerManager,
                PerformerException = _performerException,
                Endpoints = _endpoints,
                GlobalFilters = _globalFilters,
            };
        }
    }
}
