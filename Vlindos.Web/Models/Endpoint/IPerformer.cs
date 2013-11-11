using System.Web.Mvc;
using Users.Common.Mvc;

namespace Users.Common.Models.Endpoint
{
    public interface IPerformer<in Trequest, Tresponse>
        where Trequest : IEndpointRequest
        where Tresponse : IEndpointResponse
    {
        void Perform(ControllerContext controllerContext,
                     Trequest request,
                     IEndpointActionResult<Tresponse> actionResult);
    }
}