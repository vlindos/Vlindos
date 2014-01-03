namespace Framework.Web.Application.HttpEndpoint
{
    public interface IBeforePerformAction
    {
        bool BeforePerformAction(HttpContext httpContext);
    }
}