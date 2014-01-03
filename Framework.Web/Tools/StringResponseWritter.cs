using System;
using System.Text;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Tools
{
    public interface IStringResponseWritterEncodingProvider
    {
        Encoding Endcoding { get; }
    }

    public class Utf8StringResponseWritterEncodingProvider : IStringResponseWritterEncodingProvider
    {
        public Encoding Endcoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }

    public interface IStringResponseWritterTimeoutProvider
    {
        TimeSpan Timeout { get; }
    }

    public class DefaultStringResponseWritterTimeoutProvider : IStringResponseWritterTimeoutProvider
    {
        private readonly TimeSpan _timeout;

        public DefaultStringResponseWritterTimeoutProvider()
        {
            _timeout = new TimeSpan(0, 0, 0, 5);
        }
        public TimeSpan Timeout
        {
            get
            {
                return _timeout;
            }
        }
    }

    public interface IStringResponseWritter : IResponseWritter<string>
    {
    }

    public class StringResponseWritter : IStringResponseWritter
    {
        private readonly Encoding _encoding;
        private readonly TimeSpan _timeout;
        private readonly int _bufferLen;

        public StringResponseWritter(
            IStringResponseWritterEncodingProvider encodingProvider, IStringResponseWritterTimeoutProvider timeoutProvider)
        {
            _encoding = encodingProvider.Endcoding;
            _timeout = timeoutProvider.Timeout;
            _bufferLen = Environment.SystemPageSize;
        }
        
        public void WriteResponse(HttpContext httpContext, string s)
        {
            var bytes = _encoding.GetBytes(s);
            for (long i = 0; i < bytes.Length; i += _bufferLen)
            {
                var remainingBytes = bytes.Length - i;
                var buffLength = _bufferLen > remainingBytes ? _bufferLen : remainingBytes;
                httpContext.HttpResponse.OutputStream.Write(bytes, _timeout, i, buffLength);
            }
        }
    }
}