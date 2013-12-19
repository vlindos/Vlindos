namespace Framework.Web.HtmlPages
{
    public interface IRendererFactory
    {
        IRenderer<TRequest, TResponse> GetRenderer<TRequest, TResponse>()
            where TResponse : IHtmlPageViewData;
    }

    public interface IRenderer<TRequest, TResponse>
        where TResponse : IHtmlPageViewData
    {
        void Render(string format, params object[] args);
        void RenderHtmlPage(IHtmlPage<TRequest, TResponse> htmlPage);
    }
}