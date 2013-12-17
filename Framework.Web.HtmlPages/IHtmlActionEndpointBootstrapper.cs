using System;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlActionEndpointBootstrapper
    {
        void Bootstrap<TResponse>(
            IServerSideHttpEndpoint serverSideHttpEndpoint, 
            IHttpMethod httpMethod,
            string routeUrl,
            IHtmlPage<object, TResponse> page, 
            Action<IHttpRequest<object>, IHttpResponse<TResponse>> controller = null)
            where TResponse : IHtmlPageViewData;
    }
}