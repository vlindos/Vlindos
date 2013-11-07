using System.Collections.Generic;

namespace Vlindos.Logging.Configuration
{
    public class Configuration
    {
        public bool Enabled { get; set; }
        public Level MinimumLogLevel { get; set; }
        public List<OutputPipe> OutputPipes { get; set; }
    }
}