namespace Framework.Web.HtmlPages
{
    public interface IRendererFactory
    {
        IRenderer GetRenderer();
    }

    public interface IRenderer
    {
        void Render(string format, params object[] args);
        void RenderHtmlPage(IHtmlPage htmlPage);
    }
}