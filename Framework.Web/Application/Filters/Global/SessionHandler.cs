using System;
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
    
        public bool AfterPerform<TRequest, TResponse>(IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse)
        {
            for (var i = request.Headers.Count - 1; i >= 0; i--)
            {
                var header = request.Headers[i];
                var delimPosition = header.IndexOf(':');
                if (delimPosition < 0) continue;
                var key = header.Substring(0, delimPosition);
                if (key != _sessionIdSpecifier.SessionId) continue;
                var sessionValueString = header.Substring(delimPosition, header.Length - delimPosition);
                if (string.IsNullOrWhiteSpace(sessionValueString)) continue;
                var sessionValue = _sessionValueReader.ReadSessionValue(sessionValueString);
                if (sessionValue == null) continue;
                request.FiltersObjects[_sessionObjectsGroup].Add(sessionValue);
                request.Session = _sessionRepositoryManager.GetRepository(sessionValue);
                break;
            }

            return true;
        }

        public bool BeforePerform<TRequest, TResponse>(IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse)
        {
            var sessionValue = request.FiltersObjects[_sessionObjectsGroup].OfType<SessionValue>().FirstOrDefault();
            if (sessionValue == null) return false;
            httpResponse.Headers.Add(_sessionIdSpecifier.SessionId, sessionValue.ToString());
            _sessionRepositoryManager.PersistSession(sessionValue, request.Session);

            return true;
        }
    }
}
