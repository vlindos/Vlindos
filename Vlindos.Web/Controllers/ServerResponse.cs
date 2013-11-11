using System.Collections.Generic;
using System.Xml.Serialization;
using Users.Common.Models.Endpoint;

namespace Users.Common.Controllers
{
    public class ServerResponse : EndpointResponse
    {
        private List<string> _messages;
        // TODO: See EndpointResponse class / Errors property's comment
        [XmlElement("Message")]
        public List<string> Messages
        {
            get { return _messages ?? (_messages = new List<string>()); }
            set { _messages = new List<string>(value); }
        }
    }
}
