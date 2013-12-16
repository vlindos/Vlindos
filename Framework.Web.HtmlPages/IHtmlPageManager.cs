namespace Framework.Web.HtmlPages
{
    public interface IHtmlPageManager
    {
        IHtmlPage GetHtmlPage(string path);
    }
}