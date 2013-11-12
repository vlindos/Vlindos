using System.Net;

namespace Vlindos.Webserver.Configuration
{
    public class Bind
    {
        public string Name { get; set; }
        public IPAddress IpAddress { get; set; }
        public ushort Port { get; set; }
        public string HostName { get; set; }
        public string NetworkSettings { get; set; }
        public string CertificateFileName { get; set; }
        public uint MaximumPendingConnections { get; set; }
        public uint MaximumOpenedConnections { get; set; }
    }
}
