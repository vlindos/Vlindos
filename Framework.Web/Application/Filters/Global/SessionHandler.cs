using System.Linq;
using Framework.Web.Application.Session;
using Framework.Web.Models;
using Framework.Web.Models.FiltersObjects;

namespace Framework.Web.Application.Filters.Global
{
    public class SessionHandler: IAfterPerformGlobalFilter, IBeforePerformGlobalFilter
    {
        private readonly ISessionIdSpecifier _sessionIdSpecifier;
        private readonly ISessionRepositoryManager _sessionRepositoryManager;
        private readonly ISessionObjectsGroup _sessionObjectsGroup;
        private readonly ISessionValueReader _sessionValueReader;
        public int Priority { get { return 1000; } }

        public SessionHandler(
            ISessionIdSpecifier sessionIdSpecifier, 
            ISessionRepositoryManager sessionRepositoryManager, 
            ISessionObjectsGroup sessionObjectsGroup,
            ISessionValueReader sessionValueReader)
        {
            _sessionIdSpecifier = sessionIdSpecifier;
            _sessionRepositoryManager = sessionRepositoryManager;
            _sessionObjectsGroup = sessionObjectsGroup;
            _sessionValueReader = sessionValueReader;
        }
    
        public bool AfterPerform(HttpContext httpContext)
        {
            for (var i = httpContext.HttpRequest.Headers.Count - 1; i >= 0; i--)
            {
                var header = httpContext.HttpRequest.Headers[i];
                var delimPosition = header.IndexOf(':');
                if (delimPosition < 0) continue;
                var key = header.Substring(0, delimPosition);
                if (key != _sessionIdSpecifier.SessionId) continue;
                var sessionValueString = header.Substring(delimPosition, header.Length - delimPosition);
                if (string.IsNullOrWhiteSpace(sessionValueString)) continue;
                var sessionValue = _sessionValueReader.ReadSessionValue(sessionValueString);
                if (sessionValue == null) continue;
                httpContext.HttpRequest.FiltersObjects[_sessionObjectsGroup].Add(sessionValue);
                httpContext.HttpRequest.Session = _sessionRepositoryManager.GetRepository(sessionValue);
                break;
            }

            return true;
        }

        public bool BeforePerform(HttpContext httpContext)
        {
            var sessionValue = httpContext
                .HttpRequest
                .FiltersObjects[_sessionObjectsGroup]
                .OfType<SessionValue>()
                .FirstOrDefault();
            if (sessionValue == null) return false;
            httpContext.HttpResponse.Headers.Add(_sessionIdSpecifier.SessionId, sessionValue.ToString());
            _sessionRepositoryManager.PersistSession(sessionValue, httpContext.HttpRequest.Session);

            return true;
        }
    }
}
