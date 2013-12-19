namespace Framework.Web.HtmlPages
{
    public interface IHtmlResponse<TRequest>
    {
        IHtmlPage<TRequest, IHtmlPageViewData> HtmlPage { get; set; }
    }
}