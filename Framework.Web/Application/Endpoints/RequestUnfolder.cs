using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IRequestUnfolder<TRequest, TResponse>
    {
        bool UnfoldRequest(HttpRequest httpRequest,
                           HttpResponse httpResponse,
                           IEndpoint<TRequest, TResponse> endpoint,
                           out TRequest request);
    }

    public class RequestUnfolder<TRequest, TResponse> : IRequestUnfolder<TRequest, TResponse>
    {
        public bool UnfoldRequest(HttpRequest httpRequest,
                                   HttpResponse httpResponse,
                                   IEndpoint<TRequest, TResponse> endpoint,
                                   out TRequest request)
        {
            var unbinder = endpoint.RequestUnbinder;
            if (unbinder == null) // does the enpoint expects input?
            {
                request = default(TRequest);

                return true;
            }

            request = default(TRequest);
            //var messages = new List<string>();
            //if (unbinder.TryToUnbind(httpRequest, out request, messages) == false)
            //{
            //    messages.Add("Bad request.");
            //    endpointActionResult.EndpointResponse.Errors.AddRange(messages);

            //    httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;

            //    endpointActionResult.ResponseStreamWriter = _responseStreamWritter;

            //    return false;
            //}

            //var validator = endpoint.Descriptor.RequestValidator;
            //if (validator == null) // does the enpoint requires validation?
            //{
            //    return true;
            //}

            //if (endpoint.Descriptor.RequestValidator.Validate(request, messages) == false)
            //{
            //    messages.Add("Invalid request.");
            //    endpointActionResult.EndpointResponse.Errors.AddRange(messages);

            //    httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;

            //    endpointActionResult.ResponseStreamWriter = _responseStreamWritter;
            //    return false;
            //}

            return true;
        }
    }
}