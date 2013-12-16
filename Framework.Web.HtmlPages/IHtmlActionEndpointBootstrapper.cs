using System;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlActionEndpointBootstrapper
    {
        void Bootstrap<TResponse>(
            IServerSideHttpEndpoint notFoundEndpoint, 
            IGetHttpMethod getHttpMethod, 
            string url,
            Action<IHttpRequest<object>, IHttpResponse<TResponse>> perform)
            where TResponse : IHtmlPageViewData;
    }
}