using System;
using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Service
{
    public interface IJsonHttpStreamResponseReader<T> : IHttpStreamResponseReader<T>
    {
    }

    public class JsonHttpStreamResponseReader<T> : IJsonHttpStreamResponseReader<T>
    {
        public bool Read(IHttpRequest httpRequest, out T response, List<string> messages)
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
