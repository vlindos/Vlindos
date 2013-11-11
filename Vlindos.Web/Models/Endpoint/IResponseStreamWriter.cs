using System.Web.Mvc;

namespace Users.Common.Models.Endpoint
{
    public interface IResponseStreamWriter<in T> 
        where T : IEndpointResponse
    {
        void WriteResponse(ControllerContext controllerContext, T serviceResponse);
    }
}
