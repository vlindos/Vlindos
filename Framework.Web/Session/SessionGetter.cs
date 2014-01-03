using System.Collections.Generic;
using Framework.Web.Application;

namespace Framework.Web.Session
{
    public interface ISessionGetter
    {
        Dictionary<string, string> GetSession(HttpContext httpContext);
    }
    
    public class SessionGetter : ISessionGetter
    {
        private readonly ISessionReader _sessionReader;
        private readonly ISessionObject _sessionObject;

        public SessionGetter(ISessionReader sessionReader,ISessionObject sessionObject)
        {
            _sessionReader = sessionReader;
            _sessionObject = sessionObject;
        }

        public Dictionary<string, string> GetSession(HttpContext httpContext)
        {
            if (httpContext.ActionObjects.ContainsKey(_sessionObject))
            {
                return (Dictionary<string, string>)httpContext.ActionObjects[_sessionObject];
            }
            var sessionObject = _sessionReader.ReadSession(httpContext);
            httpContext.ActionObjects[_sessionObject] = sessionObject;
            return sessionObject;
        }
    }
}