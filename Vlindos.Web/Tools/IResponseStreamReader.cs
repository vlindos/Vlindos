using System.Collections.Generic;
using System.IO;
using Vlindos.Web.Models.Endpoint;

namespace Vlindos.Web.Tools
{
    public interface IResponseStreamReader<T> where T : IEndpointResponse
    {
        bool Read(StreamReader streamReader, 
                  IHttpSettings httpSettings, 
                  IHttpRequest requestContext, 
                  out T serviceResponse, 
                  List<string> messages);
    }
}