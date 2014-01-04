using System;

namespace Framework.Web.Session
{
    public interface ISessionCookieLifespanProvider
    {
        TimeSpan Lifespan { get; set; }
    }
    public class DefaultSessionCookieLifespanProvider : ISessionCookieLifespanProvider
    {
        public DefaultSessionCookieLifespanProvider()
        {
            Lifespan = new TimeSpan(0, 0, 30);
        }

        public TimeSpan Lifespan { get; set; }
    }
}