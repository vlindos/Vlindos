using Framework.Web.Models;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlPage<TRequest, TResponse>
        where TResponse : IHtmlPageViewData
    {
        void RenderPage(
            IRenderer renderer,
            IHttpRequest<TRequest> httpRequest,
            IHttpResponse<TResponse> httpResponse);
    }
}