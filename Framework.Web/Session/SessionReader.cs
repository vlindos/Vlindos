using System.Collections.Generic;
using System.Linq;
using Framework.Web.Application;
using Framework.Web.Tools;

namespace Framework.Web.Session
{
    public interface ISessionReader
    {
        Dictionary<string, string> ReadSession(HttpContext httpContext);
    }

    public class SessionReader : ISessionReader
    {
        private readonly ISessionIdLiteralSpecifier _sessionIdLiteralSpecifier;
        private readonly ICookieHeadersGetter _cookieHeadersGetter;

        public SessionReader(
            ISessionIdLiteralSpecifier sessionIdLiteralSpecifier, 
            ICookieHeadersGetter cookieHeadersGetter)
        {
            _sessionIdLiteralSpecifier = sessionIdLiteralSpecifier;
            _cookieHeadersGetter = cookieHeadersGetter;
        }

        public Dictionary<string, string> ReadSession(HttpContext httpContext)
        {
            var sessionCookie = _cookieHeadersGetter.GetCookieHeaders(httpContext)
                                                    .Last(x => x.Key == _sessionIdLiteralSpecifier.SessionId);
            if (sessionCookie == null) return null;
            // TODO: read the session items from the sessions persistence manager

            return null;
        }
    }
}