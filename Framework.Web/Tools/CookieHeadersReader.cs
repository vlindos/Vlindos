using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Framework.Web.Session;

namespace Framework.Web.Tools
{
    public interface ICookieHeadersReader
    {
        IEnumerable<HeaderCookie> ReadCookieHeaders(NameValueCollection headers);
    }

    public class CookieHeadersReader : ICookieHeadersReader
    {
        private readonly IStandardHeadersConstants _contants;

        public CookieHeadersReader(IStandardHeadersConstants contants)
        {
            _contants = contants;
        }

        public IEnumerable<HeaderCookie> ReadCookieHeaders(NameValueCollection headers)
        {
            for (var i = headers.Count - 1; i >= 0; i--)
            {
                var header = headers[i];
                var delimPosition = header.IndexOf(':');
                if (delimPosition < 0) continue; // no ':' - invalid header - skip
                if (delimPosition == header.Length) continue; // no any value - invalid header - skip
                var key = header.Substring(0, delimPosition);
                if (key != _contants.SetCookie) continue;
                var value = header.Substring(delimPosition + 1, header.Length - delimPosition - 1);
                if (string.IsNullOrWhiteSpace(value)) continue; // empty value - invalid header - skip

                var headerCookie = new HeaderCookie();
                foreach (var token in value.Split(';').Select(x => x.Trim()))
                {
                    var tokenKeyValue = token.Split('=').Select(x => x.Trim()).ToArray();
                    var tokenKey = tokenKeyValue.FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(tokenKey)) continue;
                    switch (tokenKey.ToLowerInvariant())
                    {
                        case "httponly":
                            headerCookie.HttpOnly = true;
                            break;
                        case "secure":
                            headerCookie.Secure = true;
                            break;
                        case "expires":
                        {
                            if (tokenKeyValue.Length != 2)
                            {
                                continue;
                            }
                            var tokenValue = tokenKeyValue[1];
                            DateTimeOffset expires;
                            if (DateTimeOffset.TryParseExact(
                                tokenValue, "R", CultureInfo.InvariantCulture, DateTimeStyles.None, out expires) == false)
                            {
                                continue;
                            }
                            headerCookie.Expires = expires;
                        }
                            break;
                        case "domain":
                        {
                            if (tokenKeyValue.Length != 2) continue;
                            var tokenValue = tokenKeyValue[1];
                            headerCookie.Domain = tokenValue;
                        }
                            break;
                        case "path":
                        {
                            if (tokenKeyValue.Length != 2) continue;
                            var tokenValue = tokenKeyValue[1];
                            headerCookie.Path = tokenValue;
                        }
                            break;
                        default:
                        {
                            if (tokenKeyValue.Length != 2) continue;
                            var tokenValue = tokenKeyValue[1];
                            headerCookie.Key = tokenKey;
                            headerCookie.Value = tokenValue;
                        }
                            break;
                    }

                    yield return headerCookie;
                }
            }
        }
    }
}