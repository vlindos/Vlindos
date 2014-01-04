using System.Linq;
using Framework.Web.Application;
using Framework.Web.Tools;

namespace Framework.Web.Session
{
    public interface ISessionReader
    {
        IHttpSession ReadSession(HttpContext httpContext);
    }

    public class SessionReader : ISessionReader
    {
        private readonly ISessionIdLiteralSpecifier _sessionIdLiteralSpecifier;
        private readonly ICookieHeadersGetter _cookieHeadersGetter;
        private readonly IHttpSessionRepository _httpSessionRepository;
        private readonly IDefaultSessionCookieFactory _defaultSessionCookieFactory;
        private readonly IStandardHeadersConstants _contants;

        public SessionReader(
            ISessionIdLiteralSpecifier sessionIdLiteralSpecifier, 
            ICookieHeadersGetter cookieHeadersGetter,
            IHttpSessionRepository httpSessionRepository,
            IDefaultSessionCookieFactory defaultSessionCookieFactory,
            IStandardHeadersConstants contants)
        {
            _sessionIdLiteralSpecifier = sessionIdLiteralSpecifier;
            _cookieHeadersGetter = cookieHeadersGetter;
            _httpSessionRepository = httpSessionRepository;
            _defaultSessionCookieFactory = defaultSessionCookieFactory;
            _contants = contants;
        }

        public IHttpSession ReadSession(HttpContext httpContext)
        {
            var sessionCookie = _cookieHeadersGetter.GetCookieHeaders(httpContext)
                                                    .Last(x => x.Key == _sessionIdLiteralSpecifier.SessionId);
            if (sessionCookie != null) return _httpSessionRepository.GetHttpSession(sessionCookie.Value);
            
            sessionCookie = _defaultSessionCookieFactory.GetDefaultSessionCookie(httpContext);
            httpContext.HttpResponse.Headers.Add(_contants.SetCookie, sessionCookie.ToString());

            return _httpSessionRepository.GetHttpSession(sessionCookie.Value);
        }
    }
}