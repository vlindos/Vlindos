namespace Framework.Web.Application.HttpEndpoint
{
    public interface IPrePerformAction
    {
        bool PrePerform(HttpContext httpContext);
    }
}