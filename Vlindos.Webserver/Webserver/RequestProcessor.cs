using System.Net.Sockets;

namespace Vlindos.Webserver.Webserver
{
    // http://msdn.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs(v=vs.110).aspx
    // http://www.w3.org/Protocols/rfc2616/rfc2616.html
    public interface IRequestProcessor
    {
        void Process(SocketAsyncEventArgs socketAsyncEventArgs);
    }

    public class RequestProcessor : IRequestProcessor
    {
        public void Process(SocketAsyncEventArgs socketAsyncEventArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}
