using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Tools
{
    public interface IDefaultApplicationConfigurationGetter
    {
        ApplicationConfiguration GetApplicationConfiguration();
    }

    public class DefaultApplicationConfigurationGetter : IDefaultApplicationConfigurationGetter
    {
        private readonly IExceptionHandler _exceptionHandler;
        private readonly List<IHttpEndpoint> _httpEndpoints;

        public DefaultApplicationConfigurationGetter(
            IExceptionHandler exceptionHandler, 
            List<IHttpEndpoint> httpEndpoints)
        {
            _exceptionHandler = exceptionHandler;
            _httpEndpoints = httpEndpoints;
        }

        public ApplicationConfiguration GetApplicationConfiguration()
        {
            return new ApplicationConfiguration
            {
                ExceptionHandler = _exceptionHandler,
                HttpEndpoints = _httpEndpoints
            };
        }
    }
}
