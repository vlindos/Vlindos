using System;
using Spreed.Common.Serialization;

namespace Users.Common.Models.Endpoint
{
    public interface IHttpSettings
    {
        Uri BaseUrl { get; set; }

        Guid ApiKey { get; set; }

        string Username { get; }

        string Password { get; }

        SerializationFormat TransportMethod { get; set; }
    }

    public class HttpSettings : IHttpSettings
    {
        public Uri BaseUrl { get; set; }

        public Guid ApiKey { get; set; }

        public SerializationFormat TransportMethod { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public HttpSettings()
        {
            TransportMethod = SerializationFormat.Xml;
        }
    }
}
