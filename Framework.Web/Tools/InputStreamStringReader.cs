using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vlindos.Common.Streams;

namespace Framework.Web.Tools
{
    public interface IInputStreamStringReader
    {
        bool ReadStringFromInputStream(IInputStream inputStream, List<string> messages, out string s);
    }

    public class InputStreamStringReader : IInputStreamStringReader
    {   
        private readonly TimeSpan _timeout;
        private readonly Encoding _encoding;

        public InputStreamStringReader(
            IBufferWriteTimeoutProvider timeoutProvider, ITextEncodingProvider encodingProvider)
        {
            _timeout = timeoutProvider.Timeout;
            _encoding = encodingProvider.Endcoding;
        }
     
        public bool ReadStringFromInputStream(IInputStream inputStream, List<string> messages, out string s)
        {
            var bytesCollection = new List<byte[]>();
            var bytes = new byte[Environment.SystemPageSize];
            while (true)
            {
                var bytesRead = inputStream.Read(bytes, _timeout);
                if (bytesRead <= 0) break;
                var bytesCopy = new byte[bytesRead];
                Buffer.BlockCopy(bytes, 0, bytesCopy, 0, bytesRead);
                bytesCollection.Add(bytesCopy);
            }
            var bytesCount = bytesCollection.Sum(bytesChunk => bytesChunk.Length);
            bytes = new byte[bytesCount];
            var offset = 0;
            foreach (var bytesChunk in bytesCollection)
            {
                Buffer.BlockCopy(bytesChunk, 0, bytes, offset, bytesChunk.Length);
                offset += bytesChunk.Length;
            }
            try
            {
                s = _encoding.GetString(bytes);
            }
            catch (Exception e)
            {
                messages.Add(e.ToString());
                s = null;
                return false;
            }
            return true;
        }
    }
}