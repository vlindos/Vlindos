using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Vlindos.Webserver.Webserver
{
    public interface ISocketAsyncEventArgsPoolFactory
    {
        ISocketAsyncEventArgsPool GetSocketAsyncEventArgsPool(
            uint numberOfBuffers, uint bufferSize, Action<object, SocketAsyncEventArgs> ioCompleted);
    }

    public interface ISocketAsyncEventArgsPool
    {
        bool TryPop(out SocketAsyncEventArgs socketAsyncEventArgs);
        uint Count { get; }
        uint BuffersSize { get; }
        void Push(SocketAsyncEventArgs socketAsyncEventArgs);
    }

    public class SocketAsyncEventArgsPool : ISocketAsyncEventArgsPool
    {
        private readonly uint _buffersSize;
        private readonly ConcurrentStack<SocketAsyncEventArgs> _readWriteAsyncEventArgs;

        public SocketAsyncEventArgsPool(
            uint numbersOfBuffers, uint buffersSize, Action<object, SocketAsyncEventArgs> ioCompleted)
        {
            _buffersSize = buffersSize;
            _readWriteAsyncEventArgs = new ConcurrentStack<SocketAsyncEventArgs>();

            for (var i = 0; i < numbersOfBuffers; i++)
            {
                var buffer = new byte[buffersSize];
                for (var j = 0; j < buffer.Length; j++)
                {
                    buffer[j] = (byte)j;
                }
                var readWriteAsync = new SocketAsyncEventArgs();
                readWriteAsync.Completed += new EventHandler<SocketAsyncEventArgs>(ioCompleted);
                readWriteAsync.UserToken = new AsyncUserToken();
                readWriteAsync.SetBuffer(buffer, 0, (int)buffersSize);
                _readWriteAsyncEventArgs.Push(readWriteAsync);
            }
        }

        public bool TryPop(out SocketAsyncEventArgs socketAsyncEventArgs)
        {
            return _readWriteAsyncEventArgs.TryPop(out socketAsyncEventArgs);
        }

        public void Push(SocketAsyncEventArgs socketAsyncEventArgs)
        {
            _readWriteAsyncEventArgs.Push(socketAsyncEventArgs);
            socketAsyncEventArgs.SetBuffer(socketAsyncEventArgs.Buffer, 0, (int)BuffersSize);
        }

        public uint Count { get { return (uint)_readWriteAsyncEventArgs.Count; } }
        public uint BuffersSize { get { return _buffersSize; } }
    }
}
