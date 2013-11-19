using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Vlindos.Webserver.Webserver
{
    public class ConnectionWorker
    {
        public Socket AcceptSocket { get; set; }
        public IRequestProcessor RequestProcessor { get; set; }
        public SocketAsyncEventArgs SocketAsyncEventArgs { get; set; }
    }

    public interface IConnectionWorkersPoolFactory
    {
        IConnectionWorkersPool GetSocketAsyncEventArgsPool(
            uint numberOfBuffers, uint bufferSize, Action<object, SocketAsyncEventArgs> ioCompleted);
    }

    public interface IConnectionWorkersPool
    {
        bool TryPop(out ConnectionWorker socketAsyncEventArgs);
        uint UsedNumber { get; }
        uint BuffersSize { get; }
        void Push(ConnectionWorker socketAsyncEventArgs);
    }

    public class ConnectionWorkersPool : IConnectionWorkersPool, IDisposable
    {
        private readonly uint _buffersSize;
        private readonly ConcurrentStack<ConnectionWorker> _connectionWorkers;
        private readonly uint _numbersOfBuffers;

        public ConnectionWorkersPool(
            uint numbersOfBuffers, uint buffersSize, Action<object, SocketAsyncEventArgs> ioCompleted, 
            IRequestProcessorFactory requestProcessorFactory)
        {
            _buffersSize = buffersSize;
            _numbersOfBuffers = numbersOfBuffers;
            _connectionWorkers = new ConcurrentStack<ConnectionWorker>();

            for (var i = 0; i < numbersOfBuffers; i++)
            {
                var buffer = new byte[buffersSize];
                for (var j = 0; j < buffer.Length; j++)
                {
                    buffer[j] = (byte)j;
                }
                var connectionWorker = new ConnectionWorker
                {
                    RequestProcessor = requestProcessorFactory.GetRequestProcessor()
                };
                var readWriteAsync = new SocketAsyncEventArgs {UserToken = connectionWorker};
                connectionWorker.SocketAsyncEventArgs = readWriteAsync;
                readWriteAsync.Completed += new EventHandler<SocketAsyncEventArgs>(ioCompleted);
                _connectionWorkers.Push(connectionWorker);
            }
        }

        public bool TryPop(out ConnectionWorker connectionWorker)
        {
            return _connectionWorkers.TryPop(out connectionWorker);
        }

        public void Push(ConnectionWorker connectionWorker)
        {
            connectionWorker.AcceptSocket = null;
            connectionWorker.SocketAsyncEventArgs.SetBuffer(
                connectionWorker.SocketAsyncEventArgs.Buffer, 0, (int)BuffersSize);

            _connectionWorkers.Push(connectionWorker);
        }

        public uint BuffersSize { get { return _buffersSize; } }
        public uint UsedNumber { get { return (uint) (_numbersOfBuffers - _connectionWorkers.Count); } }
        public void Dispose()
        {
            ConnectionWorker connectionWorker;
            while (TryPop(out connectionWorker))
            {
                connectionWorker.AcceptSocket = null;
                connectionWorker.RequestProcessor = null;
                connectionWorker.SocketAsyncEventArgs.UserToken = null;
                connectionWorker.SocketAsyncEventArgs = null;
            }
        }
    }
}
