using System.Collections.Generic;

namespace Vlindos.Webserver.Configuration
{
    public class Configuration
    {
        public Configuration()
        {
            Websites = new Dictionary<string, Website>();
            Binds = new HashSet<Bind>();
        }

        public HashSet<Bind> Binds { get; private set; }
        public Dictionary<string, Website> Websites { get; set; } 
    }
}
