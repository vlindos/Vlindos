using System;

namespace Vlindos.Common.Streams
{
    public interface IInputStream
    {
        int Read(byte[] bytes, TimeSpan timeout);
    }
}
