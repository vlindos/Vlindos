namespace Framework.Web.HtmlPages
{
    public interface IHtmlPageRenderer
    {
        void Render(string format, params object[] args);
    }
}