using System;
using Framework.Web.Service;

namespace Framework.Web.Tools
{
    public class WebServiceSettings
    {
        public Uri BaseUrl { get; set; }

        public Guid ApiKey { get; set; }

        public string ApiSecret { get; set; }

        public string SessionId { get; set; }

        public SerializationType TransportMethod { get; set; }
    }
}
