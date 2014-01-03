using System;
using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.HttpMethods;

namespace Framework.Web.HtmlPages
{
    public interface IIHtmlPageEndpointBootstrapper<THtmlPageViewData> 
    {
        void Bootstrap(
            IHtmlPageEndpoint<THtmlPageViewData> endpoint,
            string routeDescription,
            IHtmlPage<THtmlPageViewData> htmlPage,
            Func<HttpContext, THtmlPageViewData> perform = null,
            IList<IBeforePerformAction> beforePerformActions = null,
            IList<IAfterPerformAction> afterPerformActions = null);
    }
    public delegate bool UnbinderDelegate<in TRequest>(HttpRequest httpRequest, IList<string> messages, TRequest request);
    public interface IIHtmlPageEndpointBootstrapper<TRequest, THtmlPageViewData> 
    {
        void Bootstrap(
            IHtmlPageEndpoint<TRequest, THtmlPageViewData> endpoint,
            IHttpMethod httpMethod,
            string routeDescription,
            IHtmlPage<THtmlPageViewData> htmlPage,
            UnbinderDelegate<TRequest> unbind,
            Func<TRequest, IList<string>, bool> validate = null,
            Func<HttpContext, TRequest, THtmlPageViewData> perform = null,
            IList<IBeforePerformAction> beforePerformActions = null,
            IList<IAfterPerformAction> afterPerformActions = null);
    }
}