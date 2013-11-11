using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

// http://msdn.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs(v=vs.110).aspx
// http://www.w3.org/Protocols/rfc2616/rfc2616.html
namespace Vlindos.Webserver.Webserver
{
    public interface IWebserver
    {
        bool Start(Configuration.Configuration configuration);
        void Stop();
    }

    public class Webserver : IWebserver
    {
        public Dictionary<string, Dictionary<Application, HashSet<string>>> ApplicationHostsLocationsl;

        public Webserver()
        {

        }

        public bool Start(Configuration.Configuration configuration)
        {

            //m_bufferManager = new BufferManager(receiveBufferSize 
            //    * configuration.NetworkSettings.MaximumOpenedConnections 
            //    * opsToPreAlloc,
            //    receiveBufferSize);

            //m_readWritePool = new SocketAsyncEventArgsPool(configuration.NetworkSettings.MaximumOpenedConnections);
            //m_maxNumberAcceptedClients = new Semaphore(
            //    configuration.NetworkSettings.MaximumOpenedConnections, 
            //    configuration.NetworkSettings.MaximumOpenedConnections); 
            //foreach (var website in configuration.Websites)
            //{
            //    foreach (var bind in website.Value.Binds)
            //    {
            //        var socketAsyncEventArgs = new SocketAsyncEventArgs();
            //        _socketAsyncEventArgses.Add(socketAsyncEventArgs);
                    
            //    }
                
            //}

            return true;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}