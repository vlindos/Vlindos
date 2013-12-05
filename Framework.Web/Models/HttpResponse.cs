using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mime;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public interface IHttpResponse
    {
        HttpStatusCode HttpStatusCode { get; set; }

        NameValueCollection Headers { get; set; }

        ContentType ContentType { get; set; }

        IOutputStream OutputStream { get; set; }

        List<object> FiltersObjects { get; set; }
    }

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse()
        {
            HttpStatusCode = HttpStatusCode.OK;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public NameValueCollection Headers { get; set; }

        public ContentType ContentType { get; set; }

        public IOutputStream OutputStream { get; set; }

        public List<object> FiltersObjects { get; set; }
    }
}