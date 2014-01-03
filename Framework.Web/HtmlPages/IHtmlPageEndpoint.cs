using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlPageEndpoint<THtmlPageViewData>
        : IHttpEndpoint<IHtmlPageResponse<THtmlPageViewData>>
    {
        IHtmlPage<THtmlPageViewData> HtmlPage { get; set; }
    }

    public interface IHtmlPageEndpoint<TRequest, THtmlPageViewData> 
        : IHttpEndpoint<TRequest, IHtmlPageResponse<THtmlPageViewData>>
    {
        IHtmlPage<THtmlPageViewData> HtmlPage { get; set; }
    }
}