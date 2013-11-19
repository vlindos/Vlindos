using System;
using System.IO;

namespace Vlindos.Webserver.Webserver
{
    public interface IHttpRequestProcessor
    {
    }

    public class HttpRequestProcessor : IHttpRequestProcessor
    {
        private int currentPosition = 0 ;
        private byte[] buffer = new byte[4096]; // system pagesize
        private int offset;
        private byte[] requestEndToken = { Convert.ToByte('\r'), Convert.ToByte('\n'), Convert.ToByte('\r'), Convert.ToByte('\n') };

        public int GetRequestEndPosition()
        {
            
            for (var i = currentPosition; i < buffer.Length; i++)
            {
                for (int j = 0; j < requestEndToken.Length; j++)
                {
                    if (buffer[i + j] != requestEndToken[j])
                    {
                        break;
                    }
                }
            }
            return -1;
        }

        public void Loop(MemoryStream readStream, MemoryStream writeStream)
        {
            while (true)
            {
                offset += readStream.Read(buffer, offset, buffer.Length - offset);

                if (offset == buffer.Length)
                {
                    // signal failure
                    break;
                }
            }
        }
    }
}