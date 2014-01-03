using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlResponseWritter<in THtmlPageResponse, THtmlPageViewData> 
        : IResponseWritter<THtmlPageResponse>
        where THtmlPageResponse : IHtmlPageResponse<THtmlPageViewData>
    {
    }

    public class HtmlResponseWritter<THtmlPageResponse, THtmlPageViewData>  
        : IHtmlResponseWritter<THtmlPageResponse, THtmlPageViewData>
        where THtmlPageResponse : IHtmlPageResponse<THtmlPageViewData>
    {
        private readonly IHtmlPageRenderer _htmlPageRenderer;

        public HtmlResponseWritter(IHtmlPageRenderer htmlPageRenderer)
        {
            _htmlPageRenderer = htmlPageRenderer;
        }

        public void WriteResponse(HttpContext httpContext, THtmlPageResponse response)
        {
            response.HtmlPage.RenderPage(httpContext, _htmlPageRenderer, response.HtmlPageViewData);
        }
    }
}