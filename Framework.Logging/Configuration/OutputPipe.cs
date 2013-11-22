using System;

namespace Vlindos.Logging.Configuration
{
    public class OutputPipe
    {
        public Level MinimumLogLevel { get; set; }
        public TimeSpan BufferMaximumKeepTime { get; set; }
        public IOutput Output { get; set; }
        public IQueue Queue { get; set; }
        public IOutputEngine OutputEngine { get; set; }
    }
}
