using System;

namespace Vlindos.Common.Streams
{
    public interface IOutputStream
    {
        int Write(byte[] bytes, TimeSpan timeout, long offset = 0, long maxLen = -1);
    }
}