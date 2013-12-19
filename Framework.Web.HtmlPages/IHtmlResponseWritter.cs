using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlResponseWritter<TRequest, TResponse> : IResponseWritter<TRequest, TResponse>
        where TResponse : IHtmlResponse
    {
    }

    public class HtmlResponseWritter<TRequest, TResponse> : IHtmlResponseWritter<TRequest, TResponse>
        where TResponse : IHtmlResponse<IHtmlPageViewData>
    {
        private readonly IRendererFactory _rendererFactory;

        public HtmlResponseWritter(IRendererFactory rendererFactory)
        {
            _rendererFactory = rendererFactory;
        }

        public void WriteResponse(
            IHttpRequest<TRequest> httpRequest, IHttpResponse<TResponse> httpResponse)
        {
            var renderer = _rendererFactory.GetRenderer<TRequest, TResponse>();
            httpResponse.Response.HtmlPage.RenderPage(renderer, httpRequest, httpResponse);
        }
    }
}