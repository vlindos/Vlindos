using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Mime;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public class HttpRequest
    {
        public HttpMethods HttpMethod { get; set; }

        public string HttpUsername { get; set; }

        public string HttpPassword { get; set; }

        public IEnumerable<byte[]> PostData { get; set; }

        public string UserHostAddress { get; set; }

        public string UserAgent { get; set; }

        public string RawUrl { get; set; }

        public ContentType ContentType { get; set; }

        public NameValueCollection Headers { get; set; }

        public NameValueCollection RoutesValues { get; set; }

        public NameValueCollection QueryString { get; set; }

        public IInputStream InputStream { get; set; }
    }
}
