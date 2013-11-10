using System.Collections.Generic;

namespace Vlindos.Webserver.Configuration
{
    public class Website
    {
        public Website()
        {
            Binds = new Dictionary<string, Bind>();                  
            Applications = new Dictionary<string, Application>();
            Paths = new Dictionary<string, Path>();
        }

        public Dictionary<string, Path> Paths { get; private set; }

        public Dictionary<string, Application> Applications { get; private set; }

        public string ConfigurationFileName { get; set; }
        public string DocumentRootDirectory { get; set; }
        public string AccessLogFileName { get; set; }
        public Dictionary<string, Bind> Binds { get; private set; }
    }
}