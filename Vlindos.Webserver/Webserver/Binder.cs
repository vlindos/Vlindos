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
        private readonly ISocketAsyncEventArgsPoolFactory _socketAsyncEventArgsPoolFactory;
        private readonly IHttpRequestProcessor _requestProcessor;
        private readonly Bind _bind;
        private Socket _listenSocket;
        private ManualResetEvent _manualResetEvent;
        private ISocketAsyncEventArgsPool _socketAsyncEventArgsPool;

        public Binder(
            ISocketAsyncEventArgsPoolFactory socketAsyncEventArgsPoolFactory,
            IHttpRequestProcessor requestProcessor, 
            Bind bind)
        {
            _socketAsyncEventArgsPoolFactory = socketAsyncEventArgsPoolFactory;
            _requestProcessor = requestProcessor;
            _bind = bind;
        }

        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        /// <summary>
        /// This method is invoked when an asycnhronous receive operation completes. If the 
        /// remote host closed the connection, then the socket is closed.  If data was received then
        /// the data is echoed back to the client.
        /// </summary>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            // check if the remote host closed the connection
            var token = (AsyncUserToken)e.UserToken;
            if (e.BytesTransferred <= 0 || e.SocketError != SocketError.Success)
            {
                CloseClientSocket(e);
                return;
            }

            //echo the data received back to the client
            var willRaiseEvent = token.Socket.SendAsync(e);
            if (!willRaiseEvent)
            {
                ProcessSend(e);
            }
        }

        /// <summary>
        /// This method is invoked when an asynchronous send operation completes.  The method issues another receive
        /// on the socket to read any additional data sent from the client
        /// </summary>
        /// <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                CloseClientSocket(e);
                return;
            }
            // done echoing data back to the client
            var token = (AsyncUserToken) e.UserToken;
            // read the next block of data send from the client
            var willRaiseEvent = token.Socket.ReceiveAsync(e);
            if (!willRaiseEvent)
            {
                ProcessReceive(e);
            }
        }

        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            var token = (AsyncUserToken)e.UserToken;

            // close the socket associated with the client
            try
            {
                token.Socket.Shutdown(SocketShutdown.Send);
            }
            // throws if client process has already closed
            catch  { }
            token.Socket.Close();

            // Free the SocketAsyncEventArg so they can be reused by another client
            _socketAsyncEventArgsPool.Push(e);
        }
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            // Get the socket for the accepted client connection and put it into the 
            //ReadEventArg object user token
            SocketAsyncEventArgs readEventArgs;
            if (_socketAsyncEventArgsPool.TryPop(out readEventArgs) == false)
            {
                throw new NotImplementedException();
            }
            ((AsyncUserToken)readEventArgs.UserToken).Socket = e.AcceptSocket;

            // As soon as the client is connected, post a receive to the connection
            var willRaiseEvent = e.AcceptSocket.ReceiveAsync(readEventArgs);
            if (!willRaiseEvent)
            {
                ProcessReceive(readEventArgs);
            }

            // Accept the next connection request
            StartAccept(e);
        }



        /// <summary>
        /// This method is called whenever a receive or send opreation is completed on a socket 
        /// </summary> 
        private void IOCompleted(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        /// <summary>
        /// Begins an operation to accept a connection request from the client 
        /// </summary>
        /// <param name="acceptEventArg">The context object to use when issuing the accept operation on the 
        /// server's listening socket</param>
        public void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += AcceptEventArg_Completed;
            }
            else
            {
                // socket must be cleared since the context object is being reused
                acceptEventArg.AcceptSocket = null;
            }

            _manualResetEvent.WaitOne();
            var willRaiseEvent = _listenSocket.AcceptAsync(acceptEventArg);
            if (!willRaiseEvent)
            {
                ProcessAccept(acceptEventArg);
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

            _socketAsyncEventArgsPool = _socketAsyncEventArgsPoolFactory.GetSocketAsyncEventArgsPool(
                _bind.MaximumOpenedConnections,
                (uint)Environment.SystemPageSize, IOCompleted);

            StartAccept(null);    
        }

        public void Unbind()
        {
             _listenSocket.Close();
        }
    }
}
