using Framework.Web.Models.HttpMethods;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpEndpoint<in TRequest>
    {
        IHttpMethod[] HttpMethods { get; set; }

        string HttpUrlDescription { get; }

        IRequestValidator<TRequest> RequestValidator { get; }
    }
}