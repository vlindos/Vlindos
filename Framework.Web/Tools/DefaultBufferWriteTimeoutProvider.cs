using System;

namespace Framework.Web.Tools
{
    public interface IBufferWriteTimeoutProvider
    {
        TimeSpan Timeout { get; }
    }

    public class DefaultBufferWriteTimeoutProvider : IBufferWriteTimeoutProvider
    {
        private readonly TimeSpan _timeout;

        public DefaultBufferWriteTimeoutProvider()
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
}