using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.Filters.Global;
using Framework.Web.Models;

namespace Framework.Web.Tools
{
    public interface IDefaultApplicationConfigurationGetter
    {
        ApplicationConfiguration GetApplicationConfiguration();
    }

    public class DefaultApplicationConfigurationGetter : IDefaultApplicationConfigurationGetter
    {
        private readonly IHttpException _httpException;
        private readonly List<IGlobalFilter> _globalFilters;

        public DefaultApplicationConfigurationGetter(
            IHttpException httpException, 
            List<IGlobalFilter> globalFilters)
        {
            _httpException = httpException;
            _globalFilters = globalFilters;
        }

        public ApplicationConfiguration GetApplicationConfiguration()
        {
            return new ApplicationConfiguration
            {
                HttpException = _httpException,
                GlobalFilters = _globalFilters,
            };
        }
    }
}
