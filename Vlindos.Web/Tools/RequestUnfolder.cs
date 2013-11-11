using System.Collections.Generic;
using System.Net;
using Vlindos.Web.Models.Endpoint;
using Vlindos.Web.Mvc;

namespace Vlindos.Web.Tools
{
    //public interface IRequestUnfolder<Trequest, Tresponse>
    //    where Trequest : IEndpointRequest
    //    where Tresponse : IEndpointResponse
    //{
    //    bool UnfoldRequest(ControllerContext controllerContext, 
    //                       IEndpoint<Trequest, Tresponse> endpoint, 
    //                       out Trequest request,
    //                       IEndpointActionResult<Tresponse> endpointActionResult);
    //}

    //public class RequestUnfolder<Trequest, Tresponse> : IRequestUnfolder<Trequest, Tresponse>
    //    where Trequest : IEndpointRequest
    //    where Tresponse : IEndpointResponse
    //{
    //    private readonly ISerializedResponseStreamWritter<Tresponse> _responseStreamWritter;

    //    public RequestUnfolder(ISerializedResponseStreamWritter<Tresponse> responseStreamWritter)
    //    {
    //        _responseStreamWritter = responseStreamWritter;
    //    }

    //    public bool UnfoldRequest(ControllerContext controllerContext, 
    //                              IEndpoint<Trequest, Tresponse> endpoint, 
    //                              out Trequest request,
    //                              IEndpointActionResult<Tresponse> endpointActionResult)
    //    {
    //        var unbinder = endpoint.Descriptor.RequestUnbinder;
    //        if (unbinder == null) // endpoint expects input
    //        {
    //            request = default(Trequest);

    //            return true;
    //        }

    //        var messages = new List<string>();
    //        if (unbinder.TryToUnbind(controllerContext, out request, messages) == false)
    //        {
    //            messages.Add("Cannot continue because of the above errors.");
    //            endpointActionResult.EndpointResponse.Errors.AddRange(messages);

    //            endpointActionResult
    //                .SetHttpStatusCode(HttpStatusCode.BadRequest);

    //            endpointActionResult.ResponseStreamWriter = _responseStreamWritter;

    //            return false;
    //        }

    //        var validator = endpoint.Descriptor.RequestValidator;
    //        if (validator == null) // endpoint requires input validation
    //        {
    //            return true;
    //        }

    //        if (endpoint.Descriptor.RequestValidator.Validate(request, messages) == false)
    //        {
    //            messages.Add("Cannot continue because of the above errors.");
    //            endpointActionResult.EndpointResponse.Errors.AddRange(messages);

    //            endpointActionResult
    //                .SetHttpStatusCode(HttpStatusCode.BadRequest);

    //            endpointActionResult.ResponseStreamWriter = _responseStreamWritter;
    //            return false;
    //        }
            
    //        return true;
    //    }
    //}
}