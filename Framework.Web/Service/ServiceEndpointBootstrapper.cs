using System;
using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.HttpMethods;
using Framework.Web.Session;

namespace Framework.Web.Service
{
    public interface IServiceEndpointBootstrapper<TResponse>
    {
        void Bootstrap(
            IHttpEndpoint<TResponse> endpoint,
            string routeDescription,
            Func<HttpContext, TResponse> perform = null,
            IList<IBeforePerformAction> beforePerformActions = null,
            IList<IAfterPerformAction> afterPerformActions = null,
            IResponseWritter<TResponse> responseWritter = null);
    }

    public class ServiceEndpointBootstrapper<TResponse> : IServiceEndpointBootstrapper<TResponse>
    {
        private readonly IJsonResponseWritter<TResponse> _jsonResponseWritter;
        private readonly ISessionHandler _sessionHandler;
        private readonly IGetHttpMethod _getHttpMethod;

        public ServiceEndpointBootstrapper(
            IJsonResponseWritter<TResponse> jsonResponseWritter, ISessionHandler sessionHandler, IGetHttpMethod getHttpMethod)
        {
            _jsonResponseWritter = jsonResponseWritter;
            _sessionHandler = sessionHandler;
            _getHttpMethod = getHttpMethod;
        }

        public void Bootstrap(
            IHttpEndpoint<TResponse> endpoint, 
            string routeDescription, 
            Func<HttpContext, TResponse> perform = null, 
            IList<IBeforePerformAction> beforePerformActions = null,
            IList<IAfterPerformAction> afterPerformActions = null, 
            IResponseWritter<TResponse> responseWritter = null)
        {
            endpoint.HttpRequestDescriptor = new GenericRequestDescriptor
            {
                HttpMethod = _getHttpMethod,
                RouteDescription = routeDescription
            };

            endpoint.BeforePerformActions = new List<IBeforePerformAction> { _sessionHandler };
            if (beforePerformActions != null)
            {
                ((List<IBeforePerformAction>)endpoint.BeforePerformActions).AddRange(beforePerformActions);
            }
            endpoint.AfterPerformActions = new List<IAfterPerformAction> { _sessionHandler };
            if (afterPerformActions != null)
            {
                ((List<IAfterPerformAction>)endpoint.AfterPerformActions).AddRange(afterPerformActions);
            }
            endpoint.ResponseWritter = responseWritter ?? _jsonResponseWritter;
        }
    }

    public delegate bool UnbinderDelegate<TRequest>(HttpContext httpContext, IList<string> messages, out TRequest request);
    public interface IServiceEndpointBootstrapper<TRequest, TResponse>
    {
        void Bootstrap(
            IHttpEndpoint<TRequest, TResponse> endpoint, 
            IHttpMethod httpMethod, 
            string routeDescription,
            UnbinderDelegate<TRequest> unbind,
            Func<TRequest, IList<string>, bool> validate = null,
            Func<HttpContext, TRequest, TResponse> perform = null,
            IList<IBeforePerformAction> beforePerformActions = null,
            IList<IAfterPerformAction> afterPerformActions = null,
            IResponseWritter<TResponse> responseWritter = null);
    }

    public class ServiceEndpointBootstrapper<TRequest, TResponse> : IServiceEndpointBootstrapper<TRequest, TResponse>
    {
        private readonly IJsonResponseWritter<TResponse> _jsonResponseWritter;
        private readonly ISessionHandler _sessionHandler;

        public ServiceEndpointBootstrapper(
            IJsonResponseWritter<TResponse> jsonResponseWritter, ISessionHandler sessionHandler)
        {
            _jsonResponseWritter = jsonResponseWritter;
            _sessionHandler = sessionHandler;
        }

        public void Bootstrap(
            IHttpEndpoint<TRequest, TResponse> endpoint, 
            IHttpMethod httpMethod, 
            string routeDescription,
            UnbinderDelegate<TRequest> unbind,
            Func<TRequest, IList<string>, bool> validate = null,
            Func<HttpContext, TRequest, TResponse> perform = null,
            IList<IBeforePerformAction> beforePerformActions = null,
            IList<IAfterPerformAction> afterPerformActions = null,
            IResponseWritter<TResponse> responseWritter = null)
        {
            endpoint.HttpRequestDescriptor = new GenericRequestDescriptor
            {
                HttpMethod = httpMethod,
                RouteDescription = routeDescription
            };
            endpoint.BeforePerformActions = new List<IBeforePerformAction> { _sessionHandler };
            if (beforePerformActions != null)
            {
                ((List<IBeforePerformAction>)endpoint.BeforePerformActions).AddRange(beforePerformActions);
            }
            endpoint.AfterPerformActions = new List<IAfterPerformAction> { _sessionHandler };
            if (afterPerformActions != null)
            {
                ((List<IAfterPerformAction>)endpoint.AfterPerformActions).AddRange(afterPerformActions);
            }
            endpoint.ResponseWritter = responseWritter ?? _jsonResponseWritter;
        }
    }
}