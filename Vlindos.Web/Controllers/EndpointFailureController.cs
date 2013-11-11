using System.Net;

namespace Vlindos.Web.Controllers
{
    //public interface IEndpointFailureController : IController
    //{
    //}

    //public class EndpointFailureController : Controller, IEndpointFailureController
    //{
    //    private readonly IExtendedLogger _logger;
    //    private readonly IInitializationContext _initializationContext;
    //    private readonly IEndpointActionResultFactory _endpointActionResultFactory;
    //    private readonly ISerializedResponseStreamWritter<ServerResponse> _responseStreamWritter;

    //    public EndpointFailureController(IExtendedLogger logger, 
    //        IInitializationContext initializationContext, 
    //        IEndpointActionResultFactory endpointActionResultFactory,
    //        ISerializedResponseStreamWritter<ServerResponse> responseStreamWritter)
    //    {
    //        _logger = logger;
    //        _initializationContext = initializationContext;
    //        _endpointActionResultFactory = endpointActionResultFactory;
    //        _responseStreamWritter = responseStreamWritter;
    //    }

    //    [OutputCache(Duration = 0, Location = OutputCacheLocation.None)]
    //    public ActionResult State()
    //    {
    //        var aggregationState = _initializationContext.AggregateState;

    //        var endpointActionResult = _endpointActionResultFactory.GetEndpointActionResult<ServerResponse>();
    //        endpointActionResult.ResponseStreamWriter = _responseStreamWritter;

    //        switch (aggregationState)
    //        {
    //            case InitializationState.NotStarted:
    //                endpointActionResult.SetHttpStatusCode(HttpStatusCode.ExpectationFailed);
    //                endpointActionResult.EndpointResponse.Errors.Add("Service is not available. Most likely it is due to shutdown in  process.");
    //                break;
    //            case InitializationState.Failure:
    //                endpointActionResult.SetHttpStatusCode(HttpStatusCode.ExpectationFailed);
    //                endpointActionResult.EndpointResponse.Errors.Add("Service has failed to initialize. Check the logs.");
    //                break;
    //            case InitializationState.Started:
    //                endpointActionResult.SetHttpStatusCode(HttpStatusCode.ExpectationFailed);
    //                endpointActionResult.EndpointResponse.Errors.Add("Service is still initializing... This might take time.");
    //                break;
    //            case InitializationState.Success:
    //                endpointActionResult.EndpointResponse.Messages.Add("Service is initialized and ready to accept requests.");
    //                break;
    //            default:
    //                endpointActionResult.SetHttpStatusCode(HttpStatusCode.ExpectationFailed);
    //                _logger.ErrorFormat("Not supported initialization state hit '{0}'. Check logs.", 
    //                    _initializationContext.AggregateState);

    //                endpointActionResult.EndpointResponse.Errors.Add("Service is unable to initialize. Check logs.");
    //                break;
    //        }

    //        return endpointActionResult.ActionResult;
    //    }

    //    public ActionResult RedirectToState()
    //    {
    //        return RedirectToAction("State", "EndpointFailure");
    //    }
        
    //    public ActionResult InternalServerError()
    //    {
    //        var endpointActionResult = _endpointActionResultFactory.GetEndpointActionResult<ServerResponse>();
    //            endpointActionResult.ResponseStreamWriter = _responseStreamWritter;
    //            endpointActionResult.SetHttpStatusCode(HttpStatusCode.InternalServerError);
    //            endpointActionResult.EndpointResponse.Errors.Add("An internal server error occurred while trying to " +
    //                                                   "process your previous request. Check logs.");
    //        return endpointActionResult.ActionResult;
    //    }

    //    protected ActionResult ForbiddenError(string error)
    //    {
    //        var endpointActionResult = _endpointActionResultFactory.GetEndpointActionResult<ServerResponse>();
    //        endpointActionResult.EndpointResponse.Errors.Add(error);
    //        endpointActionResult.SetHttpStatusCode(HttpStatusCode.Forbidden);
    //        return endpointActionResult.ActionResult;
    //    }

    //    public ActionResult SslIsRequired()
    //    {
    //        const string error = "Service does not accept non ssl http requests for security reasons " +
    //                             "(ie. keeping the Api Key in secret).";
    //        return ForbiddenError(error);
    //    }

    //    public ActionResult ApiKeyIsRequired()
    //    {
    //        const string error = "Service does not accept requests without a valid ApiKey.";
    //        return ForbiddenError(error);
    //    }
    //}
}
