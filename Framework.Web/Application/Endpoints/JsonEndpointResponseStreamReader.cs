using System;
using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IJsonEndpointResponseStreamReader<T> : IResponseStreamReader<T>
    {
    }

    public class JsonEndpointResponseStreamReader<T> : IJsonEndpointResponseStreamReader<T>
    {
        public bool Read(HttpRequest httpRequest,
                         out T response, 
                         List<string> messages)
        {
            response = default(T);
            try
            {
                //var serializer = new JsonSerializer();
                //var reader = new JsonTextReader(httpRequest.InputStream);
                //response = serializer.Deserialize<T>(reader);
            }
            catch (Exception ex)
            {
                messages.Add("Unable to deserialize response to JSON. Exception:\r\n" + ex.Message);
                return false;
            }

            return true;
        }
    }
}
