using System.Net.Sockets;

namespace Vlindos.Webserver.Webserver
{
    /// <summary>
    /// This class is designed for use as the object to be assigned to the SocketAsyncEventArgs.UserToken property. 
    /// </summary>
    class AsyncUserToken
    {
        public AsyncUserToken() : this(null) { }

        public AsyncUserToken(Socket socket)
        {
            Socket = socket;
        }

        public Socket Socket { get; set; }
    }
}
