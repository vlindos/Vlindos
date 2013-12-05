using Framework.Web.Application.HttpEndpoint.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpEndpoint<in TRequest, out TResponse>
    {
        HttpUrlDescription HttpUrlDescription { get; }

        IRequestValidator<TRequest> RequestValidator { get; }
    }
}