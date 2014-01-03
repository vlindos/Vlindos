using System.Collections.Specialized;
using System.Net;
using System.Net.Mime;
using Framework.Web.HttpMethods;
using Vlindos.Common.Streams;

namespace Framework.Web.Application
{
    public class HttpRequest
    {
        public IHttpMethod HttpMethod { get; set; }

        public IPAddress UserHostAddress { get; set; }

        public bool UsesSsl { get; set; }
        
        public IPAddress ServerHostAddress { get; set; }
        
        public string ServerDomain { get; set; }
        
        public ushort Port { get; set; }

        public string Path { get; set; }

        public NameValueCollection Headers { get; set; }

        public NameValueCollection RoutesValues { get; set; }

        public NameValueCollection QueryString { get; set; }

        public IInputStream InputStream { get; set; }
    }
}
