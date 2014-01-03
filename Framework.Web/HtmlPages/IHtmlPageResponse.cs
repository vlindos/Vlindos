namespace Framework.Web.HtmlPages
{
    public interface IHtmlPageResponse<THtmlPageViewData>
    {
        IHtmlPage<THtmlPageViewData> HtmlPage { get; set; }
        THtmlPageViewData HtmlPageViewData { get; set; }
    }
}