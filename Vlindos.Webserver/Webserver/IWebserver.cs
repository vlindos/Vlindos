using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IBuffersManagerFactory _buffersManagerFactory;
        public Dictionary<string, Dictionary<Application, HashSet<string>>> ApplicationHostsLocationsl;
        private IBuffersManager _buffersManager;

        public Webserver(IRequestProcessor requestProcessor, IBuffersManagerFactory buffersManagerFactory)
        {
            _buffersManagerFactory = buffersManagerFactory;
        }

        public bool Start(Configuration.Configuration configuration)
        {
            _buffersManager =
                _buffersManagerFactory.GetBuffersManager(configuration.NetworkSettings.MaximumOpenedConnections,
                    (uint)Environment.SystemPageSize);
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

    public interface IBuffersManagerFactory
    {
        IBuffersManager GetBuffersManager(uint numberOfBuffers, uint bufferSize);
    }

    public interface IBuffersManager
    {
    }

    public class BuffersManager : IBuffersManager
    {
        private readonly uint _numbersOfBuffers;
        private readonly uint _buffersSize;
        private byte[][] _buffers;

        public BuffersManager(uint numbersOfBuffers, uint buffersSize)
        {
            _numbersOfBuffers = numbersOfBuffers;
            _buffersSize = buffersSize;
            //_buffers = new ConcurrentStack<byte[]>((int)numbersOfBuffers);   
        }
    }

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