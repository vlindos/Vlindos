using System;
using System.Net.Mime;
using System.Text;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Tools
{
    public interface IStringResponseWritter : IResponseWritter<string>
    {
    }

    public class StringResponseWritter : IStringResponseWritter
    {
        private readonly IStandardHeadersConstants _constants;
        private readonly Encoding _encoding;
        private readonly TimeSpan _timeout;
        private readonly int _bufferLen;

        public StringResponseWritter(
            ITextEncodingProvider encodingProvider, 
            IBufferWriteTimeoutProvider timeoutProvider,
            IStandardHeadersConstants constants)
        {
            _constants = constants;
            _encoding = encodingProvider.Endcoding;
            _timeout = timeoutProvider.Timeout;
            _bufferLen = Environment.SystemPageSize;
        }
        
        public void WriteResponse(HttpContext httpContext, string s)
        {
            httpContext.HttpResponse.Headers.Add(
                _constants.ContentType, 
                new ContentType(_encoding.EncodingName).ToString());

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