namespace Users.Common.Models.Endpoint
{
    public interface IRouteDescription
    {
        string Id { get; }
        string Path { get; set; }
        object Configuration { get; }
    }

    public class RouteDescription : IRouteDescription
    {
        public string Id { get; set; }

        public string Path { get; set; }

        public object Configuration { get; set; }
    }
}
