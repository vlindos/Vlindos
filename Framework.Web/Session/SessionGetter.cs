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
        private readonly ISessionRepositoryManager _sessionRepositoryManager;
        private readonly ISessionObject _sessionObject;

        public SessionGetter(ISessionRepositoryManager sessionRepositoryManager, ISessionObject sessionObject)
        {
            _sessionRepositoryManager = sessionRepositoryManager;
            _sessionObject = sessionObject;
        }

        public Dictionary<string, string> GetSession(HttpContext httpContext)
        {
            var sessionValue = (SessionValue)httpContext.ActionObjects[_sessionObject];
            if (sessionValue == null)
            {
                return null;
            }
            return _sessionRepositoryManager.GetRepository(sessionValue);
        }
    }
}