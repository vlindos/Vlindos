using System.Collections.Specialized;
using System.Net;
using System.Net.Mime;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public class HttpResponse
    {
        public HttpResponse()
        {
            HttpStatusCode = HttpStatusCode.OK;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public NameValueCollection Headers { get; set; }

        public ContentType ContentType { get; set; }

        public IOutputStream OutputStream { get; set; }
    }
}