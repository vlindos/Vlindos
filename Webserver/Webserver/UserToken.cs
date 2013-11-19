using System.Net.Sockets;

namespace Vlindos.Webserver.Webserver
{
    public class UserToken
    {
        public Socket Socket { get; set; }
        public IRequestProcessor RequestProcessor { get; set; }
    }
}
