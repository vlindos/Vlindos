namespace Framework.Web.HtmlPages
{
    public interface IRenderer
    {
        void Render(string format, params object[] args);
    }
}