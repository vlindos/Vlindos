using System;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlActionEndpointBootstrapper
    {
        void Bootstrap<TRequest, TResponse>(
            IServerSideHttpEndpoint<TRequest> serverSideHttpEndpoint, 
            IHttpMethod httpMethod,
            string routeUrl,
            IHtmlPage page, 
            Action<HttpRequest, HttpResponse> controller = null)
            where TResponse : IHtmlPageViewData;
    }
}