using System;
using Framework.Web.Service.Models;

namespace Framework.Web.Service.Client.Tools.Models
{
    public class HttpSettings
    {
        public Uri BaseUrl { get; set; }

        public Guid ApiKey { get; set; }

        public SerializationType TransportMethod { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string SessionId { get; set; }
    }
}
