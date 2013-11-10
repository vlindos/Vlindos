using System.Collections.Generic;

namespace Vlindos.Webserver.Configuration
{
    public class Configuration
    {
        public Configuration()
        {
            NetworkSettings = new NetworkSettings();
            Websites = new Dictionary<string, Website>();
        }

        public NetworkSettings NetworkSettings { get; private set; }
        public Dictionary<string, Website> Websites { get; set; } 
    }
}
