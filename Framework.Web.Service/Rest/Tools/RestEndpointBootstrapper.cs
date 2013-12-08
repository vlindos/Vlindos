﻿using System;
using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Models;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service.Models;
using Framework.Web.Service.Rest.Models;
using Vlindos.InversionOfControl;

namespace Framework.Web.Service.Rest.Tools
{
    public delegate bool UnbinderDelegate<TRequest>(IHttpRequest<TRequest> httpRequest, IList<string> messages);
    public interface IRestEndpointBootstrapper
    {
        void Bootstrap<TRequest, TResponse>(
            RestEndpointBase<TRequest, TResponse> restEndpoint,
            IHttpMethod httpMethod, string route,
            List<IHttpEndpointFilter<TRequest, TResponse>> filters = null,
            UnbinderDelegate<TRequest> unbind = null,
            Func<TRequest, List<string>, bool> validate = null,
            Func<TRequest, TResponse> perform = null,
            Action<IHttpRequest<TRequest>, IHttpResponse<TResponse>> responseWritter = null)
            where TResponse : IServiceResponse;
        void Bootstrap<TRequest, TResponse>(
            RestEndpointBase<TRequest, TResponse> restEndpoint, 
            IHttpEndpoint<TRequest> httpEndpoint,
            List<IHttpEndpointFilter<TRequest, TResponse>> filters = null,
            UnbinderDelegate<TRequest> unbind = null,
            Func<TRequest, TResponse> perform = null,
            Action<IHttpRequest<TRequest>, IHttpResponse<TResponse>> responseWritter = null)
            where TResponse : IServiceResponse;
    }

    public class RestEndpointBootstrapper : IRestEndpointBootstrapper
    {
        private readonly IContainerAccessor _containerAccessor;

        public RestEndpointBootstrapper(IContainerAccessor containerAccessor)
        {
            _containerAccessor = containerAccessor;
        }

        public void Bootstrap<TRequest, TResponse>(
            RestEndpointBase<TRequest, TResponse> restEndpoint, 
            IHttpMethod httpMethod, string route, 
            IRequestValidator<TRequest> validator = null) 
            where TResponse : IServiceResponse
        {
        }

        public void Bootstrap<TRequest, TResponse>(
            RestEndpointBase<TRequest, TResponse> restEndpoint, 
            IHttpMethod httpMethod, string route,
            List<IHttpEndpointFilter<TRequest, TResponse>> filters = null, 
            UnbinderDelegate<TRequest> unbind = null, 
            Func<TRequest, List<string>, bool> validate = null,
            Func<TRequest, TResponse> perform = null,
            Action<IHttpRequest<TRequest>, IHttpResponse<TResponse>> responseWritter = null) 
            where TResponse : IServiceResponse
        {
            throw new NotImplementedException();
        }

        public void Bootstrap<TRequest, TResponse>(
            RestEndpointBase<TRequest, TResponse> restEndpoint, 
            IHttpEndpoint<TRequest> httpEndpoint, 
            List<IHttpEndpointFilter<TRequest, TResponse>> filters = null, 
            UnbinderDelegate<TRequest> unbind = null,
            Func<TRequest, TResponse> perform = null,
            Action<IHttpRequest<TRequest>, IHttpResponse<TResponse>> responseWritter = null) 
            where TResponse : IServiceResponse
        {
            restEndpoint.HttpStreamResponseWritter =
                _containerAccessor.Container.Resolve<IJsonHttpStreamResponseWritter<TRequest, TResponse>>();
            throw new NotImplementedException();
        }
    }
}