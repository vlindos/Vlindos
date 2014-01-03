using System.Linq;
using Framework.Web.Application;
using Framework.Web.Session;

namespace Framework.Web.Tools
{
    public interface ICookieHeadersGetter
    {
        HeaderCookie[] GetCookieHeaders(HttpContext httpContext);
    }

    public class CookieHeadersGetter : ICookieHeadersGetter
    {
        private readonly ICookieHeadersObject _cookieHeadersObject;
        private readonly ICookieHeadersReader _cookieHeadersReader;
        private readonly ICookieValidator _cookieValidator;

        public CookieHeadersGetter(
            ICookieHeadersObject cookieHeadersObject, 
            ICookieHeadersReader cookieHeadersReader,
            ICookieValidator cookieValidator)
        {
            _cookieHeadersObject = cookieHeadersObject;
            _cookieHeadersReader = cookieHeadersReader;
            _cookieValidator = cookieValidator;
        }

        public HeaderCookie[] GetCookieHeaders(HttpContext httpContext)
        {
            if (httpContext.ActionObjects.ContainsKey(_cookieHeadersObject))
            {
                return (HeaderCookie[])httpContext.ActionObjects[_cookieHeadersObject];
            }

            var headerCookies = _cookieHeadersReader.ReadCookieHeaders(httpContext.HttpRequest.Headers)
                                                    .Where(x => _cookieValidator.ValidateCookie(httpContext.HttpRequest, x))
                                                    .ToArray();
            httpContext.ActionObjects[_cookieHeadersObject] = headerCookies;
            return headerCookies;
        }
    }
}