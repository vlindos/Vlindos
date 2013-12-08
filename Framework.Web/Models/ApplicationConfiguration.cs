using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.Filters.Global;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Models
{
    public class ApplicationConfiguration
    {
        public IPerformerException PerformerException { get; set; }
        public IPerformerManager PerformerManger { get; set; }
        public List<IGlobalFilter> GlobalFilters { get; set; }
        public List<IServerSideHttpEndpoint> Endpoints { get; set; }
    }
}