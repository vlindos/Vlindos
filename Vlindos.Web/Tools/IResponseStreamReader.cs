using System.Collections.Generic;
using System.IO;
using Users.Common.Models.Endpoint;

namespace Users.Common.Tools
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