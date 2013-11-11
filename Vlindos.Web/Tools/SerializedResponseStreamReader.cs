using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Spreed.Common.Serialization;
using Users.Common.Models.Endpoint;

namespace Users.Common.Tools
{
    public interface ISerializedResponseStreamReader<T> : IResponseStreamReader<T> 
        where T : EndpointResponse
    {
    }

    public class SerializedResponseStreamReader<T> : ISerializedResponseStreamReader<T>
        where T : EndpointResponse
    {
        private readonly ICachedResponseXmlSerializers _serializers;

        public SerializedResponseStreamReader(ICachedResponseXmlSerializers serializers)
        {
            _serializers = serializers;
        }

        public bool Read(StreamReader streamReader, 
                         IHttpSettings httpSettings, 
                         IHttpRequest requestContext,
                         out T serviceResponse, List<string> messages)
        {
            serviceResponse = null;
            try
            {
                switch (httpSettings.TransportMethod)
                {
                    case SerializationFormat.Json:
                        try
                        {
                            var serializer = new JsonSerializer();
                            var reader = new JsonTextReader(streamReader);
                            serviceResponse = serializer.Deserialize<T>(reader);
                        }
                        catch (Exception ex)
                        {
                            messages.Add("Unable to deserialize response to JSON. Exception:\r\n" + ex.Message);
                            return false;
                        }
                        break;
                    //case SerializationFormat.Xml:
                    default:
                        try
                        {
                            var xs = _serializers.GetCachedXmlSerializer(typeof(T));
                            serviceResponse = (T)xs.Deserialize(streamReader);
                        }
                        catch (Exception ex)
                        {
                            messages.Add("Unable to deserialized response to XML. Exception:\r\n" + ex.Message);
                            return false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                messages.Add(string.Format("Unable to read expected response from the network. Exception:\r\n{0}", ex));
                return false;
            }

            return true;
        }
    }
}
