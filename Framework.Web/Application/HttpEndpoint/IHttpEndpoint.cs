namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpEndpoint<in TRequest>
    {
        string HttpUrlDescription { get; }

        IRequestValidator<TRequest> RequestValidator { get; }
    }
}