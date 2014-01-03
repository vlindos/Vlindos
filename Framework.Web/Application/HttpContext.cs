using System.Collections.Generic;

namespace Framework.Web.Application
{
    public class HttpContext
    {
        public Dictionary<IActionObjects, object> ActionObjects { get; set; } 
        public HttpRequest HttpRequest { get; set; }
        public HttpResponse HttpResponse { get; set; }
    }
}
