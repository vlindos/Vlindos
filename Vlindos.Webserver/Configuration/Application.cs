using System;

namespace Vlindos.Webserver.Configuration
{
    public class Application
    {
        public string Location { get; set; }
        public string Directory { get; set; }
        public TimeSpan MaximumRunTime { get; set; }
        public string ControlSocket { get; set; }
    }
}
