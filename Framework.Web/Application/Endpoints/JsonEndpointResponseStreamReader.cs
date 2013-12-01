using System;
using System.Collections.Generic;
using Framework.Web.Application.Endpoints.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IJsonEndpointResponseStreamReader<T> : IResponseStreamReader<T>
        where T : IEndpointResponse
    {
    }

    public class JsonEndpointResponseStreamReader<T> : IJsonEndpointResponseStreamReader<T>
        where T : IEndpointResponse
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
