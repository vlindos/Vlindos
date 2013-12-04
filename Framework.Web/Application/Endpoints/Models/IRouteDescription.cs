namespace Framework.Web.Application.Endpoints.Models
{
    public class RouteDescription
    {
        HttpMethod[] HttpMethods { get; set; }

        public string Path { get; set; }
    }
}
