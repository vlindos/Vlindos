using System;
using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.HttpMethods;

namespace Framework.Web.Service
{
    public interface IServiceEndpointBootstrapper<TResponse>
    {
        void Bootstrap(
            IHttpEndpoint<TResponse> endpoint,
            string routeDescription,
            Func<HttpContext, TResponse> perform = null,
            List<IPrePerformAction> prePerformActions = null,
            List<IPostPerformAction> postPerformActions = null,
            IResponseWritter<TResponse> responseWritter = null);
    }

    public class ServiceEndpointBootstrapper<TResponse> : IServiceEndpointBootstrapper<TResponse>
    {
        private readonly IJsonResponseWritter<TResponse> _jsonResponseWritter;
        private readonly IGetHttpMethod _getHttpMethod;

        public ServiceEndpointBootstrapper(IJsonResponseWritter<TResponse> jsonResponseWritter, IGetHttpMethod getHttpMethod)
        {
            _jsonResponseWritter = jsonResponseWritter;
            _getHttpMethod = getHttpMethod;
        }

        public void Bootstrap(
            IHttpEndpoint<TResponse> endpoint, 
            string routeDescription, 
            Func<HttpContext, TResponse> perform = null, 
            List<IPrePerformAction> prePerformActions = null,
            List<IPostPerformAction> postPerformActions = null, 
            IResponseWritter<TResponse> responseWritter = null)
        {
            endpoint.HttpRequestDescriptor = new GenericRequestDescriptor
            {
                HttpMethod = _getHttpMethod,
                RouteDescription = routeDescription
            };

            endpoint.ResponseWritter = responseWritter ?? _jsonResponseWritter;
        }
    }

    public delegate bool UnbinderDelegate<TRequest>(HttpContext httpContext, List<string> messages, out TRequest request);
    public interface IServiceEndpointBootstrapper<TRequest, TResponse>
    {
        void Bootstrap(
            IHttpEndpoint<TRequest, TResponse> endpoint, 
            IHttpMethod httpMethod, 
            string routeDescription,
            UnbinderDelegate<TRequest> unbind,
            Func<TRequest, List<string>, bool> validate = null,
            Func<HttpContext, TRequest, TResponse> perform = null,
            Func<HttpContext, RequestFailedAt, List<string>, TRequest, TResponse> requestFailureHandler = null,
            List<IPrePerformAction> prePerformActions = null,
            List<IPostPerformAction> postPerformActions = null,
            IResponseWritter<TResponse> responseWritter = null);
    }

    public class ServiceEndpointBootstrapper<TRequest, TResponse> : IServiceEndpointBootstrapper<TRequest, TResponse>
    {
        private readonly IJsonResponseWritter<TResponse> _jsonResponseWritter;

        public ServiceEndpointBootstrapper(
            IJsonResponseWritter<TResponse> jsonResponseWritter)
        {
            _jsonResponseWritter = jsonResponseWritter;
        }

        public void Bootstrap(
            IHttpEndpoint<TRequest, TResponse> endpoint, 
            IHttpMethod httpMethod, 
            string routeDescription,
            UnbinderDelegate<TRequest> unbind,
            Func<TRequest, List<string>, bool> validate = null,
            Func<HttpContext, TRequest, TResponse> perform = null,
            Func<HttpContext, RequestFailedAt, List<string>, TRequest, TResponse> requestFailureHandler = null,
            List<IPrePerformAction> prePerformActions = null,
            List<IPostPerformAction> postPerformActions = null,
            IResponseWritter<TResponse> responseWritter = null)
        {
            endpoint.HttpRequestDescriptor = new GenericRequestDescriptor
            {
                HttpMethod = httpMethod,
                RouteDescription = routeDescription
            };
            endpoint.ResponseWritter = responseWritter ?? _jsonResponseWritter;
        }
    }
}