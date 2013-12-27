using System.Collections.Generic;
using System.Collections.Specialized;
using Framework.Web.Models.HttpMethods;
using Vlindos.Common.Streams;

namespace Framework.Web.Models
{
    public class HttpRequest
    {
        public IHttpMethod HttpMethod { get; set; }

        public IEnumerable<byte[]> PostData { get; set; }

        public string UserHostAddress { get; set; }

        public string RawUrl { get; set; }
        
        public NameValueCollection Headers { get; set; }

        public NameValueCollection RoutesValues { get; set; }

        public NameValueCollection QueryString { get; set; }

        public IInputStream InputStream { get; set; }

        private Dictionary<string, object> _session;
        public Dictionary<string, object> Session
        {
            get
            {
                return _session ?? (_session = new Dictionary<string, object>());
            }
            set
            {
                _session = value;
            }
        }
    }
}
