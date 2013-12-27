using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.Filters.Global;

namespace Framework.Web.Models
{
    public class ApplicationConfiguration
    {
        public IHttpException HttpException { get; set; }
        public List<IGlobalFilter> GlobalFilters { get; set; }
    }
}