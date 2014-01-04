using System;
using Framework.Web.Application;

namespace Framework.Web.Tools
{
    public interface ICookieValidator
    {
        bool ValidateCookie(HttpRequest httpRequest, HeaderCookie headerCookie);
    }

    public class CookieValidator : ICookieValidator
    {
        public bool ValidateCookie(HttpRequest httpRequest, HeaderCookie headerCookie)
        {
            if (headerCookie.Secure != null && headerCookie.Secure == true && httpRequest.UsesSsl == false)
            {
                return false;
            }
            if (headerCookie.Path != null && httpRequest.Path.StartsWith(headerCookie.Path) == false)
            {
                return false;
            }

            if (headerCookie.Expires != null && DateTimeOffset.Now > headerCookie.Expires)
            {
                return false;
            }

            if (headerCookie.Domain == null) return true;

            var cookieHosts = headerCookie.Domain.Split('.');
            var requestHosts = httpRequest.ServerDomain.Split('.');
            var ok = true;
            for (var i = cookieHosts.Length; i >= 0; i--)
            {
                var cookieHost = cookieHosts[i];
                if (cookieHost == "*")
                {
                    break;
                }
                if (requestHosts.Length - 1 < i)
                {
                    ok = false;
                    break;
                }
                var requestHost = requestHosts[i];
                if (requestHost == cookieHost) continue;
                ok = false;
                break;
            }
            return ok;
        }
    }
}