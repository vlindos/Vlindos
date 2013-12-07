using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.Filters;

namespace Framework.Web.Models
{
    public class ApplicationConfiguration
    {
        public IPerformerException PerformerException { get; set; }
        public IPerformerManager PerformerManger { get; set; }
        public List<IFilter> GlobalFilters { get; set; }
        public List<object> Endpoints { get; set; }
    }
}