using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace Users.Common.Models.Endpoint
{
    public interface IHttpRequest
    {
        string UserHostAddress { get; }
        string UserAgent { get; set; }
        string RawUrl { get; set; }
        NameValueCollection Headers { get; set; }
        NameValueCollection QueryString { get; set; }
        RouteValueDictionary RoutesValues { get; set; }

        string Method { get; set; }
        string HttpUsername { get; set; }
        string HttpPassword { get; set; }
        IEnumerable<byte[]> PostData { get; set; }

        Uri GetUri();
    }

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest()
        {
            Method = "GET";
        }

        public string Method { get; set; }
        public string HttpUsername { get; set; }
        public string HttpPassword { get; set; }

        public IEnumerable<byte[]> PostData { get; set; }

        public string UserHostAddress { get; set; }

        public string UserAgent { get; set; }

        public string RawUrl { get; set; }

        public NameValueCollection Headers { get; set; }

        public RouteValueDictionary RoutesValues { get; set; }

        public NameValueCollection QueryString { get; set; }

        public Uri GetUri()
        {
            var sb = new StringBuilder();
            if (RoutesValues != null)
            {
                foreach (var routesValue in RoutesValues)
                {
                    RawUrl = RawUrl.Replace("{" + routesValue.Key + "}", routesValue.Value.ToString());
                }
            }
            sb.Append(RawUrl);
            if (QueryString != null)
            {
                for (var i = 0; i < QueryString.Count; i++)
                {
                    var key = QueryString.AllKeys[i];
                    sb.AppendFormat("{0}{1}={2}", (i == 0 ? "?" : "&"), key, HttpUtility.UrlEncode(QueryString[key]));
                }
            }
            return new Uri(sb.ToString());
        }
    }
}
