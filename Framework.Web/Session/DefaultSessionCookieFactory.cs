using System;
using Framework.Web.Application;
using Framework.Web.Tools;

namespace Framework.Web.Session
{
    public interface IDefaultSessionCookieFactory
    {
        HeaderCookie GetDefaultSessionCookie(HttpContext httpContext);
    }

    public class DefaultSessionCookieFactory : IDefaultSessionCookieFactory
    {
        private readonly string _sessionIdLiteral;
        private readonly TimeSpan _lifespan;

        public DefaultSessionCookieFactory(ISessionIdLiteralSpecifier literalSpecifier, ISessionCookieLifespanProvider lifespanProvider)
        {
            _sessionIdLiteral = literalSpecifier.SessionId;
            _lifespan = lifespanProvider.Lifespan;
        }

        public HeaderCookie GetDefaultSessionCookie(HttpContext httpContext)
        {
            return new HeaderCookie
            {
                Domain = httpContext.HttpRequest.ServerDomain,
                Expires = DateTimeOffset.Now.Add(_lifespan),
                HttpOnly = true,
                Key = _sessionIdLiteral,
                Value = Guid.NewGuid().ToString(),
                Path = "/",
                Secure = httpContext.HttpRequest.UsesSsl
            };
        }
    }
}