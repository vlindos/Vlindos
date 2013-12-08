using System.Diagnostics;
using System.IO;

namespace Vlindos.Webserver.Webserver
{
    public interface IRequestProcessorFactory
    {
        IRequestProcessor GetRequestProcessor();
    }

    public interface IRequestProcessor
    {
        void Push(byte[] buffer, int bytesTransferred);
        void Initialize();
    }

    public class RequestProcessor : IRequestProcessor
    {
        //private int _bytesReceived;

        private readonly MemoryStream _memoryStream;

        public RequestProcessor()
        {
            _memoryStream = new MemoryStream();
        }

        public void Push(byte[] buffer, int bytesTransferred)
        {
            _memoryStream.WriteAsync(buffer, 0, bytesTransferred);
        }

        public void Initialize()
        {
            //_bytesReceived = 0;
        }
    }

}