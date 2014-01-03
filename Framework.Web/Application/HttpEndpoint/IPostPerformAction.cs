namespace Framework.Web.Application.HttpEndpoint
{
    public interface IPostPerformAction
    {
        bool PostPerform(HttpContext httpContext);
    }
}