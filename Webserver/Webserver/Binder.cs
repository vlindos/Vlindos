using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Vlindos.Webserver.Configuration;

namespace Vlindos.Webserver.Webserver
{
    public interface IBinderFactory
    {
        IBinder GetBinder(Bind bind, IHttpRequestProcessor requestProcessor);
    }
    public interface IBinder
    {
        void Bind();
        void Unbind();
    }

    public class Binder : IBinder
    {
        private readonly IConnectionWorkersPoolFactory _connectionWorkersPoolFactory;
        private readonly Bind _bind;
        private Socket _listenSocket;
        private ManualResetEvent _manualResetEvent;
        private IConnectionWorkersPool _connectionWorkersPool;

        public Binder(
            IConnectionWorkersPoolFactory connectionWorkersPoolFactory,
            Bind bind)
        {
            _connectionWorkersPoolFactory = connectionWorkersPoolFactory;
            _bind = bind;
        }


        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            var connectionWorker = (ConnectionWorker)e.UserToken;

            // close the socket associated with the client
            try
            {
                connectionWorker.AcceptSocket.Shutdown(SocketShutdown.Send);
            }
            // throws if client process has already closed
            catch  { }
            connectionWorker.AcceptSocket.Close();

            // Free the SocketAsyncEventArg so they can be reused by another client
            _connectionWorkersPool.Push(connectionWorker);
        }

        private void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            ConnectionWorker connectionWorker;
            if (_connectionWorkersPool.TryPop(out connectionWorker) == false)
            {
                CloseClientSocket(e); // pool exhausted - just close the connection
                return;
            }
            connectionWorker.AcceptSocket = e.AcceptSocket;

            if (_listenSocket.ReceiveFromAsync(e)) // if performed synchroniosly
            {
                ProcessAccept(e);
            }
        }


        /// <summary>
        /// This method is called whenever a receive or send opreation is completed on a socket 
        /// </summary> 
        private void IoCompleted(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    if (e.BytesTransferred <= 0 || e.SocketError != SocketError.Success)
                    {
                        CloseClientSocket(e);
                        return;
                    }
                    ((ConnectionWorker)e.UserToken).RequestProcessor.Push(e.Buffer, e.BytesTransferred);
                    e.SetBuffer(e.Buffer, 0, e.Buffer.Length);
                    break;
                case SocketAsyncOperation.Send:
                    if (e.BytesTransferred <= 0 || e.SocketError != SocketError.Success)
                    {
                        CloseClientSocket(e);
                        break;
                    }
                    e.SetBuffer(e.Buffer, 0, e.Buffer.Length);
                    break;
                case SocketAsyncOperation.Connect:
                    ((ConnectionWorker) e.UserToken).RequestProcessor.Initialize();
                    break; // ignore event
                case SocketAsyncOperation.Disconnect:
                    CloseClientSocket(e);
                    break; // ignore event
                case SocketAsyncOperation.None:
                    break; // ignore event
                default:
                    CloseClientSocket(e);
                    break;
            }
        }

        public void Bind()
        {
            _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var localEndPoint = new IPEndPoint(_bind.IpAddress, _bind.Port);
            _listenSocket.Bind(localEndPoint);
            _listenSocket.Listen((int) _bind.MaximumPendingConnections);

            _manualResetEvent = new ManualResetEvent(false);
            _manualResetEvent.Reset();

            _connectionWorkersPool = _connectionWorkersPoolFactory.GetSocketAsyncEventArgsPool(
                _bind.MaximumOpenedConnections,
                (uint)Environment.SystemPageSize, IoCompleted);

            var acceptEventArg = new SocketAsyncEventArgs();
            acceptEventArg.Completed += AcceptEventArg_Completed;

            _manualResetEvent.WaitOne();
            if (_listenSocket.AcceptAsync(acceptEventArg)) // if performed synchroniosly
            {
                ProcessAccept(acceptEventArg);
            }
        }

        public void Unbind()
        {
             _listenSocket.Close();
        }
    }
}
