namespace Framework.Web.Application.HttpEndpoint
{
    public interface IResponseWritter<in TResponse>
    {
        void WriteResponse(HttpContext httpContext, TResponse response);
    }
}
