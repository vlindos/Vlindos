using Framework.Web.Application;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service;
using Framework.Web.Service.Models;

namespace Framework.Web.DemoApp.Endpoints.Base
{
    public interface IRestEndpointBootstrapper
    {
        void Bootstrap<TRequest, TResponse>(
            RestEndpointBase<TRequest, TResponse> restEndpoint,
            IHttpMethod getHttpMethod, string route,
            IRequestValidator<TRequest> validator)
            where TResponse : IServiceResponse;
    }

    public class RestEndpointBootstrapper : IRestEndpointBootstrapper
    {
        private readonly IJsonHttpStreamResponseWritterManager _jsonHttpStreamResponseWritterManager;

        public RestEndpointBootstrapper(IJsonHttpStreamResponseWritterManager jsonHttpStreamResponseWritterManagerManager)
        {
            _jsonHttpStreamResponseWritterManager = jsonHttpStreamResponseWritterManagerManager;
        }

        public void Bootstrap<TRequest, TResponse>(
            RestEndpointBase<TRequest, TResponse> restEndpoint, 
            IHttpMethod getHttpMethod, string route, 
            IRequestValidator<TRequest> validator) 
            where TResponse : IServiceResponse
        {
            restEndpoint.HttpStreamResponseWritter = 
                _jsonHttpStreamResponseWritterManager.GetJsonHttpStreamResponseWritter<TResponse>();
        }
    }
}