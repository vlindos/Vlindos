using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Session
{
    public interface ISessionHandler : IBeforePerformAction, IAfterPerformAction
    {
    }

    public class SessionHandler : ISessionHandler
    {
        private readonly ISessionIdLiteralSpecifier _sessionIdLiteralSpecifier;
        private readonly ISessionRepositoryManager _sessionRepositoryManager;
        private readonly ISessionObject _sessionObject;
        private readonly ISessionValueReader _sessionValueReader;

        public SessionHandler(
            ISessionIdLiteralSpecifier sessionIdLiteralSpecifier,
            ISessionRepositoryManager sessionRepositoryManager,
            ISessionObject sessionObject,
            ISessionValueReader sessionValueReader)
        {
            _sessionIdLiteralSpecifier = sessionIdLiteralSpecifier;
            _sessionRepositoryManager = sessionRepositoryManager;
            _sessionObject = sessionObject;
            _sessionValueReader = sessionValueReader;
        }

        public bool BeforePerformAction(HttpContext httpContext)
        {
            for (var i = httpContext.HttpRequest.Headers.Count - 1; i >= 0; i--)
            {
                var header = httpContext.HttpRequest.Headers[i];
                var delimPosition = header.IndexOf(':');
                if (delimPosition < 0) continue;
                var key = header.Substring(0, delimPosition);
                if (key != _sessionIdLiteralSpecifier.SessionId) continue;
                var sessionValueString = header.Substring(delimPosition, header.Length - delimPosition);
                if (string.IsNullOrWhiteSpace(sessionValueString)) continue;
                var sessionValue = _sessionValueReader.ReadSessionValue(sessionValueString);
                if (sessionValue == null) continue;
                httpContext.ActionObjects[_sessionObject] = sessionValue;
                break;
            }

            return true;
        }

        public bool AfterPerformAction(HttpContext httpContext)
        {
            var sessionValue = (SessionValue)httpContext.ActionObjects[_sessionObject];
            if (sessionValue == null) return true;

            httpContext.HttpResponse.Headers.Add(_sessionIdLiteralSpecifier.SessionId, sessionValue.ToString());
            var session = _sessionRepositoryManager.GetRepository(sessionValue);
            _sessionRepositoryManager.PersistSession(sessionValue, session);

            return true;
        }
    }
}
