using System.Collections.Generic;
using System.Xml.Serialization;

namespace Users.Common.Models.Endpoint
{
    public interface IEndpointResponse
    {
        bool Success { get; }

        List<string> Errors { get; }
    }

    public class EndpointResponse : IEndpointResponse
    {
        private bool? _success;
        public bool Success 
        {
            get
            {
                if (_success.HasValue == false)
                {
                    _success = Errors.Count == 0;
                }
                return _success.Value;
            } 
            set { _success = value; }
        }


        private List<string> _errors;

        // TODO: 
        // the following attribute allows generic serialization like <Error>1</Error><Error>2</Error> rather than:
        // <Errors><string>1</string><string>2</string></Errors> which is the output behavior by default.
        //
        // After spending almost a day on this, it seems this is acceptable hack 
        // to introduce xml serialization specific functionality in the endpoint response model
        // reason is that the standard xml serialization lacks per serialization working attributes overrites
        // other libraries inveistigation didn't lead to anything good
        // and the last option DTO for response is too expensive for development while not really nessecery
        [XmlElement("Error")]
        public List<string> Errors
        {
            get { return _errors ?? (_errors = new List<string>()); }
            set { _errors = new List<string>(value); }
        }
    }
}
