using System.Collections.Generic;
using System.Collections.Specialized;
using Framework.Web.Models.HttpMethods;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public interface IHttpRequest
    {
        IHttpMethod HttpMethod { get; set; }

        IEnumerable<byte[]> PostData { get; set; }

        string UserHostAddress { get; set; }

        string RawUrl { get; set; }

        NameValueCollection Headers { get; set; }

        NameValueCollection RoutesValues { get; set; }

        NameValueCollection QueryString { get; set; }

        IInputStream InputStream { get; set; }

        List<object> FiltersObjects { get; set; }
    }

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest()
        {
            FiltersObjects = new List<object>();
        }

        public IHttpMethod HttpMethod { get; set; }

        public IEnumerable<byte[]> PostData { get; set; }

        public string UserHostAddress { get; set; }

        public string RawUrl { get; set; }
        
        public NameValueCollection Headers { get; set; }

        public NameValueCollection RoutesValues { get; set; }

        public NameValueCollection QueryString { get; set; }

        public IInputStream InputStream { get; set; }

        public List<object> FiltersObjects { get; set; }
    }
}
