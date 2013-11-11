using System.Web.Mvc;
using System.Web.UI;
using Spreed.Common.Web.Filters;
using Users.Common.Attributes;
using Users.Common.Models.Endpoint;
using Users.Common.Mvc;
using Users.Common.Tools;

namespace Users.Common.Controllers
{
// Type is needed for proper resolving of the EndpointController type of Controller
// ReSharper disable UnusedTypeParameter
    public interface IEndpointController<Trequest, Tresponse> : IController
        where Trequest : IEndpointRequest
        where Tresponse : IEndpointResponse
// ReSharper restore UnusedTypeParameter
    {
    }

    public class EndpointController<Trequest, Tresponse> : Controller, IEndpointController<Trequest, Tresponse>
        where Trequest : IEndpointRequest 
        where Tresponse : IEndpointResponse
    {
        private readonly IRequestUnfolder<Trequest, Tresponse> _requestUnfolder;
        private readonly IEndpointActionResultFactory _actionResultFactory;

        public EndpointController(IRequestUnfolder<Trequest, Tresponse> requestUnfolder, 
            IEndpointActionResultFactory actionResultFactory)
        {
            _requestUnfolder = requestUnfolder;
            _actionResultFactory = actionResultFactory;
        }

        [RedirectIfNotSecureFilterAttribute("EndpointFailure", "SslIsRequired")]
        [ApiKeyFilter("EndpointFailure", "ApiKeyIsRequired")]
        [RedirectIfNotInitializedFilter("EndpointFailure", "State")]
        [RedirectIfInitializationFailedFilter("EndpointFailure", "State")]
        [OutputCache(Duration = 0, Location = OutputCacheLocation.None)]
        public ActionResult Perform(IEndpoint<Trequest, Tresponse> endpoint) 
        {
            var endpointActionResult = _actionResultFactory.GetEndpointActionResult<Tresponse>();
            Trequest request;
            if (_requestUnfolder.UnfoldRequest(ControllerContext, endpoint, out request, endpointActionResult) == false)
            {
                return endpointActionResult.ActionResult;
            }

            endpointActionResult.ResponseStreamWriter = endpoint.ResponseStreamWriter;
            endpoint.RequestPerformer.Perform(ControllerContext, request, endpointActionResult);

            return endpointActionResult.ActionResult;
        }
    }
}
