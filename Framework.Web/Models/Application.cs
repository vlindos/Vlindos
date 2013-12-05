using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.Filters;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Models
{
    public class Application<TEndpoint, TRequest, TResponse>
        where TEndpoint : IServerSideHttpEndpoint<TRequest, TResponse>
    {
        public IPerformerException PerformerException { get; set; }
        public IPerformerManager PerformerManger { get; set; }
        public List<IFilter> GlobalFilters { get; set; }
        public List<TEndpoint> Endpoints { get; set; }
    }
}