namespace Framework.Web.Application.HttpEndpoint.Models
{
    public class HttpUrlDescription
    {
        HttpMethod[] HttpMethods { get; set; }

        public string Path { get; set; }
    }
}
