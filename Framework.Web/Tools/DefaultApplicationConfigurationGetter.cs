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
        private readonly IHttpException _httpException;
        private readonly List<IHttpEndpoint> _httpEndpoints;

        public DefaultApplicationConfigurationGetter(
            IHttpException httpException, 
            List<IHttpEndpoint> httpEndpoints)
        {
            _httpException = httpException;
            _httpEndpoints = httpEndpoints;
        }

        public ApplicationConfiguration GetApplicationConfiguration()
        {
            return new ApplicationConfiguration
            {
                HttpException = _httpException,
                HttpEndpoints = _httpEndpoints
            };
        }
    }
}
