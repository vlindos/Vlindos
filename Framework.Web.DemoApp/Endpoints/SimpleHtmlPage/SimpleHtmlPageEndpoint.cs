using Framework.Web.HtmlPages;

namespace Framework.Web.DemoApp.Endpoints.SimpleHtmlPage
{
    public interface ISimpleHtmlPage : IHtmlPage<string[]>
    {
    }

    public class SimpleHtmlPageEndpoint : HtmlPageEndpointBase<string[]>
    {
        public SimpleHtmlPageEndpoint(IHtmlPageEndpointBootstrapper<string[]> bootstrapper, ISimpleHtmlPage simpleHtmlPage)
        {
            bootstrapper.Bootstrap(this, "ShowList", simpleHtmlPage,
                httpContext => httpContext.HttpRequest.QueryString.GetValues("s"));
        }
    }
}
