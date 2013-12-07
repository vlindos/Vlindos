﻿using System.Collections.Generic;
using System.Net;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Models;
using Framework.Web.Service.Models;

namespace Framework.Web.Service.Filters
{
    public interface IServiceRequestHandler<TRequest, TResponse> : IAfterPerformHttpEndpointFilter<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
    }

    public class ServiceRequestHandler<TRequest, TResponse> : IServiceRequestHandler<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        public int Priority { get { return 100; } }

        public bool BeforePerform(
            IHttpRequest httpRequest, IHttpResponse httpResponse, IServerSideHttpEndpoint<TRequest, TResponse> httpEndpoint)
        {
            var unbinder = httpEndpoint.HttpRequestUnbinder;

            // does the enpoint expects input?
            if (unbinder == null) return true;

            var messages = new List<string>();
            TRequest request;
            if (unbinder.TryToUnbind(httpRequest, out request, messages) == false)
            {
                messages.Add("Bad request.");
                var response = default(TResponse);
                response.Messages = messages;

                httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                httpResponse.FiltersObjects.Add(messages);

                return false;
            }

            var validator = httpEndpoint.HttpEndpoint.RequestValidator;
            if (validator == null) // does the enpoint requires validation?
            {
                return true;
            }

            if (validator.Validate(request, messages) == false)
            {
                messages.Add("Invalid request.");
                var response = default(TResponse);
                response.Messages = messages;

                httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                httpResponse.FiltersObjects.Add(messages);
                return false;
            }

            httpRequest.FiltersObjects.Add(request);

            return true;
        }
    }
}