namespace Framework.Web.Application.HttpEndpoint
{
    public interface IAfterPerformAction
    {
        bool AfterPerformAction(HttpContext httpContext);
    }
}