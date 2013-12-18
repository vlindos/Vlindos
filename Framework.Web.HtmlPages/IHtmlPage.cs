using Framework.Web.Models;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlPage<TRequest, TResponse>
        where TResponse : IHtmlPageViewData
    {
        void RenderPage(
            IRenderer<object, TResponse> renderer,
            IHttpRequest<TRequest> httpRequest,
            IHttpResponse<TResponse> httpResponse);
    }
}