using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlResponseWritter<TRequest, TResponse> : IResponseWritter<TRequest, TResponse>
        where TResponse : IHtmlResponse
    {
    }
}