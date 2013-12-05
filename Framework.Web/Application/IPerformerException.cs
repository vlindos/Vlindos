using System;
using System.Collections.Generic;
using Framework.Web.Application.Filters;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.Application
{
    public interface IPerformerException
    {
        void OnException(
            IHttpRequest request, 
            IHttpResponse httpResponse, 
            IServerSideHttpEndpointDescriptor endpointDescriptor, 
            Exception exception);
    }

    public interface IApplication
    {
        IPerformerException PerformerException { get; set; }
        IPerformerManager PerformerManger { get; set; }
        List<IFilter> GlobalFilters { get; set; }
        List<IServerSideHttpEndpointDescriptor> Endpoints { get; set; }
    }

    public interface IPerformerManager
    {
        //TPerformer GetPerformer<TPerformer>(IHttpRequest request, IHttpResponse httpResponse)
        //    where TPerformer : IRequestPerformer<TResponse>
        //    where TResponse : object;
    }


    public interface IApplicationDelegate<T> where T : IApplication
    {
        bool Start(T application);
        bool Shutdown(T application);
    }
}
