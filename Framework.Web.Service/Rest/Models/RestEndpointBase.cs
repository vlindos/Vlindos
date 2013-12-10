﻿using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service.Models;

namespace Framework.Web.Service.Rest.Models
{
    public abstract class RestEndpointBase<TRequest, TResponse> : IServerSideHttpEndpoint<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        public IHttpMethod[] HttpMethods { get; set; }
        public IHttpEndpoint<TRequest> HttpEndpoint { get; set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }
        public IResponseWritter<TRequest, TResponse> ResponseWritter { get; set; }
        public IRequestPerformer<TResponse> RequestPerformer { get; set; }
        public List<IHttpEndpointFilter> Filters { get; set; }
    }
}
