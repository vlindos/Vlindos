using System.Security.Policy;

namespace Vlindos.Webserver.Configuration
{
    public class Path
    {
        public string Location { get; set; }
        public string Directory { get; set; }
        public HttpAuthentication HttpAuthentication { get; set; }
        public Url AuthenticationSourceUrl { get; set; }
    }
}