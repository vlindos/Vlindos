using System.Collections.Generic;

namespace Vlindos.Webserver.Configuration
{
    public class Website
    {
        public Website()
        {
            Binds = new HashSet<Bind>();                  
            Applications = new Dictionary<string, Application>();
            CustomLocations = new Dictionary<string, Path>();
        }

        public HashSet<Bind> Binds { get; set; }

        public Path RootLocation { get; set; }

        public Dictionary<string, Path> CustomLocations { get; private set; }

        public Dictionary<string, Application> Applications { get; private set; }

        public string ConfigurationFileName { get; set; }

        public string AccessLogFileName { get; set; }
    }
}